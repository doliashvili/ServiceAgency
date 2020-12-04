using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceAgency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceAgency.Infrastructure.Data.ModelConfigs
{
    public class CarModelConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.VinCode).HasMaxLength(30);
            builder.Property(x => x.ModelEng).HasMaxLength(20);
            builder.Property(x => x.ModelGeo).HasMaxLength(20);
            builder.Property(x => x.TransportNumber).HasMaxLength(30);
            builder.Property(x => x.MarkEng).HasMaxLength(20);
            builder.Property(x => x.MarkGeo).HasMaxLength(20);
            builder.HasIndex(x => new { x.CreatedDate, x.ModelEng, x.MarkEng, x.VinCode });

            builder.HasMany(x => x.Owners).WithMany(x=> x.Cars);
            builder.HasOne(x => x.Color);
            builder.HasOne(x => x.Fuel);
        }
    }
}
