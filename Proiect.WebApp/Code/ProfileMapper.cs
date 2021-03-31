using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Entities;
using Proiect.WebApp.Models;
using Proiect.WebApp.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.Code
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Patient, PatientViewModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(i => i.Id))
                .ForMember(i => i.IdPerson, opt => opt.MapFrom(i => i.IdPerson))
                .ForMember(i => i.IdImage, opt => opt.MapFrom(i => i.IdImage))
                .ForMember(i => i.LastName, opt => opt.MapFrom(i => i.Person.LastName))
                .ForMember(i => i.FirstName, opt => opt.MapFrom(i => i.Person.FirstName))
                 .ForMember(i => i.Email, opt => opt.MapFrom(i => i.Person.Email))
                   .ForMember(i => i.BirthDay, opt => opt.MapFrom(i => i.Person.BirthDay))
                  .ForMember(i => i.Sex, opt => opt.MapFrom(i => i.Person.Sex))
                  .ForMember(i => i.PhoneNumber, opt => opt.MapFrom(i => i.Person.PhoneNumber))
                   .ForMember(i => i.Address, opt => opt.MapFrom(i => i.Address))
                   .ForMember(i => i.Image, opt => opt.MapFrom(i => i.Image))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Patient, PatientProfileViewModel>()
                .ForPath(i => i.Patient.Id, opt => opt.MapFrom(i => i.Id))
                .ForPath(i => i.Patient.IdPerson, opt => opt.MapFrom(i => i.IdPerson))
                .ForPath(i => i.Patient.IdImage, opt => opt.MapFrom(i => i.IdImage))
                .ForPath(i => i.Patient.LastName, opt => opt.MapFrom(i => i.Person.LastName))
                .ForPath(i => i.Patient.FirstName, opt => opt.MapFrom(i => i.Person.FirstName))
                 .ForPath(i => i.Patient.Email, opt => opt.MapFrom(i => i.Person.Email))
                   .ForPath(i => i.Patient.BirthDay, opt => opt.MapFrom(i => i.Person.BirthDay))
                  .ForPath(i => i.Patient.Sex, opt => opt.MapFrom(i => i.Person.Sex))
                  .ForPath(i => i.Patient.PhoneNumber, opt => opt.MapFrom(i => i.Person.PhoneNumber))
                   .ForPath(i => i.Patient.Address, opt => opt.MapFrom(i => i.Address))
                   .ForPath(i => i.Patient.Image, opt => opt.MapFrom(i => i.Image))
                 .ForMember(i => i.Password, opt => opt.MapFrom(i => i.Person.Password))
                  .ForMember(i => i.IsAdmin, opt => opt.MapFrom(i => i.Person.IsAdmin))
                  .ForMember(i => i.Image, opt => opt.Ignore());
            

            CreateMap<PatientProfileViewModel, Patient>()
                .ForMember(i => i.Id, opt => opt.MapFrom(i => i.Patient.Id))
                .ForMember(i => i.IdPerson, opt => opt.MapFrom(i => i.Patient.IdPerson))
                .ForMember(i => i.IdImage, opt => opt.MapFrom(i => i.Patient.IdImage))
                .ForPath(i => i.Person.LastName, opt => opt.MapFrom(i => i.Patient.LastName))
                .ForPath(i => i.Person.Id, opt => opt.MapFrom(i => i.Patient.IdPerson))
                .ForPath(i => i.Person.FirstName, opt => opt.MapFrom(i => i.Patient.FirstName))
                 .ForPath(i => i.Person.Email, opt => opt.MapFrom(i => i.Patient.Email))
                 .ForPath(i => i.Person.Password, opt => opt.MapFrom(i => i.Password))
                  .ForPath(i => i.Person.BirthDay, opt => opt.MapFrom(i => i.Patient.BirthDay))
                  .ForPath(i => i.Person.Sex, opt => opt.MapFrom(i => i.Patient.Sex))
                  .ForPath(i => i.Person.PhoneNumber, opt => opt.MapFrom(i => i.Patient.PhoneNumber))
                  .ForPath(i => i.Person.IsAdmin, opt => opt.MapFrom(i => i.IsAdmin))
                  .ForMember(i => i.Address, opt => opt.MapFrom(i => i.Patient.Address))
                  .ForMember(i => i.Image, opt => opt.MapFrom(i => i.Patient.Image))
                  .ForAllOtherMembers(op => op.Ignore());

            CreateMap<Medic, Medic>()
                .ForMember(sp => sp.Specialization, opt => opt.Ignore())
                .ForMember(im => im.Image, opt => opt.Ignore());

            CreateMap<Person, Person>();

            CreateMap<Patient, Patient>();

            CreateMap<RegisterViewModel, Person>()
                .ForMember(t => t.Email, opt => opt.MapFrom(n => n.Email))
                .ForMember(t => t.Password, opt => opt.MapFrom(n => n.Password))
                .ForMember(t => t.LastName, opt => opt.MapFrom(n => n.LastName))
                .ForMember(t => t.FirstName, opt => opt.MapFrom(n => n.FirstName))
                .ForMember(t => t.BirthDay, opt => opt.MapFrom(n => n.BirthDay))
                .ForMember(t => t.PhoneNumber, opt => opt.MapFrom(n => n.PhoneNumber))
                .ForMember(t => t.Sex, opt => opt.MapFrom(s => s.Sex.ToString()))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Specialization, SelectListItem>()
                .ForMember(t => t.Text, opt => opt.MapFrom(n => n.Name))
                .ForMember(v => v.Value, opt => opt.MapFrom(i => i.Id));
        }
    }
}
