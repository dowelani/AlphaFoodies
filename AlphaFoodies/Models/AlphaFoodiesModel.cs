using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AlphaFoodies.Models
{
    public partial class AlphaFoodiesModel : DbContext
    {
        public AlphaFoodiesModel()
            : base("name=AlphaFoodiesModelContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Chef> Chefs { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Waiter> Waiters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Chef>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Chef>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Chef>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Chef)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.Price)
                .HasPrecision(5, 2);

            modelBuilder.Entity<MenuItem>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.MenuItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Order_Time)
                .HasPrecision(0);

            modelBuilder.Entity<Order>()
                .Property(e => e.Order_Total)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.Order_PrepTime)
                .HasPrecision(0);

            modelBuilder.Entity<Order>()
                .Property(e => e.Tip)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.Email_Address)
                .IsFixedLength();

            modelBuilder.Entity<Waiter>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Waiter>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Waiter)
                .WillCascadeOnDelete(false);
        }
         
    }
}
