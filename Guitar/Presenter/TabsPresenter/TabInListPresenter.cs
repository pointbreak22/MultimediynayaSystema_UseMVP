using Guitar.Models;
using Guitar.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Guitar.Presenter
{
    internal class TabInListPresenter
    {
        private ITablatureTextView tablatureTextView;
        private TabsModel tabsModel { get; set; }
        private IButtonTabsEditEvents buttonTabsEditEvents;
        private int page = 0;
        private XmlSerializer formatter = new XmlSerializer(typeof(TabsModel));

        public event Action<TabsModel> LoadTabsModal;

        public TabInListPresenter(ITablatureTextView tablatureTextView, IButtonTabsEditEvents buttonTabsEditEvents, TabsModel tabsModel)
        {
            this.tablatureTextView = tablatureTextView;
            this.tabsModel = tabsModel;
            this.buttonTabsEditEvents = buttonTabsEditEvents;
            buttonTabsEditEvents.ButNewMysikEvent += ButtonTabsEditEvents_ButNewMysikEvent;
            buttonTabsEditEvents.ButAddTabsEvent += ButtonTabsEditEvents_ButAddTabsEvent;
            buttonTabsEditEvents.ButClearEvent += ButtonTabsEditEvents_ButClearEvent;
            buttonTabsEditEvents.ButRefreshTabsEvent += ButtonTabsEditEvents_ButRefreshTabsEvent;
            buttonTabsEditEvents.ButRemoteTabsEvent += ButtonTabsEditEvents_ButRemoteTabsEvent;
            buttonTabsEditEvents.SelectNumTabsEvent += ButtonTabsEditEvents_SelectNumTabsEvent;
            buttonTabsEditEvents.ButSaveallTabsEvent += ButtonTabsEditEvents_ButSaveallTabsEvent;
            buttonTabsEditEvents.OpenTabsEvent += ButtonTabsEditEvents_OpenTabsEvent;
            buttonTabsEditEvents.EditPulseEvent += ButtonTabsEditEvents_EditPulseEvent;
        }

        private void Invoking(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else action();
        }

        private void ButtonTabsEditEvents_EditPulseEvent(object sender, EventArgs e)
        {
            tabsModel.Pulse = 1 / (double.Parse((sender as TextBox).Text) / 60) / 2;
        }

        private void ButtonTabsEditEvents_ButNewMysikEvent(object sender, EventArgs e)
        {
            tabsModel.tabs = new List<List<TabModel>>();
            //  tabsModel.tabs.Clear();
        }

        private void ButtonTabsEditEvents_ButAddTabsEvent(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                List<TabModel> lad = new List<TabModel>();

                for (int j = 0; j < 6; j++)
                {
                    if (int.TryParse(tablatureTextView.Texttabs[j, i].Text, out int tab) && (tab >= 0 && tab <= 28))
                    {
                        TabModel tabModel = new TabModel { Gfret = tab, Gstring = j };
                        lad.Add(tabModel);
                    }
                }
                tabsModel.tabs.Add(lad);
            }
        }

        private void ButtonTabsEditEvents_SelectNumTabsEvent(object sender, EventArgs e)
        {
            int n = tabsModel.tabs.Count / 32;
            if (n > 0)
            {
                (sender as NumericUpDown).Minimum = 1;
            }
            else
            {
                (sender as NumericUpDown).Minimum = 0;
            }
            if ((sender as NumericUpDown).Value > n)
            {
                (sender as NumericUpDown).Value = n;
            }
            page = (int)(sender as NumericUpDown).Value;
            if (page != 0)
            {
                ShowTabPage(page);
            }
        }

        private void ShowTabPage(int page)
        {
            foreach (TextBox textBox in tablatureTextView.Texttabs)
            {
                Invoking(textBox, () => { textBox.Clear(); });
            }
            if (page <= tabsModel.tabs.Count / 32)
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

        private void ButtonTabsEditEvents_ButRemoteTabsEvent(object sender, EventArgs e)
        {
            if (page <= tabsModel.tabs.Count / 32)
            {
                for (int i = page * 32 - 32; i < page * 32; i++)
                {
                    tabsModel.tabs.RemoveAt(page * 32 - 32);
                }
                ShowTabPage(page);
            }
        }

        private void ButtonTabsEditEvents_ButRefreshTabsEvent(object sender, EventArgs e)
        {
            if (page <= tabsModel.tabs.Count / 32)
            {
                for (int i = page * 32 - 32; i < page * 32; i++)
                {
                    List<TabModel> lad = new List<TabModel>();

                    for (int j = 0; j < 6; j++)
                    {
                        if (int.TryParse(tablatureTextView.Texttabs[j, i - ((page - 1) * 32)].Text, out int tab) && (tab >= 0 && tab <= 28))
                        {
                            TabModel tabModel = new TabModel { Gfret = tab, Gstring = j };
                            lad.Add(tabModel);
                        }
                    }
                    tabsModel.tabs[i] = lad;
                }
            }
        }

        private void ButtonTabsEditEvents_ButSaveallTabsEvent(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.xml)|*.xml|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = saveFileDialog.FileName;

            //Сохранение
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, tabsModel);
                MessageBox.Show("Данные сохранены");
            }
        }

        private void ButtonTabsEditEvents_ButClearEvent(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    tablatureTextView.Texttabs[j, i].Clear();
                }
            }
        }

        private void ButtonTabsEditEvents_OpenTabsEvent(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.xml)|*.xml|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialog.FileName;
            //Загрузка
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                tabsModel = (TabsModel)formatter.Deserialize(fs);
                LoadTabsModal?.Invoke(tabsModel);
                MessageBox.Show("Данные загружены");
            }
        }
    }
}