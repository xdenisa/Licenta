using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.ToTable("PORTOFOLIU");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.IdMedic)
                .IsRequired(false)
                .HasColumnName("ID_MEDIC");

            builder.Property(e => e.IdPatient)
                .HasColumnName("ID_PACIENT");

            builder.Property(e => e.IdResult)
                .HasColumnName("ID_REZULTAT");

            builder.HasOne(d => d.Medic)
                .WithMany(p => p.Portfolio)
                .HasForeignKey(d => d.IdMedic)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PORTOFOLIU_MEDIC");

            builder.HasOne(d => d.Patient)
                .WithMany(p => p.Portfolio)
                .HasForeignKey(d => d.IdPatient)
                .HasConstraintName("FK_PORTOFOLIU_PACIENT");

            builder.HasOne(d => d.Results)
                .WithMany(p => p.Portfolio)
                .HasForeignKey(d => d.IdResult)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PORTOFOLIU_REZULTAT");
        }
    }
}
