using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC.Integration.Domain.Entities
{
    [Table("SalesOrderItems", Schema = "dbo")]
    public class SalesOrderItems : EntityBase
    {
        public SalesOrderItems()
        {
            SalesOrder = new SalesOrder();
            Product = new Product();
        }

        [Required]
        [ForeignKey("SalesOrder")]
        public Guid SalesOrderID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }
        [NotMapped]
        public virtual SalesOrder SalesOrder { get; set; }
        [NotMapped]
        public virtual Product Product { get; set; }
    }
}