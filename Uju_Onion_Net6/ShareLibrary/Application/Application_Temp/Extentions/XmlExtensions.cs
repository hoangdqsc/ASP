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
