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
        public RSACryptoService(string xml)
        {
            csp.FromXmlString(xml);
            _privateKey = csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);
        }
        public void LoadKeys(string xml) 
        {
            csp.FromXmlString(xml);
            _privateKey= csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);
        }
        public string GetKey() => csp.ToXmlString(true);
        public string LoadKeys(string xml, string data)
        {
            csp.FromXmlString(xml);
            _privateKey = csp.ExportParameters(true);
            return Descrypt(data);
        }
        public RSAParameters ConvertPublicKey(string key)
        {
            var sr = new StringReader(key);
            var xs = new XmlSerializer(typeof(RSAParameters));
            var RSAParameter = (RSAParameters)xs.Deserialize(sr);
            return RSAParameter;
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
            var plainText = csp.Decrypt(data, false);
            return Encoding.Unicode.GetString(plainText);
        }
    }
}
