using System.Windows.Forms;

namespace Guitar.Views
{
    public class ButtonNeckView
    {
        public PictureBox pictureButtonNeck;

        public ButtonNeckView()
        {
            pictureButtonNeck = new PictureBox();
            pictureButtonNeck.Size = new System.Drawing.Size(10, 10);
            pictureButtonNeck.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}