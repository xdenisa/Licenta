using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.ToTable("REZULTATE");

            builder.HasKey(r => r.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.DateOfIssue)
                .HasColumnType("datetime")
                .HasColumnName("DATA_EMITERII");

            builder.Property(e => e.Document)
                .HasColumnName("DOCUMENT");

            builder.Property(e => e.IdImage)
                .HasColumnName("ID_IMAGINE");

            builder.Property(m => m.MimeType)
                .HasColumnName("MimeType");

            builder.Property(e => e.Observations)
                .HasMaxLength(150)
                .HasColumnName("OBSERVATII");

            builder.HasOne(d => d.Image)
                .WithMany(p => p.Results)
                .HasForeignKey(d => d.IdImage)
                .HasConstraintName("FK_REZULTATE");
        }
    }
}
