using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class MedicConfiguration : IEntityTypeConfiguration<Medic>
    {
        public void Configure(EntityTypeBuilder<Medic> builder)
        {
            builder.ToTable("MEDICI");

            builder.HasKey(m => m.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(a => a.IsApproved)
                .HasDefaultValueSql("false");

            builder.Property(e => e.IdentificationCode)
                .HasMaxLength(10)
                .HasColumnName("COD_IDENTIFICARE");

            builder.Property(e => e.MedicalCollegeYear)
                .HasMaxLength(4)
                .HasColumnName("COLEGIUL_MEDICILOR");

            builder.Property(e => e.Abilities)
                .HasMaxLength(150)
                .HasColumnName("COMPETENTE");

            builder.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("DESCRIERE");

            builder.Property(e => e.IdImage)
                .HasColumnName("ID_IMAGINE");

            builder.Property(e => e.IdPerson)
                .HasColumnName("ID_PERSOANA");

            builder.Property(e => e.IdSpecialization)
                .HasColumnName("ID_SPECIALIZARE");

            builder.Property(e => e.Hospital)
                .HasMaxLength(50)
                .HasColumnName("SPITAL");

            builder.Property(e => e.Education)
                .HasMaxLength(100)
                .HasColumnName("STUDII");

            builder.HasOne(d => d.Image)
                .WithMany(p => p.Medics)
                .HasForeignKey(d => d.IdImage)
                .HasConstraintName("FK_MEDICI1");

           builder.HasOne(d => d.Person)
                .WithMany(p => p.Medics)
                .HasForeignKey(d => d.IdPerson)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MEDICI_PERSOANE");

            builder.HasOne(d => d.Specialization)
                .WithMany(p => p.Medics)
                .HasForeignKey(d => d.IdSpecialization)
                .HasConstraintName("FK_MEDICI2");
        }
    }
}
