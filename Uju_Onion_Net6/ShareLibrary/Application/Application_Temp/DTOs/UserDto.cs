using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Temp.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } // Xác nhận rằng thuộc tính Email tồn tại
        public string PasswordHash { get; set; }
    }
}
