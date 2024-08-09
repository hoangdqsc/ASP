
using Core_Temp.Entities;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Data;
using Modbus.Device;
using System.Net.Sockets;


namespace Infrastructure_Temp.Services
{
    // Infrastructure/Services/ModbusPlcService.cs
   
   
        public class ModbusPlcService : IPlcService
        {
            private readonly ModbusIpMaster _master;
            private readonly ApplicationDbContext _context;
            private readonly string _plcIpAddress;
            private readonly int _port;

            public ModbusPlcService(string plcIpAddress, int port, ApplicationDbContext context)
            {
                _plcIpAddress = plcIpAddress;
                _port = port;
                _context = context;
                var factory = new ModbusFactory();
                var tcpClient = new TcpClient(_plcIpAddress, port);
                _master = factory.CreateMaster(tcpClient);
            }

            public bool Connect() => _master != null;

            public int GetMachineRuntime()
            {
                // Code to read machine runtime from Modbus PLC
                return 0; // Placeholder
            }

            public float GetTemperature()
            {
                // Code to read temperature from Modbus PLC
                return 0.0f; // Placeholder
            }

            public void Disconnect() => _master?.Dispose();

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


