using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Locations")]
    public class Destination
    {
        [Key, Column("LocationID")]
        public int DestinationId { get; set; }
        [Required, Column("LocationName")]
        public string Name { get; set; }
        public string Country { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Column(TypeName="image")]
        public byte[] Photo { get; set; }
        // [ForeignKey("LocationId")]
        public List<Lodging> Lodgings { get; set; }
    }
}
