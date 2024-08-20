using Microsoft.AspNetCore.Mvc;
using mvc1.Models;
using mvc1.Services;
using System.Threading.Tasks;

namespace mvc1.Controllers
{
    public class PlcController : Controller
    {
        private readonly ModbusTcpClient _modbusClient;

        public PlcController(ModbusTcpClient modbusClient)
        {
            _modbusClient = modbusClient;
        }

        public async Task<IActionResult> Index()
        {
            var model = new PlcModel
            {
                IpAddress = "192.168.1.10",
                Port = 8500 // Cập nhật cổng ở đây
            };

            if (await _modbusClient.ConnectAsync())
            {
                model.DataFromPlc = await _modbusClient.ReadHoldingRegistersAsync(0, 10);
                model.ConnectionStatus = "Connected successfully.";
            }
            else
            {
                model.DataFromPlc = "Failed to connect.";
                model.ConnectionStatus = "Connection failed.";
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Disconnect()
        {
            // Đóng kết nối
            _modbusClient.Close();

            // Chuyển hướng về trang Index và cập nhật trạng thái kết nối
            return RedirectToAction("Index");
        }
    }
}
