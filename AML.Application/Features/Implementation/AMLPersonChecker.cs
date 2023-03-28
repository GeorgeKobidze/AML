using AML.Application.Features.Interface;
using AML.ExternalApi.ApiClient;
using AML.Model.Api;
using AML.Model.Api.Request;
using AML.Model.Api.Response;
using AML.Model.ExternalApi.Tron;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AML.Application.Features.Implementation
{
    public class AMLPersonChecker : IAMLPersonChecker
    {
        private readonly ITronApiClient _tronApiClient;
        private readonly ILogger<AMLPersonChecker> _logger;

        public AMLPersonChecker(ITronApiClient tronApiClient,ILogger<AMLPersonChecker> logger)
        {
            _tronApiClient = tronApiClient;
            _logger = logger;
        }

        public async Task<ApiServiceResponse<PersonCheckerResponseDTO>> PersonChecker(PersonCheckerRequestDTO personCheckerRequest)
        {
            var part = new List<ParticipantAddress>();
            var count = 0;

            var url = ConcatURL(personCheckerRequest.Address, personCheckerRequest.FromDate, personCheckerRequest.ToDate);
            var res = await _tronApiClient.CallMethod<TronApiResponse>(url);

            if (!res.data.Any())
                return new BadRequestApiServiceResponse<PersonCheckerResponseDTO>(null);

            var suspects = res.data.Select(e => e.raw_data)
                                .OrderBy(e => e.timestamp)
                                .SelectMany(e => e.contract)
                                .Where(b => b.type.ToLower() == "TransferContract".ToLower())
                                .ToList();


            if (personCheckerRequest.HierachyLevel != count)
            {                
                var suspect = suspects[count];

                part.Add(new ParticipantAddress()
                {
                    Amount = suspect.parameter.value.amount,
                    Owner_address = suspect.parameter.value.owner_address,
                    To_address = suspect.parameter.value.to_address
                });
                
                count++;

                var address = suspect.parameter.value.owner_address == personCheckerRequest.Address ? suspect.parameter.value.to_address : suspect.parameter.value.owner_address;

                var Perdata = await PersonChecker(new PersonCheckerRequestDTO()
                {
                    Address = address,
                    FromDate = personCheckerRequest.FromDate,
                    ToDate = personCheckerRequest.ToDate,
                    HierachyLevel = personCheckerRequest.HierachyLevel - 1
                });

                if (Perdata.IsOk())
                    part.AddRange(Perdata.Data.ParticipantAddresses);
            }


            var data = new PersonCheckerResponseDTO()
            {
                ParticipantAddresses = part,
                RequestedHierachyLevel = personCheckerRequest.HierachyLevel,
                TargetAddress = personCheckerRequest.Address
            };

            if (res == null)
                return new SuccessApiServiceResponse<PersonCheckerResponseDTO>(data);

            return new SuccessApiServiceResponse<PersonCheckerResponseDTO>(data);
        }


        private string ConcatURL(string Address, DateTime? FromDate, DateTime? ToDate)
        {
            var url = new StringBuilder();
            url.Append($"v1/accounts/{Address}/transactions");

            if (FromDate.HasValue)
                url.Append($"?min_timestamp={FromDate.Value.ToString("yyyy-MM-dd")}");
            if (ToDate.HasValue && FromDate.HasValue)
                url.Append($"&max_timestamp={ToDate.Value.ToString("yyyy-MM-dd")}");
            else if(ToDate.HasValue)
                url.Append($"?max_timestamp={ToDate.Value.ToString("yyyy-MM-dd")}");

            return url.ToString();
        }

    }
}
