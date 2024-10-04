using Microsoft.EntityFrameworkCore;
using seminar1._1.Models;

namespace seminar1._1.Data
{
    public class StorageContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =. \\DESKTOP-6B2PDIN; Initial Catalog = Products; Trusted_Connection=True; TrustServerCertificate=True").UseLazyLoadingProxies().LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(pq => pq.Id)
                      .HasName("product_group_pk");

                entity.ToTable("category");

                entity.Property(pq => pq.Name)
                      .HasColumnName("name")
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id)
                       .HasName("product_pk");

                entity.Property(p => p.Name)
                      .HasColumnName("name")
                      .HasMaxLength(255);

                entity.HasOne(p => p.ProductGroup)
                      .WithMany(p => p.Products)
                      .HasForeignKey(p => p.ProductGroupId);
            });
            
            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(s => s.Id)
                       .HasName("product_pk");
               
                entity.HasOne(s => s.Product)
                      .WithMany(p => p.Storages)
                      .HasForeignKey(p => p.ProductId);
            });
        }
    }
}
