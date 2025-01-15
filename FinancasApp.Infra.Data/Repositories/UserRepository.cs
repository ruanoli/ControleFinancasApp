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
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public User? GetUserByEmail(string email)
        {

            return _dataContext.Set<User>().Where(x => x.Email == email)
                  .Include(x => x.Profile)
                  .FirstOrDefault();

        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {

            return _dataContext.Set<User>().Where(x =>
                x.Email == email &&
                x.Password == password)
                .Include(x => x.Profile)
                .FirstOrDefault();
        }


    }
}
