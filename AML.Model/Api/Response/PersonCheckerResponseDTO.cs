using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AML.Model.Api.Response
{
    public class PersonCheckerResponseDTO
    {
        public string TargetAddress { get; set; }
        public int RequestedHierachyLevel { get; set; }
        public List<ParticipantAddress> ParticipantAddresses { get; set; }
    }

    public class ParticipantAddress
    {
        public int Amount { get; set; }
        public string Owner_address { get; set; }
        public string To_address { get; set; }
    }
}
