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
    public class PlayTabsPresenter : IDisposable
    {
        private ITabsPlay tabsPlay;
        private TabsModel tabsModel;
        private IStateGuitar stateGuitar;
        private ITablatureTextView tablatureTextView;
        private IButtonTabsEditEvents buttonTabsEditEvents;
        private int[] deck = new int[6];
        private CancellationTokenSource cancellationToken;
        private CancellationToken token;
        private IPageUppdate pageUppdate;
        private IPulsUppdate pulsUppdate;
        private ListInTabsPresenter listInTabsPresenter;
        private bool flagPlay = false;

        public PlayTabsPresenter(ITabsPlay tabsPlay, ITablatureTextView tablatureTextView, TabsModel tabsModel, IStateGuitar stateGuitar, IPageUppdate pageUppdate, IPulsUppdate pulsUppdate, ListInTabsPresenter listInTabsPresenter)
        {
            this.tabsPlay = tabsPlay;
            this.tabsModel = tabsModel;
            this.stateGuitar = stateGuitar;
            this.tablatureTextView = tablatureTextView;
            this.pageUppdate = pageUppdate;
            this.pulsUppdate = pulsUppdate;
            this.listInTabsPresenter = listInTabsPresenter;
            tabsPlay.ButPlayallTabsEvent += TabsPlay_ButPlayallTabsEvent;
            tabsPlay.ButStopAllEvent += TabsPlay_ButStopAllEvent;
        }

        public TabsModel TabsModel
        {
            get { return tabsModel; }
            set { tabsModel = value; }
        }

        private void TabsPlay_ButPlayallTabsEvent(object sender, EventArgs e)
        {
            //    MessageBox.Show("111");
            if (flagPlay == false)
            {
                cancellationToken = new CancellationTokenSource();
                token = cancellationToken.Token;
                PlayNow(tabsModel.tabs);
                flagPlay = true;
            }
        }

        private async void PlayNow(List<List<TabModel>> tabs)
        {
            await Task.Run(() =>
            {
                pageUppdate.NumericEnable = false;
                pulsUppdate.PulseEnable = false;
                if (tabs == null || pageUppdate.NumericPageValue == 0 || tabs.Count == 0)
                {
                    MessageBox.Show("Табулатура не закружена или не сохранена");
                }
                else
                {
                    if (pageUppdate.NumericPageValue == 0)
                    {
                        pageUppdate.NumericPageValue = 1;
                    }
                    else
                    {
                        listInTabsPresenter.ShowTabPage(pageUppdate.NumericPageValue);
                    }
                    for (int i = (pageUppdate.NumericPageValue - 1) * 32; i < tabs.Count; i++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            return;
                        }
                        if (i % 32 == 0)
                        {
                            TabsinWhite();
                            pageUppdate.NumericPageValue = i / 32 + 1;
                        }

                        foreach (TabModel tabModel in tabs[i])
                        {
                            stateGuitar.StateButtonNecks[tabModel.Gfret, tabModel.Gstring] = true;
                            stateGuitar.StateButtonDecks[tabModel.Gstring] = true;
                            deck[tabModel.Gstring] = tabModel.Gfret;
                            Invoking(tablatureTextView.Texttabs[tabModel.Gstring, i - ((pageUppdate.NumericPageValue - 1) * 32)], () => tablatureTextView.Texttabs[tabModel.Gstring, i - ((pageUppdate.NumericPageValue - 1) * 32)].ForeColor = System.Drawing.Color.Gray);
                        }
                        Wait(tabsModel.Pulse / 12 * 11);
                        foreach (TabModel tabModel in tabs[i])
                        {
                            stateGuitar.StateButtonNecks[tabModel.Gfret, tabModel.Gstring] = false;

                            stateGuitar.StateButtonDecks[tabModel.Gstring] = false;
                        }
                        Wait(tabsModel.Pulse / 12);
                    }
                }
            });
            pageUppdate.NumericEnable = true;
            pulsUppdate.PulseEnable = true;
            flagPlay = false;
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

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            // подавляем финализацию
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Освобождаем управляемые ресурсы
                }
                // освобождаем неуправляемые объекты
                disposed = true;
            }
        }

        ~PlayTabsPresenter()
        {
            Dispose(false);
        }
    }
}