using System;
using Guitar.Views;
using Guitar.Models;
using System.Windows.Forms;
using System.Drawing;

namespace Guitar.Presenter
{
    public class ButtonNeckPresenter
    {
        private readonly IButtonNeckView buttonNeckView;
        private PaintNeckModel buttonNeckModel;
        private readonly StateGuitarPresenter stateGuitarPresenter;
        private IPictureIn pictureIn;

        public ButtonNeckPresenter(IButtonNeckView necksView)
        {
            buttonNeckView = necksView;
        }

        public ButtonNeckPresenter(IButtonNeckView necksView, StateGuitarPresenter stateGuitarPresenter, IPictureIn pictureIn) : this(necksView)
        {
            this.stateGuitarPresenter = stateGuitarPresenter;
            this.pictureIn = pictureIn;
            PaintButtonNeck();
        }

        public void PaintButtonNeck()
        {
            pictureIn.PanelNeck.Controls.Clear();
            buttonNeckView.PictureButtonNecks = new PictureBox[28, 6];
            buttonNeckModel = new PaintNeckModel();
            int x = pictureIn.PanelNeck.Width - 12;
            int y = 2;
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 28; i++)
                {
                    buttonNeckView.PictureButtonNecks[i, j] = new ButtonNeckView().pictureButtonNeck;
                    buttonNeckView.PictureButtonNecks[i, j].Location = new Point(x, y); x -= 12;
                    buttonNeckView.PictureButtonNecks[i, j].Image = buttonNeckModel.imgs[0];
                    buttonNeckView.PictureButtonNecks[i, j].MouseEnter += new EventHandler(Inmousegr);
                    buttonNeckView.PictureButtonNecks[i, j].MouseLeave += new EventHandler(Outmousegr);
                    buttonNeckView.PictureButtonNecks[i, j].Name = (27 - i).ToString() + " " + j.ToString();
                    pictureIn.PanelNeck.Controls.Add(buttonNeckView.PictureButtonNecks[i, j]);
                }
                y += 12;
                x = pictureIn.PanelNeck.Width - 12;
            }
        }

        private void Inmousegr(object sender, EventArgs e)
        {
            stateGuitarPresenter.EditStateNeck(28 - int.Parse((sender as PictureBox).Name.Split(' ')[0]), int.Parse((sender as PictureBox).Name.Split(' ')[1]), true);
        }

        private void Outmousegr(object sender, EventArgs e)
        {
            stateGuitarPresenter.EditStateNeck(28 - int.Parse((sender as PictureBox).Name.Split(' ')[0]), int.Parse((sender as PictureBox).Name.Split(' ')[1]), false);
        }
    }
}