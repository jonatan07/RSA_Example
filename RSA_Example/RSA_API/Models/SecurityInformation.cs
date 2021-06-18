using Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_API.Models
{
    public class SecurityInformation
    {
        [Key]
        public Guid ID { get; set; }
        public SecurityInformation()
        {
            var rsaProvider = new RSACryptoService();
            DateCreated = DateTime.Now;
            Active = true;
            PublicKey = rsaProvider.PublicKeyString();
            Keys = rsaProvider.GetKey();
        }
        public string Apikey { get; set; }
        public string PublicKey { get; set; }
        public string Keys { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; }
    }
}
