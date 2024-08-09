using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Temp.Entities;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Data;
using EtherNetIP;
using Core_Temp.Entities;
using Core_Temp.Interfaces;

namespace Infrastructure_Temp.Services
{
    // Infrastructure/Services/AllenBradleyPlcService.cs
  

  
        public class AllenBradleyPlcService : IPlcService
        {
            private readonly EipClient _client;
            private readonly ApplicationDbContext _context;
            private readonly string _plcIpAddress;
            private readonly int _port;

            public AllenBradleyPlcService(string plcIpAddress, int port, ApplicationDbContext context)
            {
                _plcIpAddress = plcIpAddress;
                _port = port;
                _context = context;
                _client = new EipClient();
            }

            public bool Connect() => _client.Connect(_plcIpAddress, _port);

            public int GetMachineRuntime()
            {
                // Code to read machine runtime from AllenBradley PLC
                return 0; // Placeholder
            }

            public float GetTemperature()
            {
                // Code to read temperature from AllenBradley PLC
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
