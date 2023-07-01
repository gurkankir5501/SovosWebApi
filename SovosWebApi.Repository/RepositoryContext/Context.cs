using Microsoft.EntityFrameworkCore;
using SovosWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.Repository.RepositoryContext
{
    public class Context : DbContext
    {
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        // Add-Migration NewMigration
        // update-database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<InvoiceLine>().Property(s => s.UnitPrice).HasColumnType<decimal>(typeof(decimal).Name);
            base.OnModelCreating(modelBuilder);
        }


    }
}
