using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Models.KeysModel
{
    public abstract class AbstractKey
    {
        public Keys Key { get; set; }

        public virtual void EventKey(bool[] statusDeck, bool status)
        {
        }
    }
}