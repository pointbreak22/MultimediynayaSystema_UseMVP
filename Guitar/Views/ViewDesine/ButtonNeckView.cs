using System.Windows.Forms;

namespace Guitar.Views
{
    public class ButtonNeckView
    {
        public PictureBox pictureButtonNeck;

        public ButtonNeckView()
        {
            pictureButtonNeck = new PictureBox
            {
                Size = new System.Drawing.Size(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
        }
    }
}