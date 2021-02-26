using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mappings
{
    public class RentalMapping : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("Rentals", "dbo");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("Id");
            builder.Property(r => r.CarId).HasColumnName("CarId");
            builder.Property(r => r.CustomerId).HasColumnName("CustomerId");
            builder.Property(r => r.RentDate).HasColumnName("RentDate");
            builder.Property(r => r.ReturnDate).HasColumnName("ReturnDate");
        }
    }
}
