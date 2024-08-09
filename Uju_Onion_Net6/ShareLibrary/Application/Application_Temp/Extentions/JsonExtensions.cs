using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Application_Temp.Extentions
{
          public static class JsonExtensions
        {
            // Tạo các tùy chọn cấu hình cho JsonSerializer
            private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Sử dụng camelCase cho tên thuộc tính
                PropertyNameCaseInsensitive = true, // Cho phép phân biệt chữ hoa chữ thường trong tên thuộc tính
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // Bỏ qua các thuộc tính có giá trị null
                WriteIndented = true, // Format JSON để dễ đọc (dành cho debug)
                Converters = { new JsonStringEnumConverter() } // Hỗ trợ chuyển đổi Enum thành chuỗi
            };

            /// <summary>
            /// Chuyển đổi chuỗi JSON thành đối tượng kiểu T.
            /// </summary>
            /// <typeparam name="T">Loại đối tượng cần chuyển đổi.</typeparam>
            /// <param name="json">Chuỗi JSON cần chuyển đổi.</param>
            /// <returns>Đối tượng kiểu T.</returns>
            public static T FromJson<T>(this string json)
            {
                if (string.IsNullOrEmpty(json))
                    throw new ArgumentException("Argument cannot be null or empty.", nameof(json));

                try
                {
                    return JsonSerializer.Deserialize<T>(json, _jsonOptions);
                }
                catch (JsonException ex)
                {
                    throw new InvalidOperationException("Invalid JSON format.", ex);
                }
            }

            /// <summary>
            /// Chuyển đổi đối tượng kiểu T thành chuỗi JSON.
            /// </summary>
            /// <typeparam name="T">Loại đối tượng cần chuyển đổi.</typeparam>
            /// <param name="obj">Đối tượng cần chuyển đổi.</param>
            /// <returns>Chuỗi JSON.</returns>
            public static string ToJson<T>(this T obj)
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj), "Argument cannot be null.");

                try
                {
                    return JsonSerializer.Serialize(obj, _jsonOptions);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to serialize object to JSON.", ex);
                }
            }
        }   
}
