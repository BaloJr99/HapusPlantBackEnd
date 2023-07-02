using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HapusPlant.Data.Models;

public partial class HapusplantContext : DbContext
{
    public HapusplantContext()
    {
    }

    public HapusplantContext(DbContextOptions<HapusplantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PersonalDatum> PersonalData { get; set; }

    public virtual DbSet<SharedCollection> SharedCollections { get; set; }

    public virtual DbSet<SucculentFamily> SucculentFamilies { get; set; }

    public virtual DbSet<SucculentKind> SucculentKinds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=BALOJRLAP\\BALOJRSERVER;Initial Catalog=Hapusplant;User Id=sa;Password=Pirata99*;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonalDatum>(entity =>
        {
            entity.HasKey(e => e.IdPersonalData).HasName("PK__Personal__016FA226826B5C0E");

            entity.Property(e => e.IdPersonalData)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idPersonalData");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("photo");
        });

        modelBuilder.Entity<SharedCollection>(entity =>
        {
            entity.HasKey(e => e.IdSharedCollection).HasName("PK__SharedCo__2D18995B594F2FFE");

            entity.ToTable("SharedCollection");

            entity.Property(e => e.IdSharedCollection)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idSharedCollection");
            entity.Property(e => e.IdOriginalUser).HasColumnName("idOriginalUser");
            entity.Property(e => e.IdSharedUser).HasColumnName("idSharedUser");
            entity.Property(e => e.IsShared).HasColumnName("isShared");

            entity.HasOne(d => d.IdOriginalUserNavigation).WithMany(p => p.SharedCollectionIdOriginalUserNavigations)
                .HasForeignKey(d => d.IdOriginalUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SharedCollection_UsersOrignal");

            entity.HasOne(d => d.IdSharedUserNavigation).WithMany(p => p.SharedCollectionIdSharedUserNavigations)
                .HasForeignKey(d => d.IdSharedUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SharedCollection_UsersShared");
        });

        modelBuilder.Entity<SucculentFamily>(entity =>
        {
            entity.HasKey(e => e.IdSucculentFamily).HasName("PK__Succulen__F6E1AE9073B94568");

            entity.ToTable("SucculentFamily");

            entity.Property(e => e.IdSucculentFamily)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idSucculentFamily");
            entity.Property(e => e.Family)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("family");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.SucculentFamilies)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucculentFamily_Users");
        });

        modelBuilder.Entity<SucculentKind>(entity =>
        {
            entity.HasKey(e => e.IdSucculent).HasName("PK__Succulen__806FA98AA2FF233C");

            entity.ToTable("SucculentKind");

            entity.Property(e => e.IdSucculent)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idSucculent");
            entity.Property(e => e.DocumentsLink)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("documentsLink");
            entity.Property(e => e.HasDocuments).HasColumnName("hasDocuments");
            entity.Property(e => e.IdSucculentFamily).HasColumnName("idSucculentFamily");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.IsAlive).HasColumnName("isAlive");
            entity.Property(e => e.IsEndemic).HasColumnName("isEndemic");
            entity.Property(e => e.Kind)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("kind");
            entity.Property(e => e.PhotoLink)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("photoLink");

            entity.HasOne(d => d.IdSucculentFamilyNavigation).WithMany(p => p.SucculentKinds)
                .HasForeignKey(d => d.IdSucculentFamily)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucculentKind_SucculentFamily");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.SucculentKinds)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucculentKind_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__3717C9828A661E1A");

            entity.Property(e => e.IdUser)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("idUser");
            entity.Property(e => e.IdPersonalData).HasColumnName("idPersonalData");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Jwt)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("jwt");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.IdPersonalDataNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPersonalData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_PersonalData");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
