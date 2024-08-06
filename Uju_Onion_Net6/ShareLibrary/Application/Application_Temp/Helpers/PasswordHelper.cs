using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;


namespace Application_Temp.Helpers
{
    public static class PasswordHelper
    {
        // Mã hóa mật khẩu sử dụng SHA256
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password); // Chuyển mật khẩu thành bytes
                var hash = sha256.ComputeHash(bytes); // Tạo hash từ bytes
                return Convert.ToBase64String(hash); // Chuyển hash thành chuỗi Base64
            }
        }

        // Kiểm tra tính hợp lệ của mật khẩu mới
        public static bool ValidateNewPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                return false; // Mật khẩu không được rỗng và phải có ít nhất 6 ký tự
            }

            var hasUpperChar = new Regex(@"[A-Z]+"); // Kiểm tra có ít nhất 1 ký tự viết hoa
            var hasNumber = new Regex(@"[0-9]+"); // Kiểm tra có ít nhất 1 chữ số
            var hasSpecialChar = new Regex(@"[\W]+"); // Kiểm tra có ít nhất 1 ký tự đặc biệt

            return hasUpperChar.IsMatch(password) && hasNumber.IsMatch(password) && hasSpecialChar.IsMatch(password);
        }

        // Kiểm tra mật khẩu đã mã hóa
        public static bool ValidatePassword(string password, string hashedPassword)
        {
            var hash = HashPassword(password); // Mã hóa mật khẩu
            return hash == hashedPassword; // So sánh mật khẩu đã mã hóa với mật khẩu lưu trữ
        }
    }


}
