
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnindaKapinda.Models.Security;
using AnindaKapinda.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AnindaKapinda.Models
{
    public class ConnectionDbContext:IdentityDbContext<User,UserRole,int>
    {
        public ConnectionDbContext()
        {

        }
         public ConnectionDbContext(DbContextOptions<ConnectionDbContext> dbContext) : base(dbContext) { }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=AnindaKapindaDb;Trusted_Connection=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderDetail>().HasKey(a => new { a.OrderId,  a.ProductId } );
            base.OnModelCreating(builder);
        }
    }
}
