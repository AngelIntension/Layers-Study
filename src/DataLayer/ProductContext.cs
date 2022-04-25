using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer
{
    public class ProductContext : DbContext
    {
        public ProductContext([NotNullAttribute] DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
