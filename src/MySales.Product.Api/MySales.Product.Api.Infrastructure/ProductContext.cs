using Microsoft.EntityFrameworkCore;
using MySales.Product.Api.Infrastructure.Configurations;
using System;

namespace MySales.Product.Api.Infrastructure
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
            optionsBuilder.LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(ProductConfiguration.New());
            modelBuilder.ApplyConfiguration(SkuConfiguration.New());
        }
    }
}
