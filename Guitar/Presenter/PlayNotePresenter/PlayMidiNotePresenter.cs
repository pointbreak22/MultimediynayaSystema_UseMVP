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
        private readonly MidiModel midiModel;
        private readonly Task task;
        private readonly EventWaitHandle ewh;
        private readonly CancellationTokenSource tokenSource;
        private CancellationToken token;
        private readonly IStateGuitar stateGuitar;
        private readonly IStateGuitarPlaying stateGuitarPlaying;

        public PlayMidiNotePresenter(MidiModel midiModel, IStateGuitar stateGuitar, IStateGuitarPlaying stateGuitarPlaying, EventWaitHandle ewh)
        {
            this.midiModel = midiModel;
            this.stateGuitar = stateGuitar;
            this.stateGuitarPlaying = stateGuitarPlaying;
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
                            int k = 0;
                            for (int j = 27; j >= 0; j--)
                            {
                                if (stateGuitar.StateButtonNecks[j, i] == true)
                                {
                                    midiModel.midinote1[i] = midiModel.midinoteNeck[j, i];
                                    k++;
                                    break;
                                }
                            }
                            if (k == 0)
                            {
                                midiModel.midinote1[i] = midiModel.midinote0[i];
                            }
                            midiModel.midiOutPlay[i].Send(MidiMessage.StartNote(midiModel.midinote1[i], 127, 1).RawData);
                            stateGuitarPlaying.StateButtonDecsPlaying[i] = true;
                        }
                    }
                    if (stateGuitar.StateButtonDecks[i] == false)
                    {
                        if (stateGuitarPlaying.StateButtonDecsPlaying[i] == true)
                        {
                            midiModel.midiOutPlay[i].Send(MidiMessage.StopNote(midiModel.midinote1[i], 127, 1).RawData);
                            stateGuitarPlaying.StateButtonDecsPlaying[i] = false;
                        }
                    }
                }
            }
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