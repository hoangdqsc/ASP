using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Plc.Entities
{
    public class RealTimeData
    {
        public PLCData PlcData { get; set; }
        public PLCError PlcError { get; set; }
        public DeviceInfo DeviceInfo { get; set; }
    }
}
