using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Hostel : Lodging
    {
        public int MaxPersonPerRoom { get; set; }
        public bool PrivateRoomsAvailable { get; set; }
    }
}
