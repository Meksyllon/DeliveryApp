using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.DataAccess.MSSQL.Configurations
{
    internal class DistrictConfiguration : IEntityTypeConfiguration<DistrictEntity>
    {
        public void Configure(EntityTypeBuilder<DistrictEntity> builder)
        {
            builder.HasKey(d => d.Id);

            builder
                .HasMany(d => d.Packages)
                .WithOne(p => p.District)
                .HasForeignKey(p => p.DistrictId);
        }
    }
}
