namespace Largest.Domain.Entities
{
    public class Balance
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "UAH";
        public bool IsActive { get; set; } = true;

        public List<BalanceUser> BalanceUsers { get; set; } = new();
        public List<Transaction> Transactions { get; set; } = new();
    }
}
