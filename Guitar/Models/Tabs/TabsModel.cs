using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Models
{
    [Serializable]
    public class TabsModel
    {
        public List<List<TabModel>> tabs { get; set; } = new List<List<TabModel>>();

        public double Pulse { get; set; } = 0.25;
    }
}