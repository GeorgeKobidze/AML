using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AML.Model.ExternalApi.Tron
{

    public class Contract
    {
        public Parameter parameter { get; set; }
        public string type { get; set; }
    }

    public class Datum
    {
        public List<Ret> ret { get; set; }
        public List<string> signature { get; set; }
        public string txID { get; set; }
        public int net_usage { get; set; }
        public string raw_data_hex { get; set; }
        public int net_fee { get; set; }
        public int energy_usage { get; set; }
        public int blockNumber { get; set; }
        public object block_timestamp { get; set; }
        public int energy_fee { get; set; }
        public int energy_usage_total { get; set; }
        public RawData raw_data { get; set; }
        public List<object> internal_transactions { get; set; }
        public int? withdraw_amount { get; set; }
        public int? unfreeze_amount { get; set; }
    }

    public class Links
    {
        public string next { get; set; }
    }

    public class Meta
    {
        public long at { get; set; }
        public string fingerprint { get; set; }
        public Links links { get; set; }
        public int page_size { get; set; }
    }

    public class Parameter
    {
        public Value value { get; set; }
        public string type_url { get; set; }
    }

    public class RawData
    {
        public List<Contract> contract { get; set; }
        public string ref_block_bytes { get; set; }
        public string ref_block_hash { get; set; }
        public object expiration { get; set; }
        public object timestamp { get; set; }
        public int? fee_limit { get; set; }
    }

    public class Ret
    {
        public string contractRet { get; set; }
        public int fee { get; set; }
    }

    public class TronApiResponse
    {
        public List<Datum> data { get; set; }
        public bool success { get; set; }
        public Meta meta { get; set; }
    }

    public class Value
    {
        public int amount { get; set; }
        public string owner_address { get; set; }
        public string to_address { get; set; }
        public string asset_name { get; set; }
        public string data { get; set; }
        public string contract_address { get; set; }
        public int? resource { get; set; }
        public string resource_type { get; set; }
        public int? resource_value { get; set; }
        public List<Vote> votes { get; set; }
        public int? frozen_duration { get; set; }
        public int? frozen_balance { get; set; }
    }

    public class Vote
    {
        public string vote_address { get; set; }
        public int vote_count { get; set; }
    }
}


