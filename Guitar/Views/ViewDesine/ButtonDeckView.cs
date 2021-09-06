using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Views
{
    internal class ButtonDeckView
    {
        public PictureBox pictureButtonDeck;

        public ButtonDeckView()
        {
            pictureButtonDeck = new PictureBox
            {
                Size = new System.Drawing.Size(138, 10),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
        }
    }
}