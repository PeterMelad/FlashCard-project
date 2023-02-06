using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF1
{
    public class ApplicationDbContext:IdentityDbContext
    {
         IConfiguration Configuration { get; set; }
        public ApplicationDbContext(IConfiguration configuration,DbContextOptions<ApplicationDbContext> options):base(options)
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
        }
        public DbSet<FlashCard>FlashCards { get; set; }
        public DbSet<ModelQuestion> question { get; set; }
    }
}
