using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Views
{
    public interface ISelectedEvent
    {
        event EventHandler SelectedDropEvent;

        event EventHandler SelectedIndexEvent;

        event EventHandler PlayNoteEvent;

        event EventHandler StopNoteEvent;
    }
}