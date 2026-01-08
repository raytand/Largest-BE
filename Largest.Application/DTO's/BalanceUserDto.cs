using Largest.Domain.Enums;

namespace Largest.Application.DTO_s
{
    public class BalanceUserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public BalanceRole Role { get; set; }
    }
}
