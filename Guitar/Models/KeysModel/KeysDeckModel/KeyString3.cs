using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Models.KeysModel
{
    internal class KeyString3 : AbstractKey
    {
        public KeyString3()
        {
            Key = Keys.Z;
        }

        public override void EventKey(bool[] statusDeck, bool status)
        {
            statusDeck[2] = status;
        }
    }
}