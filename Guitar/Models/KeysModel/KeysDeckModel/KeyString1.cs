using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Models.KeysModel
{
    public class KeyString1 : AbstractKey
    {
        public KeyString1()
        {
            Key = Keys.Q;
        }

        public override void EventKey(bool[] statusDeck, bool status)
        {
            statusDeck[0] = status;
        }
    }
}