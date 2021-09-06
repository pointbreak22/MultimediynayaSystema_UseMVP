using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Models.KeysModel
{
    internal class KeyString6 : AbstractKey
    {
        public KeyString6()
        {
            Key = Keys.X;
        }

        public override void EventKey(bool[] statusDeck, bool status)
        {
            statusDeck[5] = status;
        }
    }
}