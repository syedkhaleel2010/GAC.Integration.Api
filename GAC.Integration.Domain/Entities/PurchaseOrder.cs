
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC.Integration.Domain.Entities
{
    [Table("PurchaseOrders", Schema = "dbo")]
    public class PurchaseOrder : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string ExternalOrderID { get; set; } = string.Empty;
        [Required]
        public DateTime ProcessingDate { get; set; }
        [Required]
        public string CustomerID { get; set; } = string.Empty;
    }
}