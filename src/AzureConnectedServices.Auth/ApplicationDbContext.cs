using AzureConnectedServices.Auth.Entity;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzureConnectedServices.Auth
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .HasData(new
                    {
                        Id = 1,
                        Name = "William Jones",
                        PhoneNumber = "555-1212",
                        AddressId = 1
                    },
                    new
                    {
                        Id = 2,
                        Name = "Jake Smith",
                        PhoneNumber = "(718) 555-1212",
                        AddressId = 2
                    },
                    new
                    {
                        Id = 3,
                        Name = "Amy Davidson",
                        PhoneNumber = "(212) 555-1212",
                        AddressId = 3
                    });
        }
    }
}
