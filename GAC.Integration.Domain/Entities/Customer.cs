using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


    namespace GAC.Integration.Domain.Entities
    {
        [Table("Customers", Schema = "dbo")]
        public class Customer : EntityBase
        {
            public Customer()
                {
                  PurchaseOrders = new HashSet<PurchaseOrder>();
                  SalesOrders = new HashSet<SalesOrder>();    
                }
           
            [Required]
            [StringLength(100)]
            public string ExternalCustomerIdentifier { get; set; }

            [Required]
            [StringLength(200)]
            public string Name { get; set; }

            [StringLength(300)]
            public string? Address { get; set; }
            public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
            public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }

}
