using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer1
{
    public class TradingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the connection string for your specific database provider
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=wpf-stockExchange;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            // Replace "YourConnectionString" with your actual connection string
        }

        // Define DbSet properties for your entities
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyPair> CurrencyPairs { get; set; }
        // Add other DbSets for your entities
    }

}
