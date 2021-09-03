using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Models
{
    public class StateGuitar
    {
        public bool[] StateButtonDecks1 { get; set; }
        public bool[] StateButtonDecks2 { get; set; }
        public bool[,] StateButtonNecks1 { get; set; }
        public bool[,] StateButtonNecks2 { get; set; }

        public StateGuitar()
        {
            StateButtonDecks1 = new bool[6];
            StateButtonDecks2 = new bool[6];
            StateButtonNecks1 = new bool[28, 6];
            StateButtonNecks2 = new bool[28, 6];
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 28; i++)
                {
                    StateButtonNecks1[i, j] = false;
                    StateButtonNecks2[i, j] = false;
                }
                StateButtonDecks1[j] = false;
                StateButtonDecks2[j] = false;
            }
        }
    }
}