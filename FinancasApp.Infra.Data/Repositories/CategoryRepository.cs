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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public IList<Category> GetCategoryByUser(Guid userId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Category>()
                    .Where(x => x.UserId == userId)
                    .ToList();

            }
        }
    }
}
