using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_API.Models
{
    public class DB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=~SecurityRSA.db");
        }

        public DbSet<SecurityInformation> SecurityInformation { get; set; }
    }
}
