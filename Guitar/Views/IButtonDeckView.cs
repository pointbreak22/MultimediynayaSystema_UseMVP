using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Views
{
    public interface IButtonDeckView
    {
        PictureBox[] pictureButtonDecks { get; set; }
    }
}