namespace mvc1.Models
{
    public class PlcModel
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string DataFromPlc { get; set; }
        public string ConnectionStatus { get; set; } // Thêm thuộc tính trạng thái kết nối               
        public string AccessCode { get; set; } // Mã truy cập
      
      
    }
}
