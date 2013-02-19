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
        // 在Fluent API 中無法直接對物件下[Required]
        // 如果兩者都用的話 Destination 和 DestinationId 資料庫的欄位名稱不會有先後順序都是 DestinationId 不像 Data annotation 會有先後 就是欄位名稱是 Destination_DestinationId  或 DestinationId
        public Destination Destination { get; set; }
        public int DestinationId { get; set; }
        // public int LocationId { get; set; }
        public decimal MilesFromNearestAirport { get; set; }
        public List<InternetSpecial> InternetSpecials { get; set; }
        public Person PrimaryContact { get; set; }
        public Person SecondaryContact { get; set; }
    }
}
