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
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

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

            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.Parse("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"),
                    Name = "Category 1"
                },
                new Category()
                {
                    Id = Guid.Parse("02b92adc-c0dc-4a87-9aa8-df5d90cf2a01"),
                    Name = "Category 2"
                },
                new Category()
                {
                    Id = Guid.Parse("02b92adc-c0dc-4a87-9aa8-df5d90cf2a02"),
                    Name = "Category 3"
                }
            };

            modelBuilder.Entity<Category>()
               .HasData(categories);

            List<Product> product = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.Parse("254a99bb-bee5-43e0-80dc-e76622110500"),
                    Name = "Product 1 Soap",
                    TagLine = "Pag di ka pumuti, iitim ka.",
                    Description = "Palmolive Naturals White with Milk Whitening Bar Soap 80g 2+1 Value Pack",
                    CategoryId = Guid.Parse("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00")
                },
                new Product()
                {
                    Id = Guid.Parse("254a99bb-bee5-43e0-80dc-e76622110501"),
                    Name = "Product 2 Cellphone",
                    TagLine = "Pang ML",
                    Description = "Global Version Xiaomi Redmi 9A Smartphones 2GB RAM 32GB ROM 6.53″ Intelligent Face Unlock Xiaomi Mall",
                    CategoryId = Guid.Parse("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00")
                },
                new Product()
                {
                    Id = Guid.Parse("254a99bb-bee5-43e0-80dc-e76622110502"),
                    Name = "Product 3 Bread",
                    TagLine = "Busog ka agad",
                    Description = "Marby Mini Monay 250g",
                    CategoryId = Guid.Parse("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00")
                }
            };

            modelBuilder.Entity<Product>()
               .HasData(product);

            List<User> users = new List<User>(){
                new User()
                {
                    Id = Guid.Parse("628019e2-665b-4a4c-ae23-4490ffd4fa00"),
                    FirstName = "Jace",
                    LastName = "Beleren",
                    EmailAddress = "jbeleren@mailinator.com",
                    Role = Role.Admin,
                },
                new User()
                {
                    Id = Guid.Parse("628019e2-665b-4a4c-ae23-4490ffd4fa01"),
                    FirstName = "Liliana",
                    LastName = "Vess",
                    EmailAddress = "lvess@mailinator.com",
                    Role = Role.Customer,
                },
                new User()
                {
                    Id = Guid.Parse("628019e2-665b-4a4c-ae23-4490ffd4fa02"),
                    FirstName = "Chandra",
                    LastName = "Nalaar",
                    EmailAddress = "cnalaar@mailinator.com",
                    Role = Role.Customer,
                }
            };

            modelBuilder.Entity<User>()
                .HasData(users);

            List<UserLogin> userLogins = new List<UserLogin>();

            foreach(User user in users)
            {
                var salt = BCrypt.BCryptHelper.GenerateSalt();

                userLogins.Add(new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Key = "Password",
                    Value = BCrypt.BCryptHelper.HashPassword("1234", salt),
                    Type = LoginType.Email
                });

                userLogins.Add(new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Key = "LoginStatus",
                    Value = "Active", //Active, LockedOut
                    Type = LoginType.General
                });

                userLogins.Add(new UserLogin()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Key = "LoginRetries",
                    Value = "0",
                    Type = LoginType.Email
                });
            };

            modelBuilder.Entity<UserLogin>()
                .HasData(userLogins);
        }
    }
}
