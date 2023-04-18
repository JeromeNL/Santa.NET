using KwikKwekSnack.Data.Models;
using KwikKwekSnack.Data.Models.Products;
using KwikKwekSnack.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace KwikKwekSnack.Data
{
    public class KwikKwekSnackContext : DbContext
    {
        public DbSet<Snack> Snack { get; set; } = null!;
        public DbSet<Drink> Drink { get; set; } = null!;
        public DbSet<Order> Order { get; set; } = null!;

        public DbSet<SnackExtra> SnackExtra { get; set; } = null!;

        public DbSet<OrderSnack> OrderSnack { get; set; } = null!;

        public DbSet<OrderDrink> OrderDrink { get; set; } = null!;


        public KwikKwekSnackContext()
        {
        }

        public KwikKwekSnackContext(DbContextOptions<KwikKwekSnackContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured){
                optionsBuilder.UseSqlServer(DbUtil.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relations

            modelBuilder.Entity<Product>().ToTable("Product");


            modelBuilder.Entity<OrderSnackExtra>().HasKey(t => new { t.OrderSnackId, t.SnackExtraId });
            modelBuilder.Entity<OrderSnackExtra>()
                .HasOne(t => t.OrderSnack)
                .WithMany(t => t.OrderSnackExtra)
                .HasForeignKey(t => t.OrderSnackId);

            modelBuilder.Entity<OrderSnackExtra>()
                .HasOne(t => t.SnackExtra)
                .WithMany(t => t.OrderSnackExtra)
                .HasForeignKey(t => t.SnackExtraId);

            modelBuilder.Entity<Snack>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Drink>().Property(t => t.Id).ValueGeneratedOnAdd();

            #endregion


            #region SeedData

            #region Drinks

            modelBuilder.Entity<Drink>().HasData(
            new Drink
            {
                Id = 1,
                Name = "Cola",
                Description = "De lekkerste cola van Nederland",
                Price = 2.50m,
                ImageUrl = "https://www.kantinewinkel.nl/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/c/o/coca-cola_regular_025l_blik.png",
                InStock = true
            }
            );
            modelBuilder.Entity<Drink>().HasData(
            new Drink
            {
                Id = 2,
                Name = "Fanta",
                Description = "De lekkerste fanta van Nederland",
                Price = 2.40m,
                ImageUrl = "https://www.kantinewinkel.nl/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/f/a/fanta_orange_05l_pet.png",
                InStock = true
            }
            );
            modelBuilder.Entity<Drink>().HasData(
            new Drink
            {
                Id = 3,
                Name = "Sprite",
                Description = "De lekkerste sprite van Nederland",
                Price = 2.20m,
                ImageUrl = "https://e7.pngegg.com/pngimages/825/561/png-clipart-sprite-soda-can-sprite-can-food-sprite.png",
                InStock = false
            }
            );

            #endregion


            #region Snacks

            modelBuilder.Entity<Snack>().HasData(new Snack
            {
                Id = 4,
                Name = "Hamburger",
                Description = "Black Angus Beef Hamburger",
                Price = 5.50m,
                ImageUrl = "https://w7.pngwing.com/pngs/203/765/png-transparent-hamburger-with-vegetables-hamburger-slider-hamburger-burger-food-recipe-cheeseburger.png",
                InStock = true
            });
            modelBuilder.Entity<Snack>().HasData(new Snack
            {
                Id = 5,
                Name = "Chili Chicken",
                Description = "Een echte McDonalds Chili Chicken",
                Price = 2.50m,
                ImageUrl = "https://www.klikenbreng.nl/wp-content/uploads/2021/04/th-ChiliChicken.jpg",
                InStock = true
            });
            modelBuilder.Entity<Snack>().HasData(new Snack
            {
                Id = 6,
                Name = "Frikandel",
                Description = "Een Nederlandse Frikandel!",
                Price = 1.25m,
                ImageUrl = "https://moniquevandervloed.nl/wp-content/uploads/2017/05/frik_t.jpg",
                InStock = true
            });
            modelBuilder.Entity<Snack>().HasData(new Snack
            {
                Id = 7,
                Name = "Kroket",
                Description = "Een Nederlandse Kroket met veel smaak!",
                Price = 1.35m,
                ImageUrl = "https://i0.wp.com/www.frituurwereld.nl/wp-content/uploads/2020/10/Frituurwereld-Krokettendag-hoera.jpg",
                InStock = false
            });

            #endregion


            #region SnackExtras

            modelBuilder.Entity<SnackExtra>().HasData(new SnackExtra
            {
                Id = 1,
                Name = "Tomaat",
                Price = 0.45m
            });
            modelBuilder.Entity<SnackExtra>().HasData(new SnackExtra
            {
                Id = 2,
                Name = "Kaas",
                Price = 0.25m
            });
            modelBuilder.Entity<SnackExtra>().HasData(new SnackExtra
            {
                Id = 4,
                Name = "Sla",
                Price = 0.30m
            });
            modelBuilder.Entity<SnackExtra>().HasData(new SnackExtra
            {
                Id = 5,
                Name = "Ui",
                Price = 0.15m
            });

            #endregion

            #endregion
        }
    }
}