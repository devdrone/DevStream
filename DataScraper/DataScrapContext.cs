using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper
{
    public class DataScrapContext : DbContext
    {
        public DbSet<moviedata> moviedata { get; set; }
        public DbSet<movielink> movielink { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-9QNMSDL;Database=devstreamdb;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}
