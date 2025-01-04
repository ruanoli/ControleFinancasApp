using FinancasApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime DataTransaction { get; set; }
        public decimal Value { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }

        public User? User { get; set; }
        public Category? Category { get; set; }
    }
}
