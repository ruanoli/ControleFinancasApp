﻿using FinancasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        List<Transaction> GetTransactionByDateAndUser(DateTime initDate, DateTime finalDate, Guid userId);
    }
}
