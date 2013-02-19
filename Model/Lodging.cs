using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Lodging
    {
        public int LodgingId { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public bool IsResort { get; set; }
        public Destination Destination { get; set; }
        public decimal MilesFromNearestAirport { get; set; }
        public List<InternetSpecial> InternetSpecials { get; set; }
        public Person PrimaryContact { get; set; }
        public Person SecondaryContact { get; set; }
       
    }
}
