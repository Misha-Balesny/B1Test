using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace B1_1
{
    internal class AppContext : DbContext
    {
        internal readonly string ConnectionString = @"Server=BM;Database=B1_1;Trusted_Connection=True;TrustServerCertificate=True;";

        internal AppContext()
        { 
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public DbSet<DataModel> Lines { get; set; }
    }
}

