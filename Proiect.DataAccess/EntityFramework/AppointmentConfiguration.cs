using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("PROGRAMARI");

            builder.HasKey(p => p.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("DATA_PROGRAMARII");

            builder.Property(e => e.Details)
                .HasMaxLength(150)
                .HasColumnName("DETALII");

           builder.Property(e => e.IdMedic)
                .HasColumnName("ID_MEDIC");

            builder.Property(e => e.IdPatient)
                .HasColumnName("ID_PACIENT");

            builder.Property(e => e.Type)
                .IsRequired()
                .HasColumnName("TIP");

            builder.HasOne(d => d.Medic)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdMedic)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PROGRAMARI_MEDIC");

            builder.HasOne(d => d.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdPatient)
                .HasConstraintName("FK_PROGRAMARI_PACIENT");
        }
    }
}
