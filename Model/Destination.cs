using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Destination
    {
        private string _todayForecast;

        public int DestinationId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public List<Lodging> Lodgings { get; set; }

        public string TodayForecast
        {
            get { return _todayForecast; }
            set { _todayForecast = value; }
        }
    }
}
