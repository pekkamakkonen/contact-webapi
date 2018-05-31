using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsWebApi.Models
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Contacts");
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
