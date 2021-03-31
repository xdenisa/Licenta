using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("MEDICAMENTE");

            builder.HasKey(m => m.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Name)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("DENUMIRE_MEDICAMENT");

            builder.Property(e => e.AdministrationMethod)
                .HasMaxLength(100)
                .HasColumnName("MOD_ADMINISTRARE");
        }
    }
}
