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
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                 new IdentityUserRole<string>
                 {
                     RoleId = "795da95b-59e7-4439-96b5-ca032e8a8b4f",
                     UserId = "36e1d9d5a60a47e08eabdfa751344753"

                 },
                   new IdentityUserRole<string>
                   {
                       RoleId = "621d3873-8288-4bc4-bde0-13b92ec18f10",
                       UserId = "5c53292ac77f4f9181d41d744f9190ef"

                   }
                );
        }
    }
}
