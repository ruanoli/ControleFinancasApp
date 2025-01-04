using FinancasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Services
{
    public interface IUserService
    {
        void CreateAccount(User user);
        User Authenticate(string email, string password);
    }
}
