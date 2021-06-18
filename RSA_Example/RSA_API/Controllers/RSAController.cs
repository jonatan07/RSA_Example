using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSA_API.Models;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace RSA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSAController : ControllerBase
    {
        public RSAController()
        { 
        
        }

        [HttpGet]
        public IActionResult GenerateKey()
        {
            try
            {
                var data = new SecurityInformation();
                data.Apikey = "cc3a4097-9323-461e-8a54-bbf636901720";
                var db = new DB();
                
                
                db.SecurityInformation.Add(data);
                db.SaveChanges();
                return Content(data.Keys, "application/xml");
                //return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
  
        [HttpGet("AllKey")]
        public IActionResult AllPublicKey()
        {
            try {
                var db = new DB();
                return Ok(db.SecurityInformation.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        
    }
}
