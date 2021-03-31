using Proiect.DataAccess.EntityFramework;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.DataAccess
{
    public class UnitOfWork
    {
        private readonly ProjectContext context;

        public UnitOfWork(ProjectContext context)
        {
            this.context = context;
        }

        private BaseRepository<Image> images;
        public BaseRepository<Image> Images => images ?? (images = new BaseRepository<Image>(context));
        private BaseRepository<Medic> medics;
        public BaseRepository<Medic> Medics => medics ?? (medics = new BaseRepository<Medic>(context));
        private BaseRepository<Medicine> medicines;
        public BaseRepository<Medicine> Medicines => medicines ?? (medicines = new BaseRepository<Medicine>(context));
        private BaseRepository<Patient> patients;
        public BaseRepository<Patient> Patients => patients ?? (patients = new BaseRepository<Patient>(context));
        private BaseRepository<Person> persons;
        public BaseRepository<Person> Persons => persons ?? (persons = new BaseRepository<Person>(context));
        private BaseRepository<Portfolio> portfolio;
        public BaseRepository<Portfolio> Portfolio => portfolio ?? (portfolio = new BaseRepository<Portfolio>(context));
        private BaseRepository<Appointment> appointments;
        public BaseRepository<Appointment> Appointments => appointments ?? (appointments = new BaseRepository<Appointment>(context));
        private BaseRepository<Result> results;
        public BaseRepository<Result> Results => results ?? (results = new BaseRepository<Result>(context));
        private BaseRepository<Specialization> specializations;
        public BaseRepository<Specialization> Specializations => specializations ?? (specializations = new BaseRepository<Specialization>(context));
        private BaseRepository<Treatment> treatments;
        public BaseRepository<Treatment> Treatments => treatments ?? (treatments= new BaseRepository<Treatment>(context));

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
