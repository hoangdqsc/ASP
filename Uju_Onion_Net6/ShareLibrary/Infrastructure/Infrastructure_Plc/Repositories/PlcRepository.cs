using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Core_Plc.Entities;
using Core_Plc.Interfaces;
using Modbus.Data;
using Modbus.Device;




namespace Infratructure_Plc.Repositories
{
    public class PlcRepository : IPlcRepository
        {
        public async Task<PLCData> ReadCoilsAsync(string ipAddress, int port, ushort startAddress, ushort numberOfPoints)
        {
            if (numberOfPoints > 2000)
            {
                throw new ArgumentException("The number of points must be between 1 and 2000 inclusive.", nameof(numberOfPoints));
            }

            using (var client = new TcpClient())
            {
                await client.ConnectAsync(ipAddress, port);
                var master = ModbusIpMaster.CreateIp(client);

                // Đọc trạng thái của các coil
                bool[] coilStatuses = await Task.Run(() => master.ReadCoils(startAddress, numberOfPoints));

                return new PLCData
                {
                    StartAddress = startAddress,
                    NumberOfPoints = numberOfPoints,
                    Data = coilStatuses
                };
            }
        }


        public async Task<PLCError> GetPlcErrorAsync(string ipAddress, int port)
            {
                using (var client = new TcpClient(ipAddress, port))
                {
                    var master = ModbusIpMaster.CreateIp(client);

                    // Giả sử lỗi được lưu ở địa chỉ 0
                    ushort[] errorCode = await Task.Run(() => master.ReadInputRegisters(0, 1));
                    string errorMessage = $"Error code {errorCode[0]}"; // Thay đổi theo dữ liệu thực tế

                    return new PLCError
                    {
                        ErrorCode = errorCode[0],
                        ErrorMessage = errorMessage
                    };
                }
            }

            public async Task<DeviceInfo> GetDeviceInfoAsync(string ipAddress, int port)
            {
                using (var client = new TcpClient(ipAddress, port))
                {
                    var master = ModbusIpMaster.CreateIp(client);

                    // Ví dụ lấy thông tin thiết bị từ PLC
                    string deviceName = "PLC Device"; // Thay đổi theo dữ liệu thực tế
                    string deviceStatus = "Active"; // Thay đổi theo dữ liệu thực tế

                    return new DeviceInfo
                    {
                        DeviceName = deviceName,
                        DeviceStatus = deviceStatus
                    };
                }
            }
        }
    }
