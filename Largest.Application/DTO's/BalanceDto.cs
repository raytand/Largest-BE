using System.ComponentModel.DataAnnotations;
using Largest.Domain.Entities;
using Largest.Domain.Enums;

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

        public BalanceRole Role { get; set; }

        public List<BalanceUserDto> Users { get; set; } = new List<BalanceUserDto>();
    }

    public class ShareBalanceDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public BalanceRole Role { get; set; }
    }
}
