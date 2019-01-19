using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;


namespace SamuraiApp.Data
{
   public class SamuraiContext : DbContext
    {
        //public SamuraiContext(DbContextOptions<SamuraiContext> options) :base(options)
        //{

        //}

        public static readonly LoggerFactory MyConsoleLoggerFactory
            = new LoggerFactory(new[] {
                new ConsoleLoggerProvider((category, level) 
                    => category ==DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information, true)});

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyConsoleLoggerFactory)
                .UseSqlServer(
                 "Server=(localdb)\\mssqllocaldb;Database=SamuraiAppDataCore;Trusted_Connection=True;MultipleActiveResultSets=true")
                 .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.SamuraiId, s.BattleId });
        }
    }
}
