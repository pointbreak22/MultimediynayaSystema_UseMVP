using Guitar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Guitar.Views;
using System.Drawing;

namespace Guitar.Presenter
{
    public class StateGuitarPresenter
    {
        private readonly IStateGuitar stateGuitar;
        private readonly IStateGuitarDispley stateGuitarDispley;
        private readonly IButtonDeckView buttonDeck;
        private readonly IButtonNeckView buttonNeck;
        private readonly PaintDeckModel paintDeckModel;
        private readonly PaintNeckModel paintNeckModel;
        private readonly Task task;
        private readonly CancellationTokenSource tokenSource;
        private CancellationToken token;
        private readonly EventWaitHandle ewh;

        public event Action<PictureBox, Bitmap> EventEditPicture;

        public StateGuitarPresenter(IStateGuitar stateGuitar, IStateGuitarDispley stateGuitarDispley, IButtonDeckView buttonDeck, IButtonNeckView buttonNeck, PaintDeckModel paintDeckModel, PaintNeckModel paintNeckModel, EventWaitHandle ewh)
        {
            this.ewh = ewh;
            this.stateGuitar = stateGuitar;
            this.stateGuitarDispley = stateGuitarDispley;
            this.buttonDeck = buttonDeck;
            this.buttonNeck = buttonNeck;
            this.paintNeckModel = paintNeckModel;
            this.paintDeckModel = paintDeckModel;
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            task = new Task(SeachState);
            task.Start();
        }

        private async void EditStateNeckAcync()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 28; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (stateGuitar.StateButtonNecks[i, j] == true)
                        {
                            if (stateGuitarDispley.StateButtonNecksDispley[i, j] == false)
                            {
                                Invoking(buttonNeck.PictureButtonNecks[i, j], () => buttonNeck.PictureButtonNecks[i, j].Image = paintDeckModel.imgs[1]);

                                stateGuitarDispley.StateButtonNecksDispley[i, j] = true;
                            }
                        }
                        else
                        if (stateGuitar.StateButtonNecks[i, j] == false)
                        {
                            if (stateGuitarDispley.StateButtonNecksDispley[i, j] == true)
                            {
                                Invoking(buttonNeck.PictureButtonNecks[i, j], () => buttonNeck.PictureButtonNecks[i, j].Image = paintDeckModel.imgs[0]);

                                stateGuitarDispley.StateButtonNecksDispley[i, j] = false;
                            }
                        }
                    }
                }
            }

            );
        }

        private void Invoking(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else action();
        }

        private async void EditStateDeckAcync()
        {
            await Task.Run(() =>
            {
                for (int j = 0; j < 6; j++)
                {
                    if (stateGuitar.StateButtonDecks[j] == true)
                    {
                        if (stateGuitarDispley.StateButtonDecksDispley[j] == false)
                        {
                            Invoking(buttonDeck.PictureButtonDecks[j], () => buttonDeck.PictureButtonDecks[j].Image = paintDeckModel.imgs[1]);

                            stateGuitarDispley.StateButtonDecksDispley[j] = true;
                        }
                    }
                    else
                    if (stateGuitar.StateButtonDecks[j] == false)
                    {
                        if (stateGuitarDispley.StateButtonDecksDispley[j] == true)
                        {
                            Invoking(buttonDeck.PictureButtonDecks[j], () => buttonDeck.PictureButtonDecks[j].Image = paintDeckModel.imgs[0]);

                            stateGuitarDispley.StateButtonDecksDispley[j] = false;
                        }
                    }
                }
            }

        );
        }

        private void SeachState()
        {
            ewh.WaitOne();
            while (!token.IsCancellationRequested)
            {
                EditStateNeckAcync();
                EditStateDeckAcync();
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

        internal void EditStateNeck(int x, int y, bool flag)
        {
            stateGuitar.StateButtonNecks[x, y] = flag;
        }

        internal void EditStateDeck(int x, bool flag)
        {
            stateGuitar.StateButtonDecks[x] = flag;
        }
    }
}