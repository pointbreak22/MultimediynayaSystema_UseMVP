using Guitar.Models;
using Guitar.Views;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Presenter
{
    public class PlayMidiNotePresenter
    {
        private readonly ISelectedEvent selectedEvent;
        private readonly MidiModel midiModel;
        private readonly Task task;
        private readonly EventWaitHandle ewh;
        private readonly CancellationTokenSource tokenSource;
        private CancellationToken token;
        private readonly IStateGuitar stateGuitar;
        private readonly IStateGuitarPlaying stateGuitarPlaying;

        public PlayMidiNotePresenter(MidiModel midiModel, IStateGuitar stateGuitar, IStateGuitarPlaying stateGuitarPlaying, ISelectedEvent selectedEvent, EventWaitHandle ewh)
        {
            this.midiModel = midiModel;
            this.selectedEvent = selectedEvent;
            this.stateGuitar = stateGuitar;
            this.stateGuitarPlaying = stateGuitarPlaying;
            selectedEvent.SelectedDropEvent += SelectedEvent_SelectedDropEvent;
            selectedEvent.SelectedIndexEvent += SelectedEvent_SelectedIndexEvent;
            selectedEvent.PlayNoteEvent += SelectedEvent_PlayNoteEvent;
            selectedEvent.StopNoteEvent += SelectedEvent_StopNoteEvent;
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            this.ewh = ewh;
            task = new Task(PlayNote);
            task.Start();
        }

        private void PlayNote()
        {
            ewh.WaitOne();
            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < stateGuitar.StateButtonDecks.Length; i++)
                {
                    if (stateGuitar.StateButtonDecks[i] == true)
                    {
                        if (stateGuitarPlaying.StateButtonDecsPlaying[i] == false)
                        {
                            midiModel.midi0.Send(MidiMessage.StartNote(midiModel.midinote0[i], 127, 1).RawData);
                            stateGuitarPlaying.StateButtonDecsPlaying[i] = true;
                        }
                    }
                    if (stateGuitar.StateButtonDecks[i] == false)
                    {
                        if (stateGuitarPlaying.StateButtonDecsPlaying[i] == true)
                        {
                            midiModel.midi0.Send(MidiMessage.StopNote(midiModel.midinote0[i], 127, 1).RawData);
                            stateGuitarPlaying.StateButtonDecsPlaying[i] = false;
                        }
                    }
                }
            }
            MessageBox.Show("Обработка закончилась");
        }

        private void SelectedEvent_SelectedDropEvent(object sender, EventArgs e)
        {
            (sender as ComboBox).Items.Clear();
            for (int i = 0; i < midiModel.InstrumentNames.Length; i++)
                (sender as ComboBox).Items.Add(midiModel.InstrumentNames[i]);
        }

        private void SelectedEvent_SelectedIndexEvent(object sender, EventArgs e)
        {
            midiModel.midi0.Send(MidiMessage.ChangePatch((sender as ComboBox).SelectedIndex, 3).RawData);
        }

        private void SelectedEvent_PlayNoteEvent(object sender, EventArgs e)
        {
            midiModel.midi0.Send(MidiMessage.StartNote(midiModel.midinote0[5], 127, 1).RawData);
        }

        private void SelectedEvent_StopNoteEvent(object sender, EventArgs e)
        {
            midiModel.midi0.Send(MidiMessage.StopNote(midiModel.midinote0[5], 127, 1).RawData);
        }

        public void SeachStateDispose()
        {
            tokenSource.Cancel();
        }

        public void StartTread()
        {
            ewh.Set();
        }
    }
}