using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Models;

public partial class mdsDbContext : IdentityDbContext<Customer>
{
    public mdsDbContext(DbContextOptions<mdsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LAPTOP-IGU4RUNK\\SQLEXPRESS;Database=mdsapi2;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
            },
            new IdentityRole
            {
                Name = "Phar",
                NormalizedName = "PHAR",
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
            },
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);*/

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__cart__415B03D8CF0AF4A4");

            entity.ToTable("cart");

            entity.Property(e => e.CartId).HasColumnName("cartID");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__cart__customerID__5EBF139D");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.CdId).HasName("PK__cartDeta__289C55A48E433A9B");

            entity.ToTable("cartDetail");

            entity.Property(e => e.CdId).HasColumnName("cdID");
            entity.Property(e => e.CartId).HasColumnName("cartID");
            entity.Property(e => e.MedId).HasColumnName("medID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__cartDetai__cartI__60A75C0F");

            entity.HasOne(d => d.Med).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.MedId)
                .HasConstraintName("FK__cartDetai__medID__5FB337D6");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId).HasName("PK__categori__A88B4DC41691FD27");

            entity.ToTable("categories");

            entity.Property(e => e.CateId).HasColumnName("cateID");
            entity.Property(e => e.CateDesc)
                .HasMaxLength(255)
                .HasColumnName("cateDesc");
            entity.Property(e => e.CateName)
                .HasMaxLength(255)
                .HasColumnName("cateName");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__customer__B611CB9D99012085");

            entity.ToTable("customers");

            entity.Property(e => e.Id).HasColumnName("customerID");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(255)
                .HasColumnName("customerAddress");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(255)
                .HasColumnName("customerEmail");
            entity.Property(e => e.CustomerLogin)
                .HasMaxLength(255)
                .HasColumnName("customerLogin");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customerName");
            entity.Property(e => e.CustomerPassword)
                .HasMaxLength(255)
                .HasColumnName("customerPassword");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(255)
                .HasColumnName("customerPhone");
            entity.HasOne(c => c.Pharmacy)
            .WithOne(p => p.Customer)
            .HasForeignKey<Customer>(c => c.PharmacyId);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedId).HasName("PK__medicine__2D4FA91CF950ACE6");

            entity.ToTable("medicines");

            entity.Property(e => e.MedId).HasColumnName("medID");
            entity.Property(e => e.CateId).HasColumnName("cateID");
            entity.Property(e => e.MedDesc)
                .HasMaxLength(255)
                .HasColumnName("medDesc");
            entity.Property(e => e.MedDiscount)
                .HasDefaultValue(0)
                .HasColumnName("medDiscount");
            entity.Property(e => e.MedName)
                .HasMaxLength(255)
                .HasColumnName("medName");
            entity.Property(e => e.MedPicture)
                .HasMaxLength(255)
                .HasColumnName("medPicture");
            entity.Property(e => e.MedPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("medPrice");
            entity.Property(e => e.MedRemain).HasColumnName("medRemain");
            entity.Property(e => e.PharId).HasColumnName("pharID");

            entity.HasOne(d => d.Cate).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.CateId)
                .HasConstraintName("FK__medicines__cateI__5DCAEF64");

            entity.HasOne(d => d.Phar).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.PharId)
                .HasConstraintName("FK__medicines__pharI__656C112C");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__order__0809337D8B57FA0F");

            entity.ToTable("order");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.OrderDeliveredAt).HasColumnName("orderDeliveredAt");
            entity.Property(e => e.OrderPlacedAt).HasColumnName("orderPlacedAt");
            entity.Property(e => e.OsId).HasColumnName("osID");
            entity.Property(e => e.ShippingFees)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("shippingFees");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__order__customerI__628FA481");

            entity.HasOne(d => d.Os).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OsId)
                .HasConstraintName("FK__order__osID__619B8048");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__orderIte__3724BD72FB0C49AF");

            entity.ToTable("orderItem");

            entity.Property(e => e.OrderItemId).HasColumnName("orderItemID");
            entity.Property(e => e.ItemQuantity).HasColumnName("itemQuantity");
            entity.Property(e => e.MedId).HasColumnName("medID");
            entity.Property(e => e.OrderId).HasColumnName("orderID");

            entity.HasOne(d => d.Med).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.MedId)
                .HasConstraintName("FK__orderItem__medID__6477ECF3");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__orderItem__order__6383C8BA");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OsId).HasName("PK__orderSta__5241DF11B0C6ADFD");

            entity.ToTable("orderStatus");

            entity.Property(e => e.OsId).HasColumnName("osID");
            entity.Property(e => e.OsDesc)
                .HasMaxLength(255)
                .HasColumnName("osDesc");
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.PharId).HasName("PK__pharmaci__8E7B6B32784A7171");

            entity.ToTable("pharmacies");

            entity.Property(e => e.PharId).HasColumnName("pharID");
            entity.Property(e => e.PharAddress)
                .HasMaxLength(255)
                .HasColumnName("pharAddress");
            entity.Property(e => e.PharEmail)
                .HasMaxLength(255)
                .HasColumnName("pharEmail");
            entity.Property(e => e.PharName)
                .HasMaxLength(255)
                .HasColumnName("pharName");
            entity.Property(e => e.PharPhone)
                .HasMaxLength(255)
                .HasColumnName("pharPhone");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromoId).HasName("PK__promotio__E19E71D6C784FB3C");

            entity.ToTable("promotions");

            entity.Property(e => e.PromoId).HasColumnName("promoID");
            entity.Property(e => e.DiscountPercent).HasColumnName("discountPercent");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.PromoName)
                .HasMaxLength(255)
                .HasColumnName("promoName");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__reviews__2ECD6E24E98902BA");

            entity.ToTable("reviews");

            entity.Property(e => e.ReviewId).HasColumnName("reviewID");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.MedId).HasColumnName("medID");
            entity.Property(e => e.ReviewContent)
                .HasColumnType("text")
                .HasColumnName("reviewContent");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__reviews__custome__6754599E");

            entity.HasOne(d => d.Med).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.MedId)
                .HasConstraintName("FK__reviews__medID__66603565");
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
