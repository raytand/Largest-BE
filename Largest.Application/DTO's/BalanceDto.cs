using System.ComponentModel.DataAnnotations;

namespace Largest.Application.DTO_s
{
    public class BalanceCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal InitialAmount { get; set; }
        [Required]
        public string Currency { get; set; } = "UAH";

        public List<TransactionDto> Transactions { get; set; } = new();
    }
    public class BalanceUpdateDto
    {
        [Required]
        public int BalanceId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Currency { get; set; } = "UAH";
    }
    public class BalanceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "UAH";
    }

}
