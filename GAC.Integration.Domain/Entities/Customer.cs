using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GAC.Integration.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    namespace GAC.Integration.Domain.Entities
    {
        [Table("Customers", Schema = "dbo")]
        public class Customer
        {
            [Key]
            [Required]
            [StringLength(50)]
            public string CustomerID { get; set; }

            [Required]
            [StringLength(100)]
            public string ExternalCustomerIdentifier { get; set; }

            [Required]
            [StringLength(200)]
            public string Name { get; set; }

            [StringLength(300)]
            public string? Address { get; set; }

            [Required]
            public DateTime CreatedAt { get; set; }

            [Required]
            public string CreatedBy { get; set; }
        }
    }

}
