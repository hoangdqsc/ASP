using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Application_Temp.Extentions
{
    public static class XmlExtensions
    {
        private static readonly Encoding _defaultEncoding = Encoding.UTF8;

        /// <summary>
        /// Chuyển đổi chuỗi XML thành đối tượng kiểu T.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng cần chuyển đổi.</typeparam>
        /// <param name="xml">Chuỗi XML cần chuyển đổi.</param>
        /// <returns>Đối tượng kiểu T.</returns>
        public static T FromXml<T>(this string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException("Argument cannot be null or empty.", nameof(xml));

            var serializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }

        /// <summary>
        /// Chuyển đổi đối tượng kiểu T thành chuỗi XML.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng cần chuyển đổi.</typeparam>
        /// <param name="obj">Đối tượng cần chuyển đổi.</param>
        /// <returns>Chuỗi XML.</returns>
        public static string ToXml<T>(this T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Argument cannot be null.");

            var serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}
/*
1 //Cấu hình trong Program.cs
// Đối với ASP.NET Core 6+
var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ hỗ trợ XML
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

var app = builder.Build();

2. Sử Dụng Các Phương Thức Mở Rộng
Sau khi bạn đã tạo lớp tiện ích, bạn có thể sử dụng các phương thức mở rộng này trong các controller
của bạn để chuyển đổi dữ liệu giữa XML và đối tượng.

using Microsoft.AspNetCore.Mvc;
using YourNamespace; // Thay thế bằng namespace của bạn


namespace YourApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        public IActionResult CreateBook([FromBody] string xmlBook)
        {
            try
            {
                var bookDto = xmlBook.FromXml<BookDto>(); // Chuyển đổi từ XML thành BookDto
                // Xử lý logic tạo sách với bookDto
                return Ok("Book created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetBook()
        {
            var bookDto = new BookDto
            {
                Id = 1,
                Title = "Sample Book",
                Author = "Author Name"
            };
            var xml = bookDto.ToXml(); // Chuyển đổi từ BookDto thành XML
            return Content(xml, "application/xml");
        }
    }
}


*/
