using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ISH.Data
{
    public class BaseEntity
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
