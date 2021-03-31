using Proiect.BusinessLogic.Base;
using Proiect.DataAccess;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proiect.BusinessLogic
{
    public class MedicineService : BaseService
    {
        public MedicineService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Medicine InsertMedicine(Medicine medicine)
        {
            return unitOfWork.Medicines.Insert(medicine);
        }

        public void InsertTreatment(Treatment treatment)
        {
            unitOfWork.Treatments.Insert(treatment);
            unitOfWork.SaveChanges();
        }

        public IEnumerable<TreatmentMedicine> GetMedicines(Guid idPatient)
        {
            List<TreatmentMedicine> treatments = new List<TreatmentMedicine>();

            var pacientTreatments = unitOfWork.Treatments.Get()
                .Where(i => i.IdPatient == idPatient)
                .ToList();
            foreach (var treatment in pacientTreatments)
            {
                var medicine = unitOfWork.Medicines.Get()
                    .First(i => i.Id == treatment.IdMedicine);

                treatments.Add(new TreatmentMedicine
                {
                    Treatment = treatment,
                    Medicine = medicine
                });
            }
            return treatments;
        }

        public void DeleteMedicine(Guid idTreatment)
        {
            var treatment = unitOfWork.Treatments.Get()
                 .First(i => i.Id == idTreatment);
            var medicine = unitOfWork.Medicines.Get()
                .First(i => i.Id == treatment.IdMedicine);

            unitOfWork.Treatments.Delete(treatment);
            unitOfWork.Medicines.Delete(medicine);
            unitOfWork.SaveChanges();
        }

        public Medicine GetMedicineById(Guid idMedicine)
        {
            return unitOfWork.Medicines.Get()
                .First(i => i.Id == idMedicine);
        }

        public Treatment GetTreatmentByMedicineId(Guid idMedicine)
        {
            return unitOfWork.Treatments.Get()
                .First(i => i.IdMedicine == idMedicine);
        }

        public void EditMedicine(Medicine medicine, Treatment treatment)
        {
            unitOfWork.Medicines.Update(medicine);
            unitOfWork.Treatments.Update(treatment);
            unitOfWork.SaveChanges();
        }
    }

    
}
