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
    public class SalesOrderDto
    {
        public SalesOrderDto()
        {
            Customer = new CustomerDto();
            SalesOrderItems = new List<SalesOrderItemsDto>();
        }

        public Guid ID { get; set; }
        public string ExternalOrderID { get; set; } = string.Empty;
        public DateTime ProcessingDate { get; set; }
        public Guid CustomerID { get; set; } 
        public string? ShipmentAddress { get; set; }
        [JsonIgnore]
        public  CustomerDto Customer { get; set; }
        public IEnumerable<SalesOrderItemsDto> SalesOrderItems { get; set; }
    }
}
