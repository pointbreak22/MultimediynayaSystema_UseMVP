using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Models
{
    public class StateGuitar : IStateGuitar, IStateGuitarPlaying, IStateGuitarDispley
    {
        public bool[] StateButtonDecks { get; set; }
        public bool[] StateButtonDecksDispley { get; set; }

        public bool[] StateButtonDecsPlaying { get; set; }

        public bool[,] StateButtonNecks { get; set; }

        public bool[,] StateButtonNecksDispley { get; set; }

        public bool[,] StateButtonNecksPlaying { get; set; }

        public StateGuitar()
        {
            StateButtonDecks = new bool[6];
            StateButtonDecksDispley = new bool[6];
            StateButtonDecsPlaying = new bool[6];

            StateButtonNecks = new bool[29, 6];
            StateButtonNecksDispley = new bool[28, 6];
            StateButtonNecksPlaying = new bool[29, 6];
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 29; i++)
                {
                    StateButtonNecks[i, j] = false;
                    StateButtonNecksPlaying[i, j] = false;

                    if (i != 28)
                    {
                        StateButtonNecksDispley[i, j] = false;
                    }
                }
                StateButtonDecks[j] = false;
                StateButtonDecksDispley[j] = false;
                StateButtonDecsPlaying[j] = false;
            }
        }
    }
}