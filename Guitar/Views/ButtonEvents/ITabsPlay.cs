using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Views
{
    public interface ITabsPlay
    {
        event EventHandler ButPlayallTabsEvent;

        event EventHandler ButStopAllEvent;
    }
}