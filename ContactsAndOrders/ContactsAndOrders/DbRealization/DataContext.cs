using ContactBook_TwoWindows.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactBook_TwoWindows.DbRealization
{
    public class DataContext : DbContext
    {
        private readonly string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=ContactsAndOrdersDb;Trusted_Connection=True;";

        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Order>? Orders { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}