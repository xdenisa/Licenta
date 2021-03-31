using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("PACIENTI");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("ADRESA");

            builder.Property(e => e.IdImage)
                .HasColumnName("ID_IMAGINE");

            builder.Property(e => e.IdPerson)
                .HasColumnName("ID_PERSOANA");

            builder.HasOne(d => d.Image)
                .WithMany(p => p.Patients)
                .HasForeignKey(d => d.IdImage)
                .HasConstraintName("FK_PACIENTI");

            builder.HasOne(d => d.Person)
                .WithMany(p => p.Patients)
                .HasForeignKey(d => d.IdPerson)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PACIENTI_PERSOANE");
        }
    }
}
