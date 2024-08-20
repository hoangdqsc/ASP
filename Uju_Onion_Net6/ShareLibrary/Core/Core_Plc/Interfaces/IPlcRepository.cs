using Core_Plc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Plc.Interfaces
{
    public interface IPlcRepository
    {
       
            Task<PLCData> ReadCoilsAsync(string ipAddress, int port, ushort startAddress, ushort numberOfPoints);
            Task<PLCError> GetPlcErrorAsync(string ipAddress, int port);
            Task<DeviceInfo> GetDeviceInfoAsync(string ipAddress, int port);
        
    }
}
