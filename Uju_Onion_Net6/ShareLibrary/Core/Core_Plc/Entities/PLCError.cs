using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Plc.Entities
{
    public class PLCError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
