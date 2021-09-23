using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Views
{
    internal interface ITabsPlay
    {
        event EventHandler ButPlayallTabsEvent;

        event EventHandler ButStopAllEvent;

        int PageTab { get; set; }
    }
}