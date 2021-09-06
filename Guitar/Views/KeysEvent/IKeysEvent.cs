using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Views
{
    internal interface IKeysEvent
    {
        event KeyEventHandler KDown;

        event KeyEventHandler KUp;

        event KeyPressEventHandler KPress;
    }
}