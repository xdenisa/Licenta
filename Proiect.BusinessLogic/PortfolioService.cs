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
    public class PortfolioService : BaseService
    {
        public PortfolioService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Result GetDocument(Guid idResult)
        {
            return unitOfWork.Results.Get()
                .First(id => id.Id == idResult);            
        }

        public Result InsertResult(Result result)
        {
            unitOfWork.Results.Insert(result);
            unitOfWork.SaveChanges();
            return result;
        }

        public Portfolio InsertPortfolio(Portfolio portfolio)
        {
            unitOfWork.Portfolio.Insert(portfolio);
            unitOfWork.SaveChanges();
            return portfolio;
        }

        public IEnumerable<Result> GetPatientPortfolio(Guid idPatient)
        {
            List<Result> results = new List<Result>();
            var patientPortfolio = unitOfWork.Portfolio.Get()
                .Where(i => i.IdPatient == idPatient)
                .ToList();
            foreach (var result in patientPortfolio)
            {
                results.Add(unitOfWork.Results.Get()
                    .First(i => i.Id == result.IdResult));
            }
            return results;
        }

        public void DeletePortfolio(Guid idResult)
        {
            var result = unitOfWork.Results.Get()
                .First(i => i.Id == idResult);
            var portfolio = unitOfWork.Portfolio.Get()
                .First(i => i.IdResult == idResult);
            unitOfWork.Results.Delete(result);
            unitOfWork.Portfolio.Delete(portfolio);
            unitOfWork.SaveChanges();
        }

        public void DeletePatientPortfolio(Guid idPatient)
        {
            var portfolio = unitOfWork.Portfolio.Get()
                .Where(p => p.IdPatient == idPatient)
                .ToList();
            foreach (var item in portfolio)
            {
                unitOfWork.Portfolio.Delete(item);
            }
        }
    }
}
