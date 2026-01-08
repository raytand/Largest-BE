namespace Largest.Domain.Entities
{
    public class ExportJob
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public int UserId { get; set; }
        public DateTime From { get; set; } 
        public DateTime To { get; set; }
        public string? FileName { get; set; } 
    }
}   
