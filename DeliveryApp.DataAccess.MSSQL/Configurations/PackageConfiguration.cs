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
    internal class PackageConfiguration : IEntityTypeConfiguration<PackageEntity>
    {
        public void Configure(EntityTypeBuilder<PackageEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.District)
                .WithMany(d => d.Packages)
                .HasForeignKey(p => p.DistrictId);
        }
    }
}
