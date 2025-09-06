using GAC.Integration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace  GAC.Integration.Domain.Dto
{
    public class PurchaseOrderDto
    {
        public Guid ID { get; set; }

        public string ExternalOrderID { get; set; } = string.Empty;
        public DateTime ProcessingDate { get; set; }
        public Guid CustomerID { get; set; }
        [JsonIgnore]
        public CustomerDto Customer { get; set; } = new CustomerDto();
        public  IEnumerable<PurchaseOrderItemsDto> PurchaseOrderLineDto { get; set; } = new List<PurchaseOrderItemsDto>();
    }
}
