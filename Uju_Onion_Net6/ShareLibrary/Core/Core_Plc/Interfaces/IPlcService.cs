using Core_Plc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core_Plc.Interfaces
{
          public interface IPlcService
        {
            Task<PLCData> GetPlcDataAsync(string ipAddress, int port, ushort startAddress, ushort numberOfPoints);
            Task<PLCError> GetPlcErrorAsync(string ipAddress, int port);
            Task<DeviceInfo> GetDeviceInfoAsync(string ipAddress, int port);
        }

   
}
