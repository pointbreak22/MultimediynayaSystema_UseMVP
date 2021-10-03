using Guitar.Models;
using Guitar.Presenter;
using Guitar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Guitar
{
    internal class StartApp
    {
        private MainFormGuitar mainForm;
        private PaintNeckModel paintNeckModel;
        private PaintDeckModel paintDeckModel;
        private StateGuitar stateGuitar;
        private StateGuitarPresenter stateGuitarPresenter;
        private ButtonNeckPresenter neckPresenter;
        private ButtonDeckPresenter deckPresenter;
        private TextTabsPresenter tabsPresenter;
        private MidiModel midiModel;
        private PlayMidiNotePresenter playMidiNotePresenter;
        private KeyDeckPresenter keyDeckPresenter;
        private ModePlayPresenter modePlay;
        private TabsModel tabsModel;
        private TabInListPresenter tabInListPresenter;
        private PlayTabsPresenter playTabsPresenter;
        private ListInTabsPresenter listInTabsPresenter;
        private ClosingPresenter closingPresenter;
        private readonly EventWaitHandle ewh;

        public StartApp()
        {
            mainForm = new MainFormGuitar();
            paintNeckModel = new PaintNeckModel();
            paintDeckModel = new PaintDeckModel();
            stateGuitar = new StateGuitar();
            ewh = new EventWaitHandle(false, EventResetMode.AutoReset);
            stateGuitarPresenter = new StateGuitarPresenter(stateGuitar, stateGuitar, mainForm, mainForm, paintDeckModel, paintNeckModel, ewh);
            neckPresenter = new ButtonNeckPresenter(mainForm, stateGuitarPresenter, mainForm);
            deckPresenter = new ButtonDeckPresenter(mainForm, stateGuitarPresenter, mainForm);
            tabsPresenter = new TextTabsPresenter(mainForm, mainForm);
            midiModel = new MidiModel();

            keyDeckPresenter = new KeyDeckPresenter(mainForm, stateGuitar);
            modePlay = new ModePlayPresenter(mainForm, midiModel);
            tabsModel = new TabsModel();

            listInTabsPresenter = new ListInTabsPresenter(tabsModel, mainForm);
            tabInListPresenter = new TabInListPresenter(mainForm, mainForm, tabsModel, mainForm, mainForm, listInTabsPresenter);
            playTabsPresenter = new PlayTabsPresenter(mainForm, mainForm, tabsModel, stateGuitar, mainForm, mainForm, listInTabsPresenter);

            playMidiNotePresenter = new PlayMidiNotePresenter(midiModel, stateGuitar, stateGuitar, ewh);
            closingPresenter = new ClosingPresenter(mainForm, midiModel, stateGuitarPresenter, playMidiNotePresenter);
            tabInListPresenter.LoadTabsModal += TabInListPresenter_LoadTabsModal;
            playMidiNotePresenter.StartTread();
            stateGuitarPresenter.StartTread();
        }

        private void TabInListPresenter_LoadTabsModal(TabsModel tabsModel)
        {
            playTabsPresenter.TabsModel = tabsModel;
            listInTabsPresenter.TabsModel = tabsModel;
        }

        public void Show(string PluginName)
        {
            mainForm.Text = PluginName;
            mainForm.Show();
        }
    }
}