using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Temp.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } // Xác nhận rằng thuộc tính Email tồn tại
        public string PasswordHash { get; set; }
         public string Roles { get; set; }
    }
}
