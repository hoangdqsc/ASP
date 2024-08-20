using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace mvc1.Services
{
    public class ModbusTcpClient
    {
        private readonly string _ipAddress;
        private readonly int _port;
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private readonly ILogger<ModbusTcpClient> _logger;

        public ModbusTcpClient(string ipAddress, int port, ILogger<ModbusTcpClient> logger)
        {
            _ipAddress = ipAddress;
            _port = port;
            _logger = logger;
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(_ipAddress, _port);
                _networkStream = _tcpClient.GetStream();
                _logger.LogInformation("Connected to PLC at {IpAddress}:{Port}", _ipAddress, _port);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error connecting to PLC at {IpAddress}:{Port}", _ipAddress, _port);
                return false;
            }
        }

        public async Task<string> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfRegisters)
        {
            if (_networkStream == null || !_tcpClient.Connected)
            {
                return "Not connected to PLC.";
            }

            try
            {
                byte[] request = CreateReadRequest(startAddress, numberOfRegisters);
                await _networkStream.WriteAsync(request, 0, request.Length);

                byte[] response = new byte[256];
                int bytesRead = await _networkStream.ReadAsync(response, 0, response.Length);

                return ParseResponse(response, bytesRead);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading holding registers from PLC.");
                return $"Error: {ex.Message}";
            }
        }

        private byte[] CreateReadRequest(ushort startAddress, ushort numberOfRegisters)
        {
            byte[] request = new byte[12];
            request[0] = 0x00; // Transaction Identifier (High Byte)
            request[1] = 0x01; // Transaction Identifier (Low Byte)
            request[2] = 0x00; // Protocol Identifier (High Byte)
            request[3] = 0x00; // Protocol Identifier (Low Byte)
            request[4] = 0x00; // Length (High Byte)
            request[5] = 0x06; // Length (Low Byte)
            request[6] = 0x01; // Unit Identifier
            request[7] = 0x03; // Function Code (Read Holding Registers)
            request[8] = (byte)(startAddress >> 8); // Start Address (High Byte)
            request[9] = (byte)(startAddress & 0xFF); // Start Address (Low Byte)
            request[10] = (byte)(numberOfRegisters >> 8); // Number of Registers (High Byte)
            request[11] = (byte)(numberOfRegisters & 0xFF); // Number of Registers (Low Byte)

            return request;
        }

        private string ParseResponse(byte[] response, int length)
        {
            if (length < 9) // Minimum length for a valid response
                return "Invalid response length.";

            // Check if there's an error code in the response
            if (response[7] >= 0x80)
            {
                return $"Error code: {response[8]:X2}";
            }

            byte byteCount = response[8];
            int numberOfRegisters = byteCount / 2;
            ushort[] registers = new ushort[numberOfRegisters];

            for (int i = 0; i < numberOfRegisters; i++)
            {
                registers[i] = (ushort)(response[9 + (i * 2)] << 8 | response[10 + (i * 2)]);
            }

            return string.Join(", ", registers);
        }

        public void Close()
        {
            _networkStream?.Close();
            _tcpClient?.Close();
            _logger.LogInformation("Disconnected from PLC.");
        }
    }
}
