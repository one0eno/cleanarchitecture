using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var haser = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "36e1d9d5a60a47e08eabdfa751344753",
                        UserName = "jorge",
                        NormalizedUserName = "jorge",
                        Email ="admin@localhost.com",
                        NormalizedEmail = "admin@localhost.com",
                        Name = "Jorge",
                        SurName = "Arranz",
                        PasswordHash = haser.HashPassword(null,"jorge1973@")

                    },
                    new ApplicationUser
                    {
                        Id = "5c53292ac77f4f9181d41d744f9190ef",
                        UserName = "ana",
                        NormalizedUserName = "ana",
                        Email = "ana@localhost.com",
                        NormalizedEmail = "ana@localhost.com",
                        Name = "ana",
                        SurName = "ramirez",
                        PasswordHash = haser.HashPassword(null, "jorge1973@")

                    }


                );
        }
    }
}
