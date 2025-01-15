namespace FinancasApp.Presentation.Models.Transaction
{
    public class TransactionResultConsult
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        public DateTime? Data { get; set; }
    }
}
