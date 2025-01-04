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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User? GetUserByEmail(string email)
        {
            using (var datacContext = new DataContext())
            {
                return datacContext.Set<User>().Where(x => x.Email == email)
                      .Include(x => x.Profile)
                      .FirstOrDefault();
            }
        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            using (var datacContext = new DataContext())
            {
                return datacContext.Set<User>().Where(x => 
                    x.Email == email &&
                    x.Password == password)
                    .Include(x => x.Profile)
                    .FirstOrDefault();
                        }
        }

    }
}
