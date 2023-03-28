using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AML.Model.Api.Request
{
    public class PersonCheckerRequestDTO
    {
        public string Address { get; set; }
        public int HierachyLevel { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }        
    }
}
