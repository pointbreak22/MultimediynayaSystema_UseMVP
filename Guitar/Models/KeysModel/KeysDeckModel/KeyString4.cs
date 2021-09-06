using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Models.KeysModel
{
    internal class KeyString4 : AbstractKey
    {
        public KeyString4()
        {
            Key = Keys.W;
        }

        public override void EventKey(bool[] statusDeck, bool status)
        {
            statusDeck[3] = status;
        }
    }
}