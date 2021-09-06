using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Models
{
    public interface IStateGuitar
    {
        bool[] StateButtonDecks { get; set; }
        bool[,] StateButtonNecks { get; set; }
    }
}