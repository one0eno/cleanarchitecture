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
    public class RoleConfituration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole {
                    Id = "795da95b-59e7-4439-96b5-ca032e8a8b4f",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",

                },
                  new IdentityRole
                  {
                      Id = "621d3873-8288-4bc4-bde0-13b92ec18f10",
                      Name = "Operator",
                      NormalizedName = "OPERATOR",

                  }



                );
        }
    }
}
