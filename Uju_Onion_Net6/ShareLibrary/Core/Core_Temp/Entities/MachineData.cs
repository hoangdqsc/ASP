using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Temp.Entities
{
    public class MachineData
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int Runtime { get; set; }
        public float Temperature { get; set; }
    }
}
