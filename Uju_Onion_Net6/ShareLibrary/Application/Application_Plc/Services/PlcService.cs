using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Core_Plc.Entities;
using Core_Plc.Interfaces;


namespace Application_Plc.Services
{
    public class PlcService : IPlcService
    {
        private readonly IPlcRepository _plcRepository;

        public PlcService(IPlcRepository plcRepository)
        {
            _plcRepository = plcRepository;
        }

        public async Task<PLCData> GetPlcDataAsync(string ipAddress, int port, ushort startAddress, ushort numberOfPoints)
        {
            return await _plcRepository.ReadCoilsAsync(ipAddress, port, startAddress, numberOfPoints);
        }

        public async Task<PLCError> GetPlcErrorAsync(string ipAddress, int port)
        {
            return await _plcRepository.GetPlcErrorAsync(ipAddress, port);
        }

        public async Task<DeviceInfo> GetDeviceInfoAsync(string ipAddress, int port)
        {
            return await _plcRepository.GetDeviceInfoAsync(ipAddress, port);
        }
    }

}
