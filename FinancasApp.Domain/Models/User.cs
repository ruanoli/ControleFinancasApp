using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid ProfileId { get; set; }

        public Profile? Profile { get; set; }     
        public IList<Transaction>? Transactions { get; set; }     
        public IList<Category>? Categories { get; set; }     
    }
}
