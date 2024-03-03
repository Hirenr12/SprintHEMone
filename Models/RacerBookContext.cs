using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SprintHEMone.Models;

public partial class RacerBookContext : DbContext
{
    public RacerBookContext()
    {
    }

    public RacerBookContext(DbContextOptions<RacerBookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<OrderList> OrderLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ETHAN_SWANEPOEL\\SQLEXPRESS;Database=RacerBook;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7977D135146");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("CartID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ItemId)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__Email__3B75D760");

            entity.HasOne(d => d.Item).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__ItemId__3C69FB99");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Customer__A9D105357FEC9916");

            entity.ToTable("Customer");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__727E838BE06F3BC0");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.ItemCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemDetails)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Itemname)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderList>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderLis__C3905BAF8EACA624");

            entity.ToTable("OrderList");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ItemId)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.OrderLists)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__OrderList__Email__3F466844");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderLists)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__OrderList__ItemI__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
