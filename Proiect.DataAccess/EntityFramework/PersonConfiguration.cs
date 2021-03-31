using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proiect.Entities;
using Proiect.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess.EntityFramework
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("PERSOANE");

            builder.HasKey(p => p.Id);

            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("(newid())");

            builder.Property(a => a.IsAdmin)
                .HasColumnName("IsAdmin")
                .HasDefaultValueSql("false");

            builder.Property(e => e.BirthDay)
                .HasColumnType("date")
                .HasColumnName("DATA_NASTERII");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("EMAIL");

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("NUME");

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("PAROLA");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("PRENUME");

            builder.Property(e => e.Sex)
                .HasColumnName("SEX");

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("TELEFON");

            builder.HasData(new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin1@admin.com",
                Password = "admin!",
                BirthDay = DateTime.Now,
                Sex = Gender.Feminin.ToString(),
                PhoneNumber = "0712345678",
                IsAdmin=true
            }); 

        }
    }
}
