using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework
{
   public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }
        IConfiguration Configuration { get; set; }
        public ApplicationDbContext(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Configuration = configuration;

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MyConn"));
            // optionsBuilder.UseSqlServer("Server=EGYPTDBSERVER\\MSSQLSERVER2017;Database=Peter_db;user id=dbuser2; password=1234; MultipleActiveResultSets=true");
        }
        public DbSet<FlashCard> FlashCards { get; set; }
        public DbSet<ModelQuestion> question { get; set; }
    }
}
