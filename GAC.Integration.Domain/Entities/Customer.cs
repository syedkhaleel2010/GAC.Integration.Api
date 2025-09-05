using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GAC.Integration.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Principal;

    namespace GAC.Integration.Domain.Entities
    {
        [Table("Customers", Schema = "dbo")]
        public class Customer : EntityBase
        {
            [Key]
            [Required]
            [StringLength(50)]
            public string ID { get; set; }

            [Required]
            [StringLength(100)]
            public string ExternalCustomerIdentifier { get; set; }

            [Required]
            [StringLength(200)]
            public string Name { get; set; }

            [StringLength(300)]
            public string? Address { get; set; }
        }
    }

}
