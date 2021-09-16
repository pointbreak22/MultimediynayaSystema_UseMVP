using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Views
{
    internal interface ISelectedMidi
    {
        event EventHandler ComboPlaysSelectedDropEvent;

        event EventHandler ComboPlaysSelectedIndexEvent;

        event EventHandler ValueChanged;

        event EventHandler ComboGameModeSelectedIndexChanged;

        event EventHandler ComboGameModeDropDown;

        string SelectInstrument { get; set; }
    }
}