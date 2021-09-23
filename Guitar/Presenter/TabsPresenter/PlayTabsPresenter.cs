using Guitar.Models;
using Guitar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Guitar.Presenter
{
    internal class PlayTabsPresenter
    {
        private ITabsPlay tabsPlay;
        private TabsModel tabsModel;
        private IStateGuitar stateGuitar;
        private ITablatureTextView tablatureTextView;
        private IButtonTabsEditEvents buttonTabsEditEvents;
        private int[] deck = new int[6];

        // private Task taskPlay;
        private CancellationTokenSource cancellationToken;

        private CancellationToken token;
        private bool flagPlay = false;

        public PlayTabsPresenter(ITabsPlay tabsPlay, ITablatureTextView tablatureTextView, TabsModel tabsModel, IStateGuitar stateGuitar)
        {
            this.tabsPlay = tabsPlay;
            this.tabsModel = tabsModel;
            this.stateGuitar = stateGuitar;
            this.tablatureTextView = tablatureTextView;

            tabsPlay.ButPlayallTabsEvent += TabsPlay_ButPlayallTabsEvent;
            tabsPlay.ButStopAllEvent += TabsPlay_ButStopAllEvent;

            //  taskPlay = new Task(PlayNow);
        }

        private void Invoking(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else action();
        }

        private async void TabsinWhite()
        {
            await Task.Run(() =>
            {
                foreach (TextBox textBox in tablatureTextView.Texttabs)
                {
                    Invoking(textBox, () => textBox.ForeColor = Color.White);
                }
            });
        }

        private async void PlayNow()
        {
            await Task.Run(() =>
            {
                if (tabsPlay.PageTab == 0)
                {
                    tabsPlay.PageTab = 1;
                }
                for (int i = (tabsPlay.PageTab - 1) * 32; i < tabsModel.tabs.Count; i++)
                {
                    if (i % 32 == 0)
                    {
                        TabsinWhite();
                        tabsPlay.PageTab = i / 32 + 1;
                    }
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    foreach (TabModel tabModel in tabsModel.tabs[i])
                    {
                        stateGuitar.StateButtonNecks[tabModel.Gfret, tabModel.Gstring] = true;
                        stateGuitar.StateButtonDecks[tabModel.Gstring] = true;
                        deck[tabModel.Gstring] = tabModel.Gfret;
                        Invoking(tablatureTextView.Texttabs[tabModel.Gstring, i - ((tabsPlay.PageTab - 1) * 32)], () => tablatureTextView.Texttabs[tabModel.Gstring, i - ((tabsPlay.PageTab - 1) * 32)].ForeColor = System.Drawing.Color.Gray);
                    }
                    Wait(tabsModel.Pulse / 12 * 11);
                    foreach (TabModel tabModel in tabsModel.tabs[i])
                    {
                        stateGuitar.StateButtonNecks[tabModel.Gfret, tabModel.Gstring] = false;
                        stateGuitar.StateButtonDecks[tabModel.Gstring] = false;
                    }
                    Wait(tabsModel.Pulse / 12);
                }
            });
            flagPlay = false;
        }

        private void TabsPlay_ButPlayallTabsEvent(object sender, EventArgs e)
        {
            if (flagPlay == false)
            {
                cancellationToken = new CancellationTokenSource();
                token = cancellationToken.Token;
                PlayNow();
                flagPlay = true;
            }
        }

        private void TabsPlay_ButStopAllEvent(object sender, EventArgs e)
        {
            cancellationToken.Cancel();
        }

        private void Wait(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }
    }
}