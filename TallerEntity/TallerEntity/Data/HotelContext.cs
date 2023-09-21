using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TallerEntity.Models;

namespace TallerEntity.Data;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerCompanion> CustomerCompanions { get; set; }

    public virtual DbSet<CustomerReservationsView> CustomerReservationsViews { get; set; }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=163.178.173.130;Database=HotelParaiso;user id=basesdedatos;password=rpbases.2022;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => new { e.idEmail, e.idTelephone });
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerID).HasName("PK__Customer__A4AE64B8FB5B0777");

            entity.HasIndex(e => e.Cedula, "UQ__Customer__B4ADFE385C67632D").IsUnique();

            entity.Property(e => e.CustomerID).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CustomerCompanion>(entity =>
        {
            entity.HasKey(e => e.CustomerID).HasName("PK__Customer__A4AE64B8CDA75541");

            entity.ToTable("CustomerCompanion");

            entity.HasIndex(e => e.Cedula, "UQ__Customer__B4ADFE38AFE53E33").IsUnique();

            entity.Property(e => e.CustomerID).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Companion).WithMany(p => p.CustomerCompanions)
                .HasForeignKey(d => d.CompanionID)
                .HasConstraintName("FK__CustomerC__Compa__5165187F");
        });

        modelBuilder.Entity<CustomerReservationsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CustomerReservationsView");

            entity.Property(e => e.CodeRoom)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NameCustomer)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Email>(entity =>
        {
            entity.HasKey(e => new { e.idEmail, e.email1 });

            entity.ToTable("Email");

            entity.Property(e => e.email1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faciliti__3214EC070B037FA9");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(160)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationID).HasName("PK__Reservat__B7EE5F044FF43F6D");

            entity.Property(e => e.ReservationID).ValueGeneratedNever();
            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");
            entity.Property(e => e.ReservationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Custo__4CA06362");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__RoomI__4D94879B");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomID).HasName("PK__Rooms__328639192685F256");

            entity.Property(e => e.RoomID).ValueGeneratedNever();
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
