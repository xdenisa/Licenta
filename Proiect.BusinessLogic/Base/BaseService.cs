using Proiect.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Proiect.BusinessLogic.Base
{
    public class BaseService
    {
        protected readonly UnitOfWork unitOfWork;

        public BaseService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected TResult ExecuteInTransaction<TResult>(Func<UnitOfWork, TResult> func)
        {
            using (var transactionScope = new TransactionScope())
            {
                var result = func(unitOfWork);

                transactionScope.Complete();

                return result;
            }
        }

        protected void ExecuteInTransaction<TResult>(Action<UnitOfWork> action)
        {
            using (var transactionScope = new TransactionScope())
            {
                action(unitOfWork);

                transactionScope.Complete();
            }
        }
    }
}
