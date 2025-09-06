using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC.Integration.Domain.Entities
{
    [Table("SalesOrders", Schema = "dbo")]
    public class SalesOrder :EntityBase
    {
       public SalesOrder()
        {
            SalesOrderItems = new HashSet<SalesOrderItems>();
            Customer = new Customer();
        }
        [Required]
        [MaxLength(100)]
        public string ExternalOrderID { get; set; } = string.Empty;

        [Required]
        public DateTime ProcessingDate { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerID { get; set; }

        [Required]
        [MaxLength(500)]
        public string? ShipmentAddress { get; set; }
        [NotMapped]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<SalesOrderItems> SalesOrderItems { get; set; }
    }
}