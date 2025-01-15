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
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public IList<Category> GetCategoryByUser(Guid userId)
        {

                return _dataContext.Set<Category>()
                    .Where(x => x.UserId == userId)
                    .ToList();

            
        }
    }
}
