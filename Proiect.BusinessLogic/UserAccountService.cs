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
    public class UserAccountService : BaseService
    {
        private readonly PatientService patientService;
        private readonly MedicService medicService;
        public UserAccountService(UnitOfWork unitOfWork,
            PatientService patientService,
            MedicService medicService) : base(unitOfWork)
        {
            this.patientService = patientService;
            this.medicService = medicService;
        }

        public Person RegisterNewUser(Person user, string table)
        {
            return ExecuteInTransaction(uow =>
            {
                var persoana = uow.Persons.Insert(user);
                if (table == "Patient")
                {
                    uow.Patients.Insert(new Patient { Person = persoana, IdPerson = persoana.Id });
                }
                else
                {
                    uow.Medics.Insert(new Medic { Person = persoana, IdPerson = persoana.Id });
                }

                uow.SaveChanges();

                return user;
            });
        }

        public bool CanRegister(Person user)
        {
            try
            {
                unitOfWork.Persons.Get().First(e => e.Email == user.Email);
                return false;
            }
            catch
            {
                return true;
            }
        }

        public Patient LoginPatient(string email)
        {
            return ExecuteInTransaction(uow =>
            {
                try
                {
                    return patientService.GetPatientForLogin(email);
                }
                catch
                {
                    return null;
                }
            });
        }

        public Medic LoginMedic(string email)
        {
            return ExecuteInTransaction(uow =>
            {
                try
                {
                    return medicService.GetMedicForLogin(email);
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}
