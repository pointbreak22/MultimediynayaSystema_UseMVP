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
        // public List<TabModel> flet = new List<TabModel>();
        public List<List<TabModel>> tabs = new List<List<TabModel>>();

        public double Pulse { get; set; } = 0.25;
    }
}