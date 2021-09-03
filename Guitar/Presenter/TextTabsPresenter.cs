using Guitar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Presenter
{
    public class TextTabsPresenter
    {
        private ITablatureTextView tablatureText;

        public TextTabsPresenter(ITablatureTextView tablatureText)
        {
            this.tablatureText = tablatureText;
        }

        public void PaintTabs(FlowLayoutPanel layoutPanel)
        {
            tablatureText.Texttabs = new TextBox[6, 32];
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 32; j++)
                {
                    tablatureText.Texttabs[i, j] = new TablatureTextView().textTabs;
                    if (j % 8 == 0)
                        tablatureText.Texttabs[i, j].BorderStyle = BorderStyle.Fixed3D;
                    else tablatureText.Texttabs[i, j].BorderStyle = BorderStyle.FixedSingle;
                    layoutPanel.Controls.Add(tablatureText.Texttabs[i, j]);
                }
        }
    }
}