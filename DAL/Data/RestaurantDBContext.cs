using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Models;

namespace RestaurantManagement.DAL.Data
{
    public class RestaurantDBContext(DbContextOptions<RestaurantDBContext> options) : IdentityDbContext(options)
    {
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<MenuItem> MenuItem { get; set; } = default!;
        public DbSet<Tablespace> Tablespace{ get; set; } = default!;
        public DbSet<Order> Order{ get; set; } = default!;
        public DbSet<OrderItem> OrderItem{ get; set; } = default!;
        public DbSet<Bill> Bill { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Schedule> Schedule { get; set; } = default!;
        public DbSet<SystemSetting> SystemSetting { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SystemSetting>().HasData(
                    new SystemSetting()
                    {
                        Id = 1234,
                        VAT = 10.0m,
                        ServiceCharge = 10.0m
                    }
                );
        }
    }
}
