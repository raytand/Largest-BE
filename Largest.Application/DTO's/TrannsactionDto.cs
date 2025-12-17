using System.ComponentModel.DataAnnotations;
namespace Largest.Application.DTO_s
{
    public class TransactionCreateDto
    {
        [Required]
        public int BalanceId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public bool IsIncome { get; set; } = false;
        [MaxLength(150)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? BalanceName { get; set; }
    }


}
