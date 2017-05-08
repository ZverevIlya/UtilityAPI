using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using Newtonsoft.Json;

namespace Helper.IOHelper
{
    public class Serializer
    {
        public static void Serialize<T>(T obj, string path)
        {
            using (TextWriter writer = new StreamWriter(path, false))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
                writer.Flush();
            }
        }

        public static T Deserialize<T>(string path)
        {
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// XML Serialize Helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialObject"></param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T serialObject) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            System.IO.MemoryStream mem = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8);
            ser.Serialize(writer, serialObject);
            writer.Close();

            return Encoding.UTF8.GetString(mem.ToArray());
        }

        /// <summary>
        /// XML Deserialize Helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string str) where T : class
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            StreamReader mem2 = new StreamReader(
                    new MemoryStream(System.Text.Encoding.UTF8.GetBytes(str)),
                    System.Text.Encoding.UTF8);

            return (T)mySerializer.Deserialize(mem2);
        }

        /// <summary>
        /// Json Deserialize Helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string json) where T: class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Json Serialize Helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialObject"></param>
        /// <returns></returns>
        public static string JsonSerialize<T>(T serialObject) where T : class
        {
            return JsonConvert.SerializeObject(serialObject, Newtonsoft.Json.Formatting.Indented);
        }

    }
}
