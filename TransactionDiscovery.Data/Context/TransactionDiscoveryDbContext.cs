using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using TransactionDiscovery.Data.Models;

namespace TransactionDiscovery.Data.Context
{
    public class TransactionDiscoveryDbContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<DiscoveryPatch> SyncPatchs { get; set; }

        public TransactionDiscoveryDbContext(DbContextOptions<TransactionDiscoveryDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }
        }
    }
}
