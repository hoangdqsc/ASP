using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using S7.Net;

namespace Infrastructure.Services
{
    public class SiemensPlcService : IPlcService
    {
        private readonly S7Client _client;
        private readonly ApplicationDbContext _context;
        private readonly string _plcIpAddress;
        private readonly int _rack;
        private readonly int _slot;

        public SiemensPlcService(string plcIpAddress, int rack, int slot, ApplicationDbContext context)
        {
            _plcIpAddress = plcIpAddress;
            _rack = rack;
            _slot = slot;
            _context = context;
            _client = new S7Client();
        }

        public bool Connect() => _client.ConnectTo(_plcIpAddress, _rack, _slot) == 0;

        public int GetMachineRuntime()
        {
            // Code to read machine runtime from Siemens PLC
            return 0; // Placeholder
        }

        public float GetTemperature()
        {
            // Code to read temperature from Siemens PLC
            return 0.0f; // Placeholder
        }

        public void Disconnect() => _client.Disconnect();

        public void SaveMachineData()
        {
            var data = new MachineData
            {
                Timestamp = DateTime.Now,
                Runtime = GetMachineRuntime(),
                Temperature = GetTemperature()
            };

            _context.MachineData.Add(data);
            _context.SaveChanges();
        }
    }
}
