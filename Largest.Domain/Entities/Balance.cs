namespace Largest.Domain.Entities
{
    public class Balance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "UAH";
        public bool IsActive { get; set; } = true;

        public User User { get; set; } = null!;

        public List<Transaction> Transactions { get; set; } = new();
    }
}
