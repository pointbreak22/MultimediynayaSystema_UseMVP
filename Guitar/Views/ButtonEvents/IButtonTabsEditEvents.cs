using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Views
{
    internal interface IButtonTabsEditEvents
    {
        event EventHandler ButNewMysikEvent;

        event EventHandler ButAddTabsEvent;

        event EventHandler ButClearEvent;

        event EventHandler SelectNumTabsEvent;

        event EventHandler ButRefreshTabsEvent;

        event EventHandler ButRemoteTabsEvent;

        event EventHandler ButSaveallTabsEvent;

        event EventHandler OpenTabsEvent;

        event EventHandler EditPulseEvent;
    }
}