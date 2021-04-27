using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proiect.BusinessLogic.Base;
using Proiect.DataAccess;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proiect.BusinessLogic
{
    public class MedicService : BaseService
    {
        private readonly IMapper mapper;
        private readonly ImageService imageService;
        private readonly AppointmentService appointmentService;
        public MedicService(UnitOfWork unitOfWork,
            AppointmentService appointmentService,
            IMapper mapper,
            ImageService imageService) : base(unitOfWork)
        {
            this.mapper = mapper;
            this.imageService = imageService;
            this.appointmentService = appointmentService;
        }

        public IEnumerable<Medic> GetMedics()
        {
            var medics= unitOfWork.Medics.Get()
                .Include(p => p.Person)
                .Include(s => s.Specialization)
                .Include(i => i.Image)
                .ToList();

            foreach (var medic in medics)
            {
                medic.Person.Password = null;
            }
            return medics;
        }

        public string GetMedicPersonId(Guid idMedic)
        {
            return GetMedicByID(idMedic.ToString()).IdPerson.ToString();
        }

        public IEnumerable<Medic> GetMedicsBySpecialization(string specialization)
        {
            var medics= unitOfWork.Medics.Get()
                .Where(s => s.Specialization.Name == specialization)
                .Include(p => p.Person)
                .Include(s => s.Specialization)
                .Include(i => i.Image)
                .ToList();

            foreach (var medic in medics)
            {
                medic.Person.Password = null;
            }
            return medics;
        }

        public void ApproveMedic(Guid idMedic)
        {
            var medic = GetMedicByID(idMedic.ToString());
            if (medic != null)
            {
                medic.IsApproved = bool.TrueString;
                unitOfWork.Medics.Update(medic);
                unitOfWork.SaveChanges();
            }
        }

        public Medic GetMedicByID(string id)
        {
            return unitOfWork.Medics.Get()
                .Include(p => p.Person)
                .Include(s => s.Specialization)
                .Include(i => i.Image)
                .FirstOrDefault(i => i.Id.ToString() == id);
        }

        //public void UpdateProfilePicture(Medic medic, Image image)
        //{
        //    if (medic.Image != null)
        //    {
        //        imageService.DeleteImage(medic.Image);
        //    }
        //    medic.IdImage = image.Id;
        //    medic.Image = image;
        //}

        public Medic GetMedicForLogin(string email)
        {
            return unitOfWork.Medics.Get()
                .Include(p => p.Person)
                .First(e => e.Person.Email == email);
        }

        public void UpdateMedic(Medic medic,Image image)
        {
            var medicContext = mapper.Map<Medic>(medic);
            if(image!=null)
            {
                if(medic.Image!=null)
                {
                    imageService.DeleteImage(medic.Image);
                }
                medicContext.IdImage = image.Id;
                medicContext.Image = image;
            }
            unitOfWork.Medics.Update(medicContext);
            unitOfWork.SaveChanges();
        }

        public void DeleteMedicProfile(Guid idMedic)
        {
            var medicToDelete = GetMedicByID(idMedic.ToString());
            var personToDelete = medicToDelete.Person;
            var imageToDelete = medicToDelete.Image;
            unitOfWork.Medics.Delete(medicToDelete);
            unitOfWork.Persons.Delete(personToDelete);
            if (imageToDelete != null)
            { unitOfWork.Images.Delete(imageToDelete); }
            unitOfWork.SaveChanges();
        }

        public Medic GetMedicByPersonID(Guid id)
        {
            return unitOfWork.Medics.Get()
                .Include(p => p.Person)
                .Include(s => s.Specialization)
                .Include(i => i.Image)
                .FirstOrDefault(p => p.IdPerson == id);
        }

        public bool IsMedicPatient(Guid idPatient, string idPerson)
        {
            var medic = GetMedicByPersonID(Guid.Parse(idPerson));
            var appointments = appointmentService.GetAppointmentsByMedicId(medic.Id).Where(ap=>ap.AppointmentDate<DateTime.Now);
            foreach (var appointment in appointments)
            {
                if (appointment.IdMedic == medic.Id && appointment.IdPatient == idPatient)
                    return true;
            }

            return false;
        }

        public void ChangePassword(Guid id, string hashPassword)
        {
            using (var newContext = new DataAccess.EntityFramework.ProjectContext())
            {
                var medic = newContext.Medics.Include(p => p.Person).First(i => i.Id == id);
                medic.Person.Password = hashPassword;
                newContext.Update(medic);
                newContext.SaveChanges();
            }
        }
    }
}
