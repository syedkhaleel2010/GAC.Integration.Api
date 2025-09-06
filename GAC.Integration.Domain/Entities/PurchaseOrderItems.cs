using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC.Integration.Domain.Entities
{
    [Table("PurchaseOrderItems",Schema ="dbo")]
    public class PurchaseOrderItems : EntityBase
    {
        public PurchaseOrderItems()
        {
            PurchaseOrder = new PurchaseOrder();
            Product = new Product();
        }

        [Required]
        [ForeignKey("PurchaseOrder")]
        public Guid PurchaseOrderID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }
        [NotMapped]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        [NotMapped]
        public virtual Product Product { get; set; } 
    }
}