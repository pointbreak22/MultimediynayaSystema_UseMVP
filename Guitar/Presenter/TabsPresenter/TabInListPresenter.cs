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
    public class TabInListPresenter
    {
        private ITablatureTextView tablatureTextView;
        private TabsModel tabsModel;
        private IButtonTabsEditEvents buttonTabsEditEvents;
        private IPageUppdate pageUppdate;
        private IPulsUppdate pulsUppdate;
        private ListInTabsPresenter listInTabsPresenter;

        private XmlSerializer formatter = new XmlSerializer(typeof(TabsModel));

        public event Action<TabsModel> LoadTabsModal;

        public TabInListPresenter(ITablatureTextView tablatureTextView, IButtonTabsEditEvents buttonTabsEditEvents, TabsModel tabsModel, IPageUppdate pageUppdate, IPulsUppdate pulsUppdate, ListInTabsPresenter listInTabsPresenter)
        {
            this.tabsModel = tabsModel;
            this.tablatureTextView = tablatureTextView;
            this.buttonTabsEditEvents = buttonTabsEditEvents;
            this.pageUppdate = pageUppdate;
            this.pulsUppdate = pulsUppdate;
            this.listInTabsPresenter = listInTabsPresenter;
            buttonTabsEditEvents.ButNewMysikEvent += ButtonTabsEditEvents_ButNewMysikEvent;
            buttonTabsEditEvents.ButAddTabsEvent += ButtonTabsEditEvents_ButAddTabsEvent;
            buttonTabsEditEvents.ButClearEvent += ButtonTabsEditEvents_ButClearEvent;
            buttonTabsEditEvents.ButRefreshTabsEvent += ButtonTabsEditEvents_ButRefreshTabsEvent;
            buttonTabsEditEvents.ButRemoteTabsEvent += ButtonTabsEditEvents_ButRemoteTabsEvent;
            buttonTabsEditEvents.SelectNumTabsEvent += ButtonTabsEditEvents_SelectNumTabsEvent;
            buttonTabsEditEvents.ButSaveallTabsEvent += ButtonTabsEditEvents_ButSaveallTabsEvent;
            buttonTabsEditEvents.OpenTabsEvent += ButtonTabsEditEvents_OpenTabsEvent;
            buttonTabsEditEvents.EditPulseEvent += ButtonTabsEditEvents_EditPulseEvent;

            pageUppdate.NumericPageMin = 0;
            pageUppdate.NumericPageValue = 0;
            pageUppdate.NumericPageMax = 0;

            pageUppdate.NumericEnable = true;
            pulsUppdate.PulseEnable = true;
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
            tabsModel.Pulse = 1 / ((double.TryParse((sender as TextBox).Text, out double n) ? n : 0) / 60) / 2;
        }

        private void ButtonTabsEditEvents_ButNewMysikEvent(object sender, EventArgs e)
        {
            tabsModel.tabs = new List<List<TabModel>>();
            pageUppdate.NumericPageMin = 0;
            pageUppdate.NumericPageMax = 0;
            pageUppdate.NumericPageValue = 0;
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
            pageUppdate.NumericPageMax = tabsModel.tabs.Count / 32;
            pageUppdate.NumericPageMin = 1;
            pageUppdate.NumericPageValue = pageUppdate.NumericPageMax;
        }

        private void ButtonTabsEditEvents_SelectNumTabsEvent(object sender, EventArgs e)
        {
            try
            {
                if (pageUppdate.NumericPageValue != 0)
                {
                    listInTabsPresenter.ShowTabPage((int)(sender as NumericUpDown).Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ButtonTabsEditEvents_ButRemoteTabsEvent(object sender, EventArgs e)
        {
            if (pageUppdate.NumericPageValue <= tabsModel.tabs.Count / 32)
            {
                for (int i = pageUppdate.NumericPageValue * 32 - 32; i < pageUppdate.NumericPageValue * 32; i++)
                {
                    tabsModel.tabs.RemoveAt(pageUppdate.NumericPageValue * 32 - 32);
                }
                pageUppdate.NumericPageMax = tabsModel.tabs.Count / 32;
                listInTabsPresenter.ShowTabPage(pageUppdate.NumericPageValue);
            }
        }

        private void ButtonTabsEditEvents_ButRefreshTabsEvent(object sender, EventArgs e)
        {
            if (pageUppdate.NumericPageValue <= tabsModel.tabs.Count / 32)
            {
                for (int i = pageUppdate.NumericPageValue * 32 - 32; i < pageUppdate.NumericPageValue * 32; i++)
                {
                    List<TabModel> lad = new List<TabModel>();

                    for (int j = 0; j < 6; j++)
                    {
                        if (int.TryParse(tablatureTextView.Texttabs[j, i - ((pageUppdate.NumericPageValue - 1) * 32)].Text, out int tab) && (tab >= 0 && tab <= 28))
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

                if (tabsModel.tabs.Count > 0)
                {
                    pulsUppdate.PulsePlay = 60 / (tabsModel.Pulse * 2);
                    pageUppdate.NumericPageMin = 1;
                    pageUppdate.NumericPageMax = tabsModel.tabs.Count / 32;
                    LoadTabsModal?.Invoke(tabsModel);
                    MessageBox.Show("Данные загружены");
                }
                else
                {
                    MessageBox.Show("Данные не найдены");
                }
            }
        }
    }
}