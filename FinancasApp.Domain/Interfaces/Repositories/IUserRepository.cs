using FinancasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User? GetUserByEmail(string email);
        User? GetUserByEmailAndPassword(string email, string password);
    }
}
