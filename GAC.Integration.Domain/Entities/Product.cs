using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC.Integration.Domain.Entities
{
    [Table("Products", Schema = "dbo")]
    public class Product : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string ProductCode { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        public decimal? Length { get; set; } 
        public decimal? Width { get; set; } 
        public decimal? Height { get; set; } 
        public decimal? Weight { get; set; } 
    }
}