using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Assembly demek aynı proje içerisinde yer almak demektir.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Assembly deki tüm interfaceleri bulup uygualar.

            //modelBuilder.ApplyConfiguration(new ProductConfiguration()); // Bu kullanımda ise hangi configuration dosyalarını uygulayacağını tek tek göstermek gerekiyor.

            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 200,
                ProductId=1

            },
            new ProductFeature
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                Width =500,
                ProductId = 2

            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
