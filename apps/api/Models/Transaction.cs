namespace Api.Models
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OccurredAt { get; set; }
        public TransactionType Type { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
