using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsPro.Models;

namespace SportsPro.DataLayer
{
    // A class to configure the many-to-many relationship between products and customers tables via a Registration table as a linking table
    internal class RegistrationConfig : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> entity)
        {
            //modelBuilder.Entity<Registration>()
                entity.HasKey(reg => new { reg.CustomerID, reg.ProductID });

            //modelBuilder.Entity<Registration>()
                entity.HasOne(reg => reg.Product)
                .WithMany(p => p.Registrations)
                .HasForeignKey(reg => reg.ProductID);

            //modelBuilder.Entity<Registration>()
               entity.HasOne(reg => reg.Customer)
               .WithMany(c => c.Registrations)
               .HasForeignKey(reg => reg.CustomerID);
        }
    }
}
