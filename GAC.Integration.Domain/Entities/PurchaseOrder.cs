
using GAC.Integration.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC.Integration.Domain.Entities
{
    [Table("PurchaseOrders", Schema = "dbo")]
    public class PurchaseOrder : EntityBase
    {

        public PurchaseOrder()
        {
            PurchaseOrderItems = new HashSet<PurchaseOrderItems>();
           Customer = new Customer();
        }

        [Required]
        [StringLength(50)]
        public string ExternalOrderID { get; set; } = string.Empty;
        [Required]
        public DateTime ProcessingDate { get; set; }
        [Required]
        public Guid CustomerID { get; set; }
        [NotMapped]
        public virtual Customer Customer { get; set; } 

        public virtual ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItems>();
    }
}