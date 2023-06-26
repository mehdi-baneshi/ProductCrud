using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCrud.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }
    }
}
