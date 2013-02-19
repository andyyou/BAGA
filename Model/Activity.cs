using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public List<Trip> Trips { get; set; }
    }
}
