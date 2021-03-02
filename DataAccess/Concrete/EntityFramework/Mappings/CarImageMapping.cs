using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mappings
{
    public class CarImageMapping : IEntityTypeConfiguration<CarImage>
    {
        public void Configure(EntityTypeBuilder<CarImage> builder)
        {
            builder.ToTable("CarImages", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.CarId).HasColumnName("CarId");
            builder.Property(x => x.ImageName).HasColumnName("Name");
            builder.Property(x => x.UploadDate).HasColumnName("Date");
            builder.Property(x => x.ImagePath).HasColumnName("Path");
        }
    }
}
