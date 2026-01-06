using Largest.Domain.Enums;

namespace Largest.Domain.Entities
{
    public class BalanceUser
    {
        public int Id { get; set; }

        public int BalanceId { get; set; } 
        public Balance Balance { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public BalanceRole Role { get; set; }
    }
}
