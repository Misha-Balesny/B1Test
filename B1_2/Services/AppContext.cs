using B1_2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_2.Services
{
    internal class AppContext : DbContext
    {
        internal readonly string ConnectionString = @"Server=BM;Database=B1_2;Trusted_Connection=True;TrustServerCertificate=True;";

        internal AppContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountClass> AccountClasses { get; set; }
        public DbSet<AccountSubclass> AccountSubclasses { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
