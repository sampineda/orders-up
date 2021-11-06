using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersUpBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.DataContext
{
    public class ProductMap: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Code).HasColumnType("varchar(50)").HasMaxLength(100).IsRequired();


            builder.HasOne(e => e.Business)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.BusinessId);
        } 
    }

    public class InventoryMap : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Color).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Quantity).HasColumnType("varchar(50)").HasMaxLength(100).IsRequired();


            builder.HasOne(e => e.Business)
                .WithMany(e => e.Inventories)
                .HasForeignKey(e => e.BusinessId);

            builder.HasOne(e => e.Product)
                .WithMany(e => e.Inventories)
                .HasForeignKey(e => e.ProductId);
        }
    }

    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.DueDate).HasColumnType("date");
            builder.Property(e => e.ElaborationMinutes).HasColumnType("float");
            builder.Property(e => e.Done).HasColumnType("bit").IsRequired();

            builder.HasOne(e => e.Client)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.ClientId);

            builder.HasOne(e => e.Business)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.BusinessId);
        }
    }

    public class BusinessMap : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.ToTable("Businesses", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.RTN).HasColumnType("varchar(50)").IsRequired();
        }
    }

    public class MachinesMap : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.ToTable("Machines", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Heads).HasColumnType("int");

            builder.HasOne(e => e.Business)
                .WithMany(e => e.Machines)
                .HasForeignKey(e => e.BusinessId);
        }
    }

    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.Property(e => e.FullName).HasColumnType("varchar(100)").IsRequired();
            
            builder.HasOne(e => e.Business)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.BusinessId);
        }
    }

    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Title).HasColumnType("varchar(100)").IsRequired();
        }
    }

    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Title).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.End).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.StartStr).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.EndStr).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Start).HasColumnType("datetime").IsRequired();

            builder.HasOne(e => e.Order)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.OrderId);
        }
    }

    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.PhoneNumber).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar(100)").IsRequired();
        }
    }

    public class LogoMap : IEntityTypeConfiguration<Logo>
    {
        public void Configure(EntityTypeBuilder<Logo> builder)
        {
            builder.ToTable("Logos", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Location).HasColumnType("varchar(max)");
        }
    }

    public class DetailMap : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder.ToTable("Details", "dbo");
            builder.HasKey(q => q.Id);
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Quantity).HasColumnType("int").IsRequired();
            builder.Property(e => e.Price).HasColumnType("float");
            builder.Property(e => e.Stitches).HasColumnType("int").IsRequired();

            builder.HasOne(e => e.Logo)
                .WithMany(e => e.Details)
                .HasForeignKey(e => e.LogoId);

            builder.HasOne(e => e.Inventory)
                .WithMany(e => e.Details)
                .HasForeignKey(e => e.InventoryId);

            builder.HasOne(e => e.Order)
                .WithMany(e => e.Details)
                .HasForeignKey(e => e.OrderId);
        }
    }
}
