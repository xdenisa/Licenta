using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("IMAGINI");

            builder.HasKey(i => i.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("ID")
                   .HasDefaultValueSql("(newid())");

            builder.Property(i => i._Image)
                .IsRequired()
                .HasColumnType("image")
                    .HasColumnName("IMAGINE");

            builder.Property(i => i.MimeType)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasColumnName("Descriere");

            builder.Property(m => m.MimeType)
                .HasColumnName("FormatImagine");

        }
    }
}
