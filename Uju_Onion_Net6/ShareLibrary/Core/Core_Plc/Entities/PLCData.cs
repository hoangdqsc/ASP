using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Plc.Entities
{
    public class PLCData
    {
        public ushort StartAddress { get; set; }          // Địa chỉ bắt đầu đọc từ PLC
        public ushort NumberOfPoints { get; set; }        // Số lượng điểm đọc từ PLC
        public bool[] Data { get; set; }                  // Dữ liệu được đọc từ PLC (trạng thái các cuộn dây)

        // Bạn có thể thêm các thuộc tính khác nếu cần thiết, tùy thuộc vào yêu cầu cụ thể của bạn.
    }

}
