﻿using MODELS.Models;
using Microsoft.EntityFrameworkCore;
using Project;

namespace MODELS.Models
{
    public class DBContext : DbContext
    {
        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-L1QQEC3\\SQLEXPRESS;Database=SecondDB;Integrated Security=True;",
                    b => b.MigrationsAssembly("MODELS"));
            }
        }
    }
}