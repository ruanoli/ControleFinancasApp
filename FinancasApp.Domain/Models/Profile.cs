using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Models
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public IList<User> Users { get; set; }
    }
}
