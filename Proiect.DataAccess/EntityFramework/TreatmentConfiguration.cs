using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> builder)
        {
            builder.ToTable("TRATAMENTE");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.IdMedicine)
                .HasColumnName("ID_MEDICAMENT");

            builder.Property(e => e.IdPatient)
                .HasColumnName("ID_PACIENT");

            builder.Property(e => e.Observations)
                .HasMaxLength(100)
                .HasColumnName("OBSERVATII");

            builder.Property(e => e.NumberOfDays)
                .HasMaxLength(100)
                .HasColumnName("SCHEMA_TRATAMENT");

            builder.HasOne(d => d.Medicine)
                .WithMany(p => p.Treatments)
                .HasForeignKey(d => d.IdMedicine)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TRATAMENTE_MEDICAMENT");

            builder.HasOne(d => d.Patient)
                .WithMany(p => p.Treatments)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TRATAMENTE_PACIENT");
        }
    }
}
