using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.ToTable("SPECIALIZARI");

            builder.HasKey(s => s.Id);
            
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("DENUMIRE_SPECIALIZARE");

            builder.Property(i => i.IdImage)
                .HasColumnName("IdImagine");

            builder.HasOne(i => i.Image)
                .WithOne(s => s.Specialization)
                .HasForeignKey<Specialization>(i => i.IdImage);
        }
    }
}
