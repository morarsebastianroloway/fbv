using System.IO.Abstractions;
using System.Text;

namespace FBV.API.Managers
{
    public class FileWrapper : IFileWrapper
    {
        public string ReadAllText(string templatePath)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var est = File.ReadAllText(templatePath, Encoding.GetEncoding(1252));

            var utf = Encoding.UTF8;
            est = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(est)));

            return est;
        }
    }
}
