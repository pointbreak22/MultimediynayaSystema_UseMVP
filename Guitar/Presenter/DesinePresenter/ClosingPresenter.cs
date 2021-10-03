using Guitar.Models;
using Guitar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Presenter
{
    public class ClosingPresenter
    {
        private IFormClosing formClosing;
        private MidiModel midiModel;
        private StateGuitarPresenter stateGuitarPresenter;
        private PlayMidiNotePresenter playMidiNotePresenter;

        public ClosingPresenter(IFormClosing formClosing, MidiModel midiModel, StateGuitarPresenter stateGuitarPresenter, PlayMidiNotePresenter playMidiNotePresenter)
        {
            this.formClosing = formClosing;
            this.midiModel = midiModel;
            this.stateGuitarPresenter = stateGuitarPresenter;
            this.playMidiNotePresenter = playMidiNotePresenter;
            formClosing.ClosingForm += FormClosing_ClosingForm;
        }

        private void FormClosing_ClosingForm(object sender, EventArgs e)
        {
            stateGuitarPresenter.SeachStateDispose();
            playMidiNotePresenter.SeachStateDispose();
            midiModel.midiOut0.Dispose();
            midiModel.midiOut1.Dispose();
        }
    }
}