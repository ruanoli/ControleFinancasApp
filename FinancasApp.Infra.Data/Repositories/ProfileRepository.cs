using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Models;
using FinancasApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
 
        public Profile GetProfileByName(string name)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Profile>().Where(x => x.Name == name).FirstOrDefault();
            }
        }
    }
}
