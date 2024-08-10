using System;
using System.IO;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

public static class XmlJsonConverter
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true // Định dạng đẹp cho JSON
    };

    // Chuyển đổi từ JSON sang XML
    public static string JsonToXml<T>(string json)
    {
        try
        {
            // Deserialize JSON thành đối tượng
            T obj = JsonSerializer.Deserialize<T>(json, _jsonOptions);

            if (obj == null)
                throw new InvalidOperationException("Cannot deserialize JSON to object.");

            // Serialize đối tượng thành XML
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error converting JSON to XML: {ex.Message}", ex);
        }
    }

    // Chuyển đổi từ XML sang JSON
    public static string XmlToJson<T>(string xml)
    {
        try
        {
            // Deserialize XML thành đối tượng
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader stringReader = new StringReader(xml))
            {
                T obj = (T)xmlSerializer.Deserialize(stringReader);

                if (obj == null)
                    throw new InvalidOperationException("Cannot deserialize XML to object.");

                // Serialize đối tượng thành JSON
                return JsonSerializer.Serialize(obj, _jsonOptions);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error converting XML to JSON: {ex.Message}", ex);
        }
    }

    // Tạo một đối tượng từ XML
    public static T? FromXml<T>(string xml)
    {
        try
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader stringReader = new StringReader(xml))
            {
                return (T?)xmlSerializer.Deserialize(stringReader);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error deserializing XML: {ex.Message}", ex);
        }
    }

    // Tạo một đối tượng từ JSON
    public static T? FromJson<T>(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error deserializing JSON: {ex.Message}", ex);
        }
    }

    // Chuyển đổi một đối tượng thành XML
    public static string ToXml<T>(T obj)
    {
        try
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error serializing to XML: {ex.Message}", ex);
        }
    }

    // Chuyển đổi một đối tượng thành JSON
    public static string ToJson<T>(T obj)
    {
        try
        {
            return JsonSerializer.Serialize(obj, _jsonOptions);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error serializing to JSON: {ex.Message}", ex);
        }
    }
}

/*
Sử Dụng
Dưới đây là ví dụ cách sử dụng lớp XmlJsonConverter:

// Đối tượng mẫu
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Chuyển đổi từ JSON sang XML
string json = "{\"Name\":\"John Doe\",\"Age\":30}";
string xml = XmlJsonConverter.JsonToXml<Person>(json);
Console.WriteLine(xml);

// Chuyển đổi từ XML sang JSON
string jsonResult = XmlJsonConverter.XmlToJson<Person>(xml);
Console.WriteLine(jsonResult);

// Chuyển đối tượng thành JSON
Person person = new Person { Name = "Jane Doe", Age = 25 };
string personJson = XmlJsonConverter.ToJson(person);
Console.WriteLine(personJson);

// Chuyển đối tượng thành XML
string personXml = XmlJsonConverter.ToXml(person);
Console.WriteLine(personXml);


*/

