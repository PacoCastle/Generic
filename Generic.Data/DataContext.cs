using Generic.Core;
using Generic.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Generic.Data
{
    public class DataContext : DbContext

    {
        public DbSet<Client> Client { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
