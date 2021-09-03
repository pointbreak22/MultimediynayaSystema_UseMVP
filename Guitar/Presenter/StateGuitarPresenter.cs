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
        private StateGuitar stateGuitar;
        private IButtonDeckView buttonDeck;
        private IButtonNeckView buttonNeck;
        private PaintDeckModel paintDeckModel;
        private PaintNeckModel paintNeckModel;
        private Task task;
        private CancellationTokenSource tokenSource;
        private CancellationToken token;
        private EventWaitHandle ewh;

        public event Action<PictureBox, Bitmap> EventEditPicture;

        public StateGuitarPresenter(StateGuitar stateGuitar, IButtonDeckView buttonDeck, IButtonNeckView buttonNeck, PaintDeckModel paintDeckModel, PaintNeckModel paintNeckModel, EventWaitHandle ewh)
        {
            this.ewh = ewh;
            this.stateGuitar = stateGuitar;
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
                        if (stateGuitar.StateButtonNecks1[i, j] == true)
                        {
                            if (stateGuitar.StateButtonNecks2[i, j] == false)
                            {
                                EventEditPicture?.Invoke(buttonNeck.pictureButtonNecks[i, j], paintNeckModel.imgs[1]);
                                stateGuitar.StateButtonNecks2[i, j] = true;
                            }
                        }
                        else
                        if (stateGuitar.StateButtonNecks1[i, j] == false)
                        {
                            if (stateGuitar.StateButtonNecks2[i, j] == true)
                            {
                                EventEditPicture?.Invoke(buttonNeck.pictureButtonNecks[i, j], paintNeckModel.imgs[0]);
                                stateGuitar.StateButtonNecks2[i, j] = false;
                            }
                        }
                    }
                }
            }

            );
        }

        private async void EditStateDeckAcync()
        {
            await Task.Run(() =>
            {
                for (int j = 0; j < 6; j++)
                {
                    if (stateGuitar.StateButtonDecks1[j] == true)
                    {
                        if (stateGuitar.StateButtonDecks2[j] == false)
                        {
                            EventEditPicture?.Invoke(buttonDeck.pictureButtonDecks[j], paintDeckModel.imgs[1]);
                            stateGuitar.StateButtonDecks2[j] = true;
                        }
                    }
                    else
                    if (stateGuitar.StateButtonDecks1[j] == false)
                    {
                        if (stateGuitar.StateButtonDecks2[j] == true)
                        {
                            EventEditPicture?.Invoke(buttonDeck.pictureButtonDecks[j], paintDeckModel.imgs[0]);
                            stateGuitar.StateButtonDecks2[j] = false;
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
            MessageBox.Show("Обработка закончилась");
        }

        public void SeachStareDispose()
        {
            tokenSource.Cancel();
        }

        public void StartTread()
        {
            ewh.Set();
        }

        internal void EditStateNeck(int x, int y, bool flag)
        {
            stateGuitar.StateButtonNecks1[x, y] = flag;
        }

        internal void EditStateDeck(int x, bool flag)
        {
            stateGuitar.StateButtonDecks1[x] = flag;
        }
    }
}