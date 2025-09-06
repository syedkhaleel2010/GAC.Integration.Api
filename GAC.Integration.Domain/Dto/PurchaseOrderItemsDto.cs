using GAC.Integration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GAC.Integration.Domain.Dto
{
    public class PurchaseOrderItemsDto
    {
        public Guid ID { get; set; }
        public Guid PurchaseOrderID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public virtual PurchaseOrderDto PurchaseOrder { get; set; } = new PurchaseOrderDto();
        [JsonIgnore]
        public virtual ProductDto Product { get; set; } = new ProductDto();
    }
}