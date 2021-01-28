using GoshenJimenez.Mercadia3.Web.Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoshenJimenez.Mercadia3.Web.Infrastructure.Domain
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
           : base(options)
                { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Tag> tags = new List<Tag>()
            {
                new Tag()
                {
                    Id = Guid.Parse("32febb22-f596-4b1e-b0a8-b11ad54be200"),
                    Name = "Tag 1"
                },
                new Tag()
                {
                    Id = Guid.Parse("32febb22-f596-4b1e-b0a8-b11ad54be201"),
                    Name = "Tag 2"
                },
                new Tag()
                {
                    Id = Guid.Parse("32febb22-f596-4b1e-b0a8-b11ad54be203"),
                    Name = "Tag 3"
                }
            };

            modelBuilder.Entity<Tag>()
               .HasData(tags);



        }
    }
}
