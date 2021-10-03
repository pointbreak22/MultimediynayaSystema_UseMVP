using Guitar.Models;
using Guitar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Presenter
{
    public class ListInTabsPresenter
    {
        private TabsModel tabsModel;
        private ITablatureTextView tablatureTextView;

        public ListInTabsPresenter(TabsModel tabsModel, ITablatureTextView tablatureTextView)
        {
            this.tabsModel = tabsModel;
            this.tablatureTextView = tablatureTextView;
        }

        public TabsModel TabsModel
        {
            get { return tabsModel; }
            set { tabsModel = value; }
        }

        private void Invoking(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else action();
        }

        public async void ShowTabPage(int page)
        {
            await Task.Run(() =>
            {
                foreach (TextBox textBox in tablatureTextView.Texttabs)
                {
                    Invoking(textBox, () => { textBox.Clear(); });
                }
            }
            );
            await Task.Run(() =>
            {
                if (page <= tabsModel.tabs.Count / 32 && page != 0)
                {
                    for (int i = page * 32 - 32; i < page * 32; i++)
                    {
                        foreach (TabModel tab in tabsModel.tabs[i])
                        {
                            Invoking(tablatureTextView.Texttabs[tab.Gstring, i - ((page - 1) * 32)], () => tablatureTextView.Texttabs[tab.Gstring, i - ((page - 1) * 32)].Text = tab.Gfret.ToString());
                        }
                    }
                }
            }
            );
        }
    }
}