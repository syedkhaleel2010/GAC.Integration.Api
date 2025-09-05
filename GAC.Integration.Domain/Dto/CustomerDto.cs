using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GAC.Integration.Domain.Dto
{
    public class CustomerDto
    {
        public string ID { get; set; } = string.Empty;
        public string ExternalCustomerIdentifier { get; set; }= string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }

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
