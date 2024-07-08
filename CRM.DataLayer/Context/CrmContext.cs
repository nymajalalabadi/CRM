using CRM.Domain.Entities.Account;
using CRM.Domain.Entities.Companies;
using CRM.Domain.Entities.Events;
using CRM.Domain.Entities.Leads;
using CRM.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Context
{
    public class CrmContext : DbContext
    {
        public CrmContext(DbContextOptions<CrmContext> options) : base(options)
        {
                
        }

        #region Db Set

        public DbSet<User> Users { get; set; }

        public DbSet<Marketer> Marketers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<OrderSelectedMarketer> orderSelectedMarketers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Lead> Leads { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Fluent

            #region Order Selected Marketer

            modelBuilder.Entity<OrderSelectedMarketer>()
                .HasOne(a => a.Order)
                .WithOne(a => a.OrderSelectedMarketer)
                .HasForeignKey<OrderSelectedMarketer>(a => a.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderSelectedMarketer>()
                .HasOne(a => a.Marketer)
                .WithMany(a => a.OrderSelectedMarketers)
                .HasForeignKey(a => a.MarketerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderSelectedMarketer>()
                .HasOne(a => a.ModifyUser)
                .WithMany(a => a.OrderSelectedMarketers)
                .HasForeignKey(a => a.ModifyUserId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Leads

            modelBuilder.Entity<Lead>()
                .HasOne(a => a.Owner)
                .WithMany(a => a.CollectionLeadOwner)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lead>()
                .HasOne(a => a.CreatedBy)
                .WithMany(a => a.CollectionLeadCreatedBy)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion


            #endregion
        }

    }
}
