using Guitar.Models;
using Guitar.Views;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Presenter
{
    public class ButtonDeckPresenter
    {
        private IButtonDeckView buttonDeckView;
        private PaintDeckModel buttonDeckModel;
        private StateGuitarPresenter stateGuitarPresenter;

        public ButtonDeckPresenter(IButtonDeckView necksView)
        {
            buttonDeckView = necksView;
        }

        public ButtonDeckPresenter(IButtonDeckView necksView, StateGuitarPresenter stateGuitarPresenter) : this(necksView)
        {
            this.stateGuitarPresenter = stateGuitarPresenter;
        }

        public void PaintButtonDeck(Panel panel)
        {
            buttonDeckModel = new PaintDeckModel();

            int x = 2;

            for (int j = 0; j < 6; j++)
            {
                buttonDeckView.pictureButtonDecks[j] = new ButtonDeckView().pictureButtonDeck;
                buttonDeckView.pictureButtonDecks[j].Location = new Point(2, x); x += 12;
                buttonDeckView.pictureButtonDecks[j].Image = buttonDeckModel.imgs[0];

                buttonDeckView.pictureButtonDecks[j].MouseEnter += new EventHandler(Inmousegr);
                buttonDeckView.pictureButtonDecks[j].MouseLeave += new EventHandler(Outmousegr);
                buttonDeckView.pictureButtonDecks[j].Name = j.ToString();
                panel.Controls.Add(buttonDeckView.pictureButtonDecks[j]);
            }
        }

        private void Outmousegr(object sender, EventArgs e)
        {
            stateGuitarPresenter.EditStateDeck(int.Parse((sender as PictureBox).Name), false);
        }

        private void Inmousegr(object sender, EventArgs e)
        {
            stateGuitarPresenter.EditStateDeck(int.Parse((sender as PictureBox).Name), true);
        }
    }
}