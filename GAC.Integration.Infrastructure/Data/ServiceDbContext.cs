using GAC.Integration.Domain;
using GAC.Integration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GAC.Integration.Infrastructure.Data
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderItems> PurchaseOrderItems { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItems> SalesOrderItems { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                if (entityType.ClrType.IsSubclassOf(typeof(EntityBase<Guid>)))
                    SetTrackingColumnsMapping(modelBuilder, entityType.ClrType);

            //modelBuilder.Entity<Customer>()
            //            .HasMany(b => b.PurchaseOrders)
            //            .WithOne(t => t.Customer)
            //            .HasForeignKey(x => x.CustomerID)
            //            .HasPrincipalKey(p => p.ID)
            //            .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Product>().
            //             HasMany(b => b.PurchaseOrderItems)
            //            .WithOne(t => t.Product)
            //            .HasForeignKey(x => x.ProductID)
            //            .HasPrincipalKey(p => p.ID)
            //            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseOrder>()
                        .HasMany(b => b.PurchaseOrderItems)
                        .WithOne(t => t.PurchaseOrder)
                        .HasForeignKey(x => x.PurchaseOrderID)
                        .HasPrincipalKey(p => p.ID)
                        .OnDelete(DeleteBehavior.Cascade);



            //modelBuilder.Entity<Customer>()
            //            .HasMany(b => b.SalesOrders)
            //            .WithOne(t => t.Customer)
            //            .HasForeignKey(x => x.CustomerID)
            //            .HasPrincipalKey(p => p.ID)
            //            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalesOrder>()
                        .HasMany(b => b.SalesOrderItems)
                        .WithOne(t => t.SalesOrder)
                        .HasForeignKey(x => x.SalesOrderID)
                        .HasPrincipalKey(p => p.ID)
                        .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        private void SetTrackingColumnsMapping(ModelBuilder modelBuilder, Type entityType)
        {
                modelBuilder.Entity(entityType).Property(nameof(EntityBase.ID)).IsRequired();
        }
    }
}