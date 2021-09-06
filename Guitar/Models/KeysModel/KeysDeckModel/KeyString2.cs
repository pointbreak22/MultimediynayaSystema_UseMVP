using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Models.KeysModel
{
    internal class KeyString2 : AbstractKey
    {
        public KeyString2()
        {
            Key = Keys.A;
        }

        public override void EventKey(bool[] statusDeck, bool status)
        {
            statusDeck[1] = status;
        }
    }
}