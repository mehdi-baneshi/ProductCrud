using ProductCrud.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCrud.Domain.Entities
{
    public class Product : BaseDomainEntity
    {
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ProduceDate { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string ManufacturePhone { get; set; }
        [Column(TypeName = "varchar(254)")]
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
    }
}
