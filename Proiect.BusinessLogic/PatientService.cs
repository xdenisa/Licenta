using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proiect.BusinessLogic.Base;
using Proiect.DataAccess;
using Proiect.DataAccess.EntityFramework;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proiect.BusinessLogic
{
    public class PatientService : BaseService
    {
        private readonly IMapper mapper;
        private readonly ImageService imageService;
        public PatientService(UnitOfWork unitOfWork,
            IMapper mapper,
            ImageService imageService) : base(unitOfWork)
        {
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public Patient GetPatientByPersonId(Guid id)
        {
            var patient= unitOfWork.Patients.Get()
                .Include(p => p.Person)
                .Include(i => i.Image)
                .FirstOrDefault(p => p.IdPerson == id);

            patient.Person.Password = null;
            return patient;
        }

        public Patient GetPatientById(Guid id)
        {
            return unitOfWork.Patients.Get()
                .Include(p => p.Person)
                .Include(i => i.Image)
                .FirstOrDefault(i => i.Id == id);
        }

        //public void UpdateProfilePicture(Patient patient, Image image)
        //{
        //    if (patient.Image != null)
        //    {
                
        //    }
        //    patient.IdImage = image.Id;
        //    patient.Image = image;
        //}

        public void ChangePassword(Guid idPatient,string newPassword)
        { 
            using(var newContext=new ProjectContext())
            {
                var patient = newContext.Patients.Include(p=>p.Person).First(i => i.Id == idPatient);
                patient.Person.Password = newPassword;
                newContext.Update(patient);
                newContext.SaveChanges();
            }
        }

        public void UpdatePatient(Patient patient, Image image)
        {
            var patientContext = mapper.Map<Patient>(patient);
            if (image != null)
            {
                if (patient.Image != null)
                {
                    imageService.DeleteImage(patient.Image);
                }
                patientContext.IdImage = image.Id;
                patientContext.Image = image;
            }

            unitOfWork.Patients.Update(patientContext);
            unitOfWork.SaveChanges();
        }

        public Patient GetPatientForLogin(string email)
        {
            return unitOfWork.Patients.Get()
                .Include(p => p.Person)
                .First(e => e.Person.Email == email);
        }

        public void DeletePatientProfile(Guid idPatient)
        {
            var patientToDelete = GetPatientById(idPatient);
            var personToDelete = patientToDelete.Person;
            var imageToDelete = patientToDelete.Image;
            unitOfWork.Patients.Delete(patientToDelete);
            unitOfWork.Persons.Delete(personToDelete);
            if (imageToDelete != null)
            {
                unitOfWork.Images.Delete(imageToDelete);
            }
            unitOfWork.SaveChanges();
        }

        public string GetPatientPersonId(Guid idPatient)
        {
            return GetPatientById(idPatient).IdPerson.ToString();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            var patients = unitOfWork.Patients.Get()
                .Include(p => p.Person)
                .Include(i=>i.Image)
                .ToList();
            foreach (var patient in patients)
            {
                patient.Person.Password = null;
            }

            return patients;
        }
    }
}
