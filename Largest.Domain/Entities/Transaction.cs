namespace Largest.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; } = null!;

        public int? BalanceId { get; set; }
        public Balance? Balance { get; set; } = null!;
        public bool IsIncome { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
