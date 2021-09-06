using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Models
{
    public interface IStateGuitarPlaying
    {
        bool[] StateButtonDecsPlaying { get; set; }
        bool[,] StateButtonNecksPlaying { get; set; }
    }
}