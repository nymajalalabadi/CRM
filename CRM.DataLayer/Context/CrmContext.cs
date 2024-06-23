using CRM.Domain.Entities.Account;
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

        public DbSet<User> Users { get; set; }

        public DbSet<Marketer> Marketers { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}
