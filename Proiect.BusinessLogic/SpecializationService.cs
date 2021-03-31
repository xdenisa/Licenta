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
    public class SpecializationService : BaseService
    {
        public SpecializationService(UnitOfWork unitOfWork) : base(unitOfWork) {  }

        public List<Specialization> GetSpecializations()
        {
            return unitOfWork.Specializations.Get()
                .ToList();
        }

        public IEnumerable<Specialization> GetSpecializationsWithImage()
        {
            return unitOfWork.Specializations.Get()
                .Include(i => i.Image)
                .ToList();
        }

        public void AddSpecialization(Specialization specialization)
        {
            unitOfWork.Specializations.Insert(specialization);
            unitOfWork.SaveChanges();
        }
    }
}
