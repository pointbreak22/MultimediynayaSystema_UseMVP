using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Models
{
    public interface IStateGuitarDispley
    {
        bool[] StateButtonDecksDispley { get; set; }
        bool[,] StateButtonNecksDispley { get; set; }
    }
}