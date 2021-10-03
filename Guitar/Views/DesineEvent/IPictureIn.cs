using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Views
{
    public interface IPictureIn
    {
        Panel PanelDeck { get; set; }
        Panel PanelNeck { get; set; }
        FlowLayoutPanel LayoutPanel { get; set; }
    }
}