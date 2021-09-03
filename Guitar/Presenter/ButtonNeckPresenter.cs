using System;
using Guitar.Views;
using Guitar.Models;
using System.Windows.Forms;
using System.Drawing;

namespace Guitar.Presenter
{
    public class ButtonNeckPresenter
    {
        private IButtonNeckView buttonNeckView;
        private PaintNeckModel buttonNeckModel;
        private StateGuitarPresenter stateGuitarPresenter;

        public ButtonNeckPresenter(IButtonNeckView necksView)
        {
            buttonNeckView = necksView;
        }

        public ButtonNeckPresenter(IButtonNeckView necksView, StateGuitarPresenter stateGuitarPresenter) : this(necksView)
        {
            this.stateGuitarPresenter = stateGuitarPresenter;
        }

        public void PaintButtonNeck(Panel panel)
        {
            buttonNeckModel = new PaintNeckModel();

            int x = panel.Width - 12;
            int y = 2;
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 28; i++)
                {
                    buttonNeckView.pictureButtonNecks[i, j] = new ButtonNeckView().pictureButtonNeck;
                    buttonNeckView.pictureButtonNecks[i, j].Location = new Point(x, y); x -= 12;
                    buttonNeckView.pictureButtonNecks[i, j].Image = buttonNeckModel.imgs[0];
                    buttonNeckView.pictureButtonNecks[i, j].MouseEnter += new EventHandler(Inmousegr);
                    buttonNeckView.pictureButtonNecks[i, j].MouseLeave += new EventHandler(Outmousegr);
                    buttonNeckView.pictureButtonNecks[i, j].Name = (27 - i).ToString() + " " + j.ToString();
                    panel.Controls.Add(buttonNeckView.pictureButtonNecks[i, j]);
                }
                y += 12;
                x = panel.Width - 12;
            }
        }

        private void Inmousegr(object sender, EventArgs e)
        {
            //  (sender as PictureBox).Image = buttonNeckModel.imgs[1];
            stateGuitarPresenter.EditStateNeck(27 - int.Parse((sender as PictureBox).Name.Split(' ')[0]), int.Parse((sender as PictureBox).Name.Split(' ')[1]), true);
        }

        private void Outmousegr(object sender, EventArgs e)
        {
            //  (sender as PictureBox).Image = buttonNeckModel.imgs[0];
            stateGuitarPresenter.EditStateNeck(27 - int.Parse((sender as PictureBox).Name.Split(' ')[0]), int.Parse((sender as PictureBox).Name.Split(' ')[1]), false);
        }
    }
}