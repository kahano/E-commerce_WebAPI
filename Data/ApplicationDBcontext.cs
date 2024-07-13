using E_commercial_Web_RESTAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Stripe;
using Stripe.Climate;
using System.Reflection.Emit;
using Order = E_commercial_Web_RESTAPI.Models.Order;
using Product = E_commercial_Web_RESTAPI.Models.Product;

namespace E_commercial_Web_RESTAPI.Data
{
    public class ApplicationDBcontext : IdentityDbContext<AppUser>
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
        {


        }

      

        public DbSet<CartItem> CarttItems { get; set; }
        public DbSet<Cart> carts { get; set; }
        public override DbSet<AppUser> Users { get; set; }   
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> products { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Order>(s => s.HasKey(p => p.Id));
            //builder.Entity<Order>()
            //       .Property(s => s.Status)
            //        .HasConversion(
            //    o => o.ToString(),
            //    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
            //builder.Entity<Order>(k => k.Property(s => s.OrderDate));
            //builder.Entity<Order>(s => s.Property(p => p.total));



           // builder.Entity<Order>(k => k.HasMany(o => o.OrderItems));
            //builder.Entity<Order>(k => k.HasOne(o => o.Payment).WithOne());

            builder.Entity<CartItem>(s => s.HasKey(p => p.Id));
            builder.Entity<CartItem>(k => k.HasOne(o => o.Cart).WithMany(c => c.Items).HasForeignKey(o => o.CartId).OnDelete(DeleteBehavior.Cascade));


            //builder.Entity<BasketItem>(k => k.HasOne(o => o.Cart).WithMany(c => c.BasketItems).HasForeignKey(o => o.CartId));
            //builder.Entity<BasketItem>(k => k.HasOne(o => o.Product).WithOne());
            //builder.Entity<Order>(k => k.HasOne(o => o.Payment));

            builder.Entity<Product>(s => s.HasKey(p => p.Id));
            builder.Entity<Product>(s => s.Property(p => p.Name).HasMaxLength(300));
            builder.Entity<Product>(s => s.Property(p => p.Description).HasMaxLength(400));
            builder.Entity<Product>(s => s.Property(p => p.Stock));
            builder.Entity<Product>(s => s.Property(p => p.Quantity));
            builder.Entity<Product>(s => s.Property(p => p.imageurl));
            builder.Entity<Product>(s => s.Property(p => p.price).HasColumnType("decimal(18,2)"));

            builder.Entity<Product>(s => s.HasData(new Product
            {
                Id = 1,
                Description = "Introducing the XSO25 electronic product which helps online shoppers conduct transactions with ease.This powerful eCommerce tool not only directs visitors to the correct product page, but also provides inventory data and pricinginformation on products for quick purchase. With an intuitive user interface, this device makes shopping a breeze for online consumers.",
                Name = "Samsung 43 Inch Class Series LED 4K",
                imageurl = "images/Samsung 43 Inch Class Series LED 4K.jpg",
                price = 1200.00m,
                Quantity = 2
            }, new Product
            {
                Id = 2,
                Description = "Introducing the XSO25 electronic product which helps online shoppers conduct transactions with ease.This powerful eCommerce tool not only directs visitors to the correct product page, but also provides inventory data and pricinginformation on products for quick purchase. With an intuitive user interface, this device makes shopping a breeze for online consumers.",
                Name = "JBL Tune 760NC",
                imageurl = "images/JBL Tune 760NC.jpg",
                price = 300.00m,
                Quantity = 9
            }, new Product
            {
                Id = 3,
                Description = "Introducing the XSO25 electronic product which helps online shoppers conduct transactions with ease.This powerful eCommerce tool not only directs visitors to the correct product page, but also provides inventory data and pricinginformation on products for quick purchase. With an intuitive user interface, this device makes shopping a breeze for online consumers.",

                Name = "Samsung - Galaxy A71 A716U 5G Fully Unlocked 128GB - Prism Cube Black",
                imageurl = "images/Samsung - Galaxy A71 A716U 5G Fully Unlocked 128GB - Prism Cube Black.jpg",
                price = 990.00m,
                Quantity = 3
                //}
            }, new Product
            {
                Id = 4,
                Description = "Introducing the XSO35 electronic product which helps online shoppers conduct transactions with ease.This powerful eCommerce tool not only directs visitors to the correct product page, but also provides inventory data and pricinginformation on products for quick purchase. With an intuitive user interface, this device makes shopping a breeze for online consumers.",
                Name = "Samsung - Galaxy A73 A716E 5G Fully Unlocked 128GB - Prism Cube Navy",
                imageurl = "images/Samsung - Galaxy A71 A716U 5G Fully Unlocked 128GB - Prism Cube Black.jpg",
                price = 1150.00m,
                Quantity = 4
            }));


            builder.Entity<CartItem>(s => s.HasKey(p => p.Id));
            builder.Entity<CartItem>(s => s.Property(p => p.ProductName).HasMaxLength(100));
            builder.Entity<CartItem>(s => s.Property(p => p.Quantity));

            builder.Entity<CartItem>(s => s.Property(p => p.imageurl));
            builder.Entity<CartItem>(s => s.Property(p => p.Price).HasColumnType("decimal(18,2)"));

            //builder.Entity<BasketItem>(s => s.HasData(new BasketItem
            //{
            //    Id = 1,
            //    CartId = 1,
            //    ProductId = 1,
            //    ProductName = "Samsung 43 Inch Class Series LED 4K",
            //    imageurl = "images/Samsung 43 Inch Class Series LED 4K.jpg",
            //    Price = 1400.00m,
            //    Quantity = 4
            //}, new BasketItem
            //{
            //    Id = 2,
            //    CartId = 1,
            //    ProductId = 2,
            //    ProductName = "Samsung 43 Inch Class Series LED 4K",
            //    imageurl = "images/Samsung 43 Inch Class Series LED 4K.jpg",
            //    Price = 1200.00m,
            //    Quantity = 2
            //}, new BasketItem
            //{
            //    Id = 3,
            //    CartId = 2,
            //    ProductId = 3,
            //    ProductName = "Samsung - Galaxy A71 A716U 5G Fully Unlocked 128GB - Prism Cube Black",
            //    imageurl = "images/Samsung - Galaxy A71 A716U 5G Fully Unlocked 128GB - Prism Cube Black.jpg",
            //    Price = 990.00m,
            //    Quantity = 1
            //}, new BasketItem
            //{
            //    Id = 4,
            //    CartId = 2,
            //    ProductId = 4,
            //    ProductName = "Samsung - Galaxy A73 A716E 5G Fully Unlocked 128GB - Prism Cube Navy",
            //    imageurl = "images/Samsung - Galaxy A71 A716U 5G Fully Unlocked 128GB - Prism Cube Black.jpg",
            //    Price = 1150.00m,
            //    Quantity = 2
            //}));















        }
    }
}
