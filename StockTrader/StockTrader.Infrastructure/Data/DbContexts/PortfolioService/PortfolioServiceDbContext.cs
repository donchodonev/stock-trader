﻿using Microsoft.EntityFrameworkCore;

using StockTrader.Core.Entities;

namespace StockTrader.Infrastructure.Data.DbContexts.PortfolioService
{
    public class PortfolioServiceDbContext : DbContext
    {
        public PortfolioServiceDbContext(DbContextOptions<PortfolioServiceDbContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }

        public DbSet<VersionedStock> Stock { get; set; }

        public DbSet<PersonStock> PersonStock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Person

            modelBuilder
                .Entity<Person>()
                .Property(p => p.Id)
                .IsRequired()
                .UseIdentityColumn();

            modelBuilder
                .Entity<Person>()
                .Property(p => p.CreatedOn)
                .IsRequired();

            modelBuilder
                .Entity<Person>()
                .Property(p => p.UpdatedOn)
                .IsRequired();

            modelBuilder
                .Entity<Person>()
                .HasKey(p => p.Id);

            modelBuilder
                .Entity<Person>()
                .HasMany(x => x.PersonStocks)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);

            #endregion

            #region Stock

            modelBuilder
                .Entity<VersionedStock>()
                .Property(p => p.Id)
                .IsRequired()
                .UseIdentityColumn();

            modelBuilder
                .Entity<VersionedStock>()
                .Property(p => p.Ticker)
                .HasMaxLength(4)
                .IsRequired();

            modelBuilder
                .Entity<VersionedStock>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder
                .Entity<VersionedStock>()
                .Property(p => p.CreatedOn)
                .IsRequired();

            modelBuilder
                .Entity<VersionedStock>()
                .Property(p => p.UpdatedOn)
                .IsRequired();

            modelBuilder
                .Entity<VersionedStock>()
                .HasKey(p => p.Id);

            modelBuilder
                .Entity<VersionedStock>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder
                .Entity<VersionedStock>()
                .HasMany(x => x.PersonStocks)
                .WithOne(x => x.Stock)
                .HasForeignKey(x => x.StockId);

            #endregion

            #region PersonStock

            modelBuilder
                .Entity<PersonStock>()
                .Property(p => p.PersonId)
                .IsRequired();

            modelBuilder
                .Entity<PersonStock>()
                .Property(p => p.StockId)
                .IsRequired();

            modelBuilder
                .Entity<PersonStock>()
                .Property(p => p.CreatedOn)
                .IsRequired();

            modelBuilder
                .Entity<PersonStock>()
                .Property(p => p.UpdatedOn)
                .IsRequired();

            modelBuilder
                .Entity<PersonStock>()
                .HasKey(p => new { p.PersonId, p.StockId});

            modelBuilder
                .Entity<PersonStock>()
                .HasOne(x => x.Stock)
                .WithMany(x => x.PersonStocks)
                .HasForeignKey(x => x.StockId);

            modelBuilder
                .Entity<PersonStock>()
                .HasOne(x => x.Person)
                .WithMany(x => x.PersonStocks)
                .HasForeignKey(x => x.PersonId);

            #endregion
        }
    }
}