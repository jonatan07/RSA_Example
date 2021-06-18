using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Security
{
    public class RSACryptoService
    {
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        public RSACryptoService()
        {
            _privateKey = csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);

        }
        public string GetKey() => csp.ToXmlString(false);
        public RSAParameters x(string key)
        {
            var sr = new StringReader(key);
            var xs = new XmlSerializer(typeof(RSAParameters));
            var t = (RSAParameters)xs.Deserialize(sr);
            return t;
        }
        public string PublicKeyString()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }
        public string Encrypt(string d, RSAParameters key)
        {

            csp.ImportParameters(key);

            var data = Encoding.Unicode.GetBytes(d);
            var cynpher = csp.Encrypt(data, false);
            return Convert.ToBase64String(cynpher);
        }
        public string Descrypt(string cyd)
        {
            var data = Convert.FromBase64String(cyd);
            csp.ImportParameters(_privateKey);
            var plaint = csp.Decrypt(data, false);
            return Encoding.Unicode.GetString(plaint);
        }
    }
}
