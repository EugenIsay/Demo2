using System;
using System.Collections.Generic;
using Demo2.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo2.Context;

public partial class IsajkinContext : DbContext
{
    public IsajkinContext()
    {
    }

    public IsajkinContext(DbContextOptions<IsajkinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Clientservice> Clientservices { get; set; }

    public virtual DbSet<Documentbyservice> Documentbyservices { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productphoto> Productphotos { get; set; }

    public virtual DbSet<Productsale> Productsales { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Servicephoto> Servicephotos { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=45.67.56.214;Port=5454;Database=isajkin;Username=isajkin;password=hqD9f9EZ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Gendercode)
                .HasMaxLength(1)
                .HasColumnName("gendercode");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Photopath)
                .HasMaxLength(1000)
                .HasColumnName("photopath");
            entity.Property(e => e.Registrationdate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("registrationdate");

            entity.HasOne(d => d.GendercodeNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Gendercode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_client_gender");

            entity.HasMany(d => d.Tags).WithMany(p => p.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "Tagofclient",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("Tagid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("tagofclient_tag_fk"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("Clientid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tagofclient_client"),
                    j =>
                    {
                        j.HasKey("Clientid", "Tagid").HasName("tagofclient_pkey");
                        j.ToTable("tagofclient", "task16-09");
                        j.IndexerProperty<int>("Clientid").HasColumnName("clientid");
                        j.IndexerProperty<int>("Tagid").HasColumnName("tagid");
                    });
        });

        modelBuilder.Entity<Clientservice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clientservice_pkey");

            entity.ToTable("clientservice", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Serviceid).HasColumnName("serviceid");
            entity.Property(e => e.Starttime)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("starttime");

            entity.HasOne(d => d.Client).WithMany(p => p.Clientservices)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_clientservice_client");

            entity.HasOne(d => d.Service).WithMany(p => p.Clientservices)
                .HasForeignKey(d => d.Serviceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_clientservice_service");
        });

        modelBuilder.Entity<Documentbyservice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("documentbyservice_pkey");

            entity.ToTable("documentbyservice", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clientserviceid).HasColumnName("clientserviceid");
            entity.Property(e => e.Documentpath)
                .HasMaxLength(1000)
                .HasColumnName("documentpath");

            entity.HasOne(d => d.Clientservice).WithMany(p => p.Documentbyservices)
                .HasForeignKey(d => d.Clientserviceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_documentbyservice_clientservice");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("gender_pkey");

            entity.ToTable("gender", "task16-09");

            entity.Property(e => e.Code)
                .HasMaxLength(1)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manufacturer_pkey");

            entity.ToTable("manufacturer", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasPrecision(19, 4)
                .HasColumnName("cost");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Mainimagepath)
                .HasMaxLength(1000)
                .HasColumnName("mainimagepath");
            entity.Property(e => e.Manufacturerid).HasColumnName("manufacturerid");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacturerid)
                .HasConstraintName("fk_product_manufacturer");

            entity.HasMany(d => d.Attachedproducts).WithMany(p => p.Mainproducts)
                .UsingEntity<Dictionary<string, object>>(
                    "Attachedproduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("Attachedproductid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_attachedproduct_product1"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Mainproductid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_attachedproduct_product"),
                    j =>
                    {
                        j.HasKey("Mainproductid", "Attachedproductid").HasName("attachedproduct_pkey");
                        j.ToTable("attachedproduct", "task16-09");
                        j.IndexerProperty<int>("Mainproductid").HasColumnName("mainproductid");
                        j.IndexerProperty<int>("Attachedproductid").HasColumnName("attachedproductid");
                    });

            entity.HasMany(d => d.Mainproducts).WithMany(p => p.Attachedproducts)
                .UsingEntity<Dictionary<string, object>>(
                    "Attachedproduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("Mainproductid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_attachedproduct_product"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Attachedproductid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_attachedproduct_product1"),
                    j =>
                    {
                        j.HasKey("Mainproductid", "Attachedproductid").HasName("attachedproduct_pkey");
                        j.ToTable("attachedproduct", "task16-09");
                        j.IndexerProperty<int>("Mainproductid").HasColumnName("mainproductid");
                        j.IndexerProperty<int>("Attachedproductid").HasColumnName("attachedproductid");
                    });
        });

        modelBuilder.Entity<Productphoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productphoto_pkey");

            entity.ToTable("productphoto", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Photopath)
                .HasMaxLength(1000)
                .HasColumnName("photopath");
            entity.Property(e => e.Productid).HasColumnName("productid");

            entity.HasOne(d => d.Product).WithMany(p => p.Productphotos)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_productphoto_product");
        });

        modelBuilder.Entity<Productsale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productsale_pkey");

            entity.ToTable("productsale", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clientserviceid).HasColumnName("clientserviceid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Saledate)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("saledate");

            entity.HasOne(d => d.Clientservice).WithMany(p => p.Productsales)
                .HasForeignKey(d => d.Clientserviceid)
                .HasConstraintName("fk_productsale_clientservice");

            entity.HasOne(d => d.Product).WithMany(p => p.Productsales)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_productsale_product");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("service", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasPrecision(19, 4)
                .HasColumnName("cost");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Durationinseconds).HasColumnName("durationinseconds");
            entity.Property(e => e.Mainimagepath)
                .HasMaxLength(1000)
                .HasColumnName("mainimagepath");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Servicephoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("servicephoto_pkey");

            entity.ToTable("servicephoto", "task16-09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Photopath)
                .HasMaxLength(1000)
                .HasColumnName("photopath");
            entity.Property(e => e.Serviceid).HasColumnName("serviceid");

            entity.HasOne(d => d.Service).WithMany(p => p.Servicephotos)
                .HasForeignKey(d => d.Serviceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_servicephoto_service");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pkey");

            entity.ToTable("tag", "task16-09");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("color");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
