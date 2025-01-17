using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Models;
using FinancasApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly DataContext _dataContext;

        public TransactionRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Transaction> GetTransactionByDateAndUser(DateTime initDate, DateTime finalDate, Guid userId)
        {

            var transaction = _dataContext.Set<Transaction>()
                    .Include(x => x.Category)
                    .Where(x => x.UserId == userId && (x.DataTransaction >= initDate && x.DataTransaction <= finalDate))
                    .OrderBy(x => x.DataTransaction)
                    .ToList();
            return transaction;
        }
    }
}
