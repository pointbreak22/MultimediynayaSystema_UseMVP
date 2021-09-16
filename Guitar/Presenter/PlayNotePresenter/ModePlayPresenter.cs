using Guitar.Models;
using Guitar.Views;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Presenter
{
    internal class ModePlayPresenter
    {
        private ISelectedMidi selectedMidi;
        private MidiModel midiModel;

        public ModePlayPresenter(ISelectedMidi selectedMidi, MidiModel midiModel)
        {
            this.selectedMidi = selectedMidi;
            this.midiModel = midiModel;
            selectedMidi.ComboPlaysSelectedDropEvent += SelectedMidi_ComboPlaysSelectedDropEvent;
            selectedMidi.ComboPlaysSelectedIndexEvent += SelectedMidi_ComboPlaysSelectedIndexEvent;
            selectedMidi.ComboGameModeSelectedIndexChanged += SelectedMidi_ComboGameModeSelectedIndexChanged;
            selectedMidi.ComboGameModeDropDown += SelectedMidi_ComboGameModeDropDown;
            selectedMidi.ValueChanged += SelectedMidi_ValueChanged;
        }

        private void SelectedMidi_ComboPlaysSelectedDropEvent(object sender, EventArgs e)
        {
            (sender as ComboBox).Items.Clear();
            foreach (int playModeMidi in midiModel.PlayModeMidi)
            {
                (sender as ComboBox).Items.Add(playModeMidi);
            }
        }

        private void SelectedMidi_ComboPlaysSelectedIndexEvent(object sender, EventArgs e)
        {
            midiModel.SelectedModeMidi = (sender as ComboBox).SelectedIndex;
            for (int i = 0; i < midiModel.midiOutPlay.Length; i++)
            {
                midiModel.midiOutPlay[i] = midiModel.midiOutSelected[(sender as ComboBox).SelectedIndex];
            }
        }

        private void SelectedMidi_ComboGameModeDropDown(object sender, EventArgs e)
        {
        }

        private void SelectedMidi_ComboGameModeSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SelectedMidi_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < midiModel.midiOutPlay.Length; i++)
            {
                midiModel.midiOutPlay[i].Send(MidiMessage.ChangePatch(Convert.ToInt32((sender as NumericUpDown).Value), 1).RawData);
            }
            if (midiModel.SelectedModeMidi == 0)
            {
                selectedMidi.SelectInstrument = midiModel.InstrumentNames[Convert.ToInt32((sender as NumericUpDown).Value)].ToString();
            }
            else
            {
                selectedMidi.SelectInstrument = "";
            }
        }
    }
}