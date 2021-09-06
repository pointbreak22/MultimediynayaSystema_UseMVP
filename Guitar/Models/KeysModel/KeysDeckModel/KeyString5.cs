using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Models.KeysModel
{
    internal class KeyString5 : AbstractKey
    {
        public KeyString5()
        {
            Key = Keys.S;
        }

        public override void EventKey(bool[] statusDeck, bool status)
        {
            statusDeck[4] = status;
        }
    }
}