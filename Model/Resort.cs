using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Resort:Lodging
    {
        public string Entertainment { get; set; }
        public string Activities { get; set; }
    }
}
