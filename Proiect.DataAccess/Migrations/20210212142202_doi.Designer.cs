// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proiect.DataAccess.EntityFramework;

namespace Proiect.DataAccess.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20210212142202_doi")]
    partial class doi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Proiect.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<byte[]>("_Image")
                        .IsRequired()
                        .HasColumnType("image")
                        .HasColumnName("IMAGINE");

                    b.HasKey("Id");

                    b.ToTable("IMAGINI");
                });

            modelBuilder.Entity("Proiect.Entities.Medic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<int?>("IdentificationCode")
                        .HasMaxLength(10)
                        .HasColumnType("int")
                        .HasColumnName("COD_IDENTIFICARE");

                    b.Property<int?>("MedicalCollegeYear")
                        .HasMaxLength(4)
                        .HasColumnType("int")
                        .HasColumnName("COLEGIUL_MEDICILOR");

                    b.Property<string>("Abilities")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("COMPETENTE");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("DESCRIERE");

                    b.Property<Guid?>("IdImage")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_IMAGINE");

                    b.Property<Guid>("IdPerson")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_PERSOANA");

                    b.Property<Guid?>("IdSpecialization")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_SPECIALIZARE");

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(false)");

                    b.Property<string>("Hospital")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("SPITAL");

                    b.Property<string>("Education")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("STUDII");

                    b.HasKey("Id");

                    b.HasIndex("IdImage");

                    b.HasIndex("IdPerson");

                    b.HasIndex("IdSpecialization");

                    b.ToTable("MEDICI");
                });

            modelBuilder.Entity("Proiect.Entities.Medicine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("DENUMIRE_MEDICAMENT");

                    b.Property<string>("AdministrationMethod")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("MOD_ADMINISTRARE");

                    b.HasKey("Id");

                    b.ToTable("MEDICAMENTE");
                });

            modelBuilder.Entity("Proiect.Entities.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("ADRESA");

                    b.Property<Guid?>("IdImage")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_IMAGINE");

                    b.Property<Guid>("IdPerson")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_PERSOANA");

                    b.HasKey("Id");

                    b.HasIndex("IdImage");

                    b.HasIndex("IdPerson");

                    b.ToTable("PACIENTI");
                });

            modelBuilder.Entity("Proiect.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("date")
                        .HasColumnName("DATA_NASTERII");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NUME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("PAROLA");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PRENUME");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SEX");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("TELEFON");

                    b.HasKey("Id");

                    b.ToTable("PERSOANE");
                });

            modelBuilder.Entity("Proiect.Entities.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<Guid>("IdMedic")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_MEDIC");

                    b.Property<Guid>("IdPatient")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_PACIENT");

                    b.Property<Guid>("IdResult")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_REZULTAT");

                    b.HasKey("Id");

                    b.HasIndex("IdMedic");

                    b.HasIndex("IdPatient");

                    b.HasIndex("IdResult");

                    b.ToTable("PORTOFOLIU");
                });

            modelBuilder.Entity("Proiect.Entities.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime")
                        .HasColumnName("DATA_PROGRAMARII");

                    b.Property<string>("Details")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("DETALII");

                    b.Property<Guid>("IdMedic")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_MEDIC");

                    b.Property<Guid>("IdPatient")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_PACIENT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TIP");

                    b.HasKey("Id");

                    b.HasIndex("IdMedic");

                    b.HasIndex("IdPatient");

                    b.ToTable("PROGRAMARI");
                });

            modelBuilder.Entity("Proiect.Entities.Result", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime")
                        .HasColumnName("DATA_EMITERII");

                    b.Property<byte[]>("Document")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("DOCUMENT");

                    b.Property<byte[]>("IdImage")
                        .IsRequired()
                        .HasColumnType("varbinary(16)")
                        .HasColumnName("ID_IMAGINE");

                    b.Property<string>("Observations")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("OBSERVATII");

                    b.HasKey("Id");

                    b.HasIndex("IdImage");

                    b.ToTable("REZULTATE");
                });

            modelBuilder.Entity("Proiect.Entities.Specializations", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("DENUMIRE_SPECIALIZARE");

                    b.HasKey("Id");

                    b.ToTable("SPECIALIZARI");
                });

            modelBuilder.Entity("Proiect.Entities.Treatment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<Guid>("IdMedicine")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_MEDICAMENT");

                    b.Property<Guid>("IdPatient")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_PACIENT");

                    b.Property<string>("Observations")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("OBSERVATII");

                    b.Property<int>("NumberOfDays")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("SCHEMA_TRATAMENT");

                    b.HasKey("Id");

                    b.HasIndex("IdMedicine");

                    b.HasIndex("IdPatient");

                    b.ToTable("TRATAMENTE");
                });

            modelBuilder.Entity("Proiect.Entities.Medic", b =>
                {
                    b.HasOne("Proiect.Entities.Image", "Image")
                        .WithMany("Medics")
                        .HasForeignKey("IdImage")
                        .HasConstraintName("FK_MEDICI1");

                    b.HasOne("Proiect.Entities.Person", "Person")
                        .WithMany("Medics")
                        .HasForeignKey("IdPerson")
                        .HasConstraintName("FK_MEDICI_PERSOANE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect.Entities.Specializations", "Specializations")
                        .WithMany("Medics")
                        .HasForeignKey("IdSpecialization")
                        .HasConstraintName("FK_MEDICI2");

                    b.Navigation("Image");

                    b.Navigation("Person");

                    b.Navigation("Specializations");
                });

            modelBuilder.Entity("Proiect.Entities.Patient", b =>
                {
                    b.HasOne("Proiect.Entities.Image", "Image")
                        .WithMany("Patients")
                        .HasForeignKey("IdImage")
                        .HasConstraintName("FK_PACIENTI");

                    b.HasOne("Proiect.Entities.Person", "Person")
                        .WithMany("Patients")
                        .HasForeignKey("IdPerson")
                        .HasConstraintName("FK_PACIENTI_PERSOANE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Proiect.Entities.Portfolio", b =>
                {
                    b.HasOne("Proiect.Entities.Medic", "Medic")
                        .WithMany("Portfolio")
                        .HasForeignKey("IdMedic")
                        .HasConstraintName("FK__PORTOFOLIU_MEDIC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect.Entities.Patient", "Patient")
                        .WithMany("Portfolio")
                        .HasForeignKey("IdPatient")
                        .HasConstraintName("FK_PORTOFOLIU_PACIENT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect.Entities.Result", "Results")
                        .WithMany("Portfolio")
                        .HasForeignKey("IdResult")
                        .HasConstraintName("FK__PORTOFOLIU_REZULTAT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medic");

                    b.Navigation("Patient");

                    b.Navigation("Results");
                });

            modelBuilder.Entity("Proiect.Entities.Appointment", b =>
                {
                    b.HasOne("Proiect.Entities.Medic", "Medic")
                        .WithMany("Appointments")
                        .HasForeignKey("IdMedic")
                        .HasConstraintName("FK__PROGRAMARI_MEDIC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect.Entities.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("IdPatient")
                        .HasConstraintName("FK_PROGRAMARI_PACIENT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medic");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Proiect.Entities.Result", b =>
                {
                    b.HasOne("Proiect.Entities.Image", "Image")
                        .WithMany("Results")
                        .HasForeignKey("IdImage")
                        .HasConstraintName("FK_REZULTATE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Proiect.Entities.Treatment", b =>
                {
                    b.HasOne("Proiect.Entities.Medicine", "Medicine")
                        .WithMany("Treatments")
                        .HasForeignKey("IdMedicine")
                        .HasConstraintName("FK__TRATAMENTE_MEDICAMENT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect.Entities.Patient", "Patient")
                        .WithMany("Treatments")
                        .HasForeignKey("IdPatient")
                        .HasConstraintName("FK_TRATAMENTE_PACIENT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Proiect.Entities.Image", b =>
                {
                    b.Navigation("Medics");

                    b.Navigation("Patients");

                    b.Navigation("Results");
                });

            modelBuilder.Entity("Proiect.Entities.Medic", b =>
                {
                    b.Navigation("Portfolio");

                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Proiect.Entities.Medicine", b =>
                {
                    b.Navigation("Treatments");
                });

            modelBuilder.Entity("Proiect.Entities.Patient", b =>
                {
                    b.Navigation("Portfolio");

                    b.Navigation("Appointments");

                    b.Navigation("Treatments");
                });

            modelBuilder.Entity("Proiect.Entities.Person", b =>
                {
                    b.Navigation("Medics");

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("Proiect.Entities.Result", b =>
                {
                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Proiect.Entities.Specializations", b =>
                {
                    b.Navigation("Medics");
                });
#pragma warning restore 612, 618
        }
    }
}
