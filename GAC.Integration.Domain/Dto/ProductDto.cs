using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GAC.Integration.Domain.Dto
{
    public class ProductDto
    {
        public string ProductCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        [JsonIgnore]
        public virtual DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public virtual string CreatedBy { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual string UpdatedBy { get; set; } = string.Empty;
    }
}
