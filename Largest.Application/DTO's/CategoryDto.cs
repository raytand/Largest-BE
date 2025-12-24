using System.ComponentModel.DataAnnotations;
namespace Largest.Application.DTO_s
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool IsIncome { get; set; } = false;
    }
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsIncome { get; set; } = false;
    }
}
