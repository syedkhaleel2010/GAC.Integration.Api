using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GAC.Integration.Domain.Dto
{
    public class SalesOrderItemsDto
    {
        public SalesOrderItemsDto()
        {
            Product = new ProductDto();
            SalesOrder = new SalesOrderDto();
        }
        public Guid ID { get; set; }
        public Guid SalesOrderID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public  SalesOrderDto SalesOrder { get; set; }
        [JsonIgnore]
        public virtual ProductDto Product { get; set; }
    }
}