using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mappings
{
    public class CarMapping : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars", "dbo");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.BrandId).HasColumnName("BrandId");
            builder.Property(c => c.ColorId).HasColumnName("ColorId");
            builder.Property(c => c.Name).HasColumnName("Name");
            builder.Property(c => c.Description).HasColumnName("Description");
            builder.Property(c => c.DailyPrice).HasColumnName("DailyPrice");
            builder.Property(c => c.IsAvailable).HasColumnName("IsAvailable");
        }
    }
}
