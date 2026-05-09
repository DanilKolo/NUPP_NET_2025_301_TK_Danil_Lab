using Microsoft.EntityFrameworkCore;
using HardwareStore.Infrastructure.Models;

namespace HardwareStore.Infrastructure
{
    public class HardwareContext : DbContext
    {
        public DbSet<HardwareModel> HardwareItems { get; set; }
        public DbSet<BrandModel> Brands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Вказуємо, що використовуємо SQLite і назву файлу бази
            optionsBuilder.UseSqlite("Data Source=hardware.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Налаштування через Fluent API (вимога ЛР)
            modelBuilder.Entity<BrandModel>()
                .HasMany(b => b.Items)
                .WithOne(i => i.Brand)
                .HasForeignKey(i => i.BrandId);

            // Початкові дані, щоб база не була порожньою
            modelBuilder.Entity<BrandModel>().HasData(
                new BrandModel { Id = 1, Name = "Intel", Country = "USA" },
                new BrandModel { Id = 2, Name = "NVIDIA", Country = "USA" }
            );
        }
    }
}