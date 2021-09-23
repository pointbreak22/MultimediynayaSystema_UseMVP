using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Models
{
    [Serializable]
    public class TabModel
    {
        public int Gstring { get; set; }
        public int Gfret { get; set; }

        //  public bool StatusString { get; set; }
    }
}