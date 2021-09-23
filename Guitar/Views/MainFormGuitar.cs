using Guitar.Models;
using Guitar.Presenter;

using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Views
{
    public partial class MainFormGuitar : Form, IButtonNeckView, IButtonDeckView, ITablatureTextView, IKeysEvent, ISelectedMidi, IButtonTabsEditEvents, ITabsPlay
    {
        public PictureBox[,] PictureButtonNecks { get; set; }
        public PictureBox[] PictureButtonDecks { get; set; }
        public TextBox[,] Texttabs { get; set; }
        public string SelectInstrument { get { return labelInstruments.Text; } set { labelInstruments.Text = value; } }

        public int PageTab
        {
            get
            {
                return (int)SelectNumTabs.Value;
            }
            set
            {
                if (SelectNumTabs.InvokeRequired)
                {
                    Action action = () => SelectNumTabs.Value = value;
                    Invoke(action);
                }
                else
                {
                    SelectNumTabs.Value = value;
                }
            }
        }

        private readonly EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.AutoReset);

        public MainFormGuitar()
        {
            InitializeComponent();
            PictureButtonDecks = new PictureBox[6];
            PictureButtonNecks = new PictureBox[28, 6];

            KeyPreview = true;
        }

        private StateGuitar stateGuitar;
        private StateGuitarPresenter stateGuitarPresenter;
        private PaintNeckModel paintNeckModel;
        private PaintDeckModel paintDeckModel; private PlayMidiNotePresenter playMidiNotePresenter;

        public event EventHandler SelectedDropEvent;

        public event EventHandler SelectedIndexEvent;

        public event EventHandler PlayNoteEvent;

        public event EventHandler StopNoteEvent;

        public event KeyEventHandler KDown;

        public event KeyEventHandler KUp;

        public event KeyPressEventHandler KPress;

        public event EventHandler ComboPlaysSelectedDropEvent;

        public event EventHandler ComboPlaysSelectedIndexEvent;

        public event EventHandler ValueChanged;

        public event EventHandler SelectedIndexChanged;

        public event EventHandler comboGameModeDropDown;

        public event EventHandler ComboGameModeSelectedIndexChanged;

        // ButtonTabsEditEvents
        public event EventHandler ComboGameModeDropDown;

        public event EventHandler ButNewMysikEvent;

        public event EventHandler ButAddTabsEvent;

        public event EventHandler ButClearEvent;

        public event EventHandler SelectNumTabsEvent;

        public event EventHandler ButRefreshTabsEvent;

        public event EventHandler ButRemoteTabsEvent;

        public event EventHandler ButSaveallTabsEvent;

        public event EventHandler OpenTabsEvent;

        public event EventHandler EditPulseEvent;

        //ITabsPlay
        public event EventHandler ButPlayallTabsEvent;

        public event EventHandler ButStopAllEvent;

        private TabsModel tabsModel;
        private PlayTabsPresenter playTabsPresenter;
        private TabInListPresenter tabInListPresenter;

        private void MainFormGuitar_Load(object sender, EventArgs e)
        {
            paintNeckModel = new PaintNeckModel();
            paintDeckModel = new PaintDeckModel();
            stateGuitar = new StateGuitar();
            stateGuitarPresenter = new StateGuitarPresenter(stateGuitar, stateGuitar, this, this, paintDeckModel, paintNeckModel, ewh);
            ButtonNeckPresenter neckPresenter = new ButtonNeckPresenter(this, stateGuitarPresenter);
            ButtonDeckPresenter deckPresenter = new ButtonDeckPresenter(this, stateGuitarPresenter);
            TextTabsPresenter tabsPresenter = new TextTabsPresenter(this);
            neckPresenter.PaintButtonNeck(panel_grif);
            deckPresenter.PaintButtonDeck(panel_deg);
            tabsPresenter.PaintTabs(flowLayoutPanel1);

            stateGuitarPresenter.EventEditPicture += EditPicture;
            MidiModel midiModel = new MidiModel();
            playMidiNotePresenter = new PlayMidiNotePresenter(midiModel, stateGuitar, stateGuitar, ewh);
            KeyDeckPresenter keyDeckPresenter = new KeyDeckPresenter(this, stateGuitar);
            ModePlayPresenter modePlay = new ModePlayPresenter(this, midiModel);
            tabsModel = new TabsModel();
            tabInListPresenter = new TabInListPresenter(this, this, tabsModel);
            tabInListPresenter.LoadTabsModal += TabInListPresenter_LoadTabsModal;
            playTabsPresenter = new PlayTabsPresenter(this, this, tabsModel, stateGuitar);

            playMidiNotePresenter.StartTread();
            stateGuitarPresenter.StartTread();
        }

        private void TabInListPresenter_LoadTabsModal(TabsModel obj)
        {
            tabsModel = obj;
            playTabsPresenter = new PlayTabsPresenter(this, this, tabsModel, stateGuitar);
        }

        private void EditPicture(PictureBox pictureBox, Bitmap bitmap)
        {
            Action action = async () =>
            {
                await Task.Run(() => pictureBox.Image = bitmap);
            };
            Invoke(action);
        }

        private void MainFormGuitar_FormClosing(object sender, FormClosingEventArgs e)
        {
            stateGuitarPresenter.SeachStateDispose();
            playMidiNotePresenter.SeachStateDispose();
        }

        private void ComboPlays_DropDown(object sender, EventArgs e)
        {
            ComboPlaysSelectedDropEvent?.Invoke(sender, e);
        }

        private void ComboPlays_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboPlaysSelectedIndexEvent?.Invoke(sender, e);
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            PlayNoteEvent?.Invoke(sender, e);
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            StopNoteEvent?.Invoke(sender, e);
        }

        private void MainFormGuitar_KeyDown(object sender, KeyEventArgs e)
        {
            KDown?.Invoke(sender, e);
        }

        private void MainFormGuitar_KeyUp(object sender, KeyEventArgs e)
        {
            KUp?.Invoke(sender, e);
        }

        private void MainFormGuitar_KeyPress(object sender, KeyPressEventArgs e)
        {
            KPress?.Invoke(sender, e);
        }

        private void numericUpDownSelected_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }

        private void comboGameMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboGameModeSelectedIndexChanged?.Invoke(sender, e);
        }

        private void comboGameMode_DropDown(object sender, EventArgs e)
        {
            comboGameModeDropDown?.Invoke(sender, e);
        }

        private void ButNewMysik_Click(object sender, EventArgs e)
        {
            ButNewMysikEvent?.Invoke(sender, e);
        }

        private void ButAddTabs_Click(object sender, EventArgs e)
        {
            ButAddTabsEvent?.Invoke(sender, e);
        }

        private void ButClear_Click(object sender, EventArgs e)
        {
            ButClearEvent?.Invoke(sender, e);
        }

        private void SelectNumTabs_ValueChanged(object sender, EventArgs e)
        {
            SelectNumTabsEvent?.Invoke(sender, e);
        }

        private void ButRefreshTabs_Click(object sender, EventArgs e)
        {
            ButRefreshTabsEvent?.Invoke(sender, e);
        }

        private void ButRemoteTabs_Click(object sender, EventArgs e)
        {
            ButRemoteTabsEvent?.Invoke(sender, e);
        }

        private void ButSaveallTabs_Click(object sender, EventArgs e)
        {
            ButSaveallTabsEvent?.Invoke(sender, e);
        }

        private void OpenTabs_Click(object sender, EventArgs e)
        {
            OpenTabsEvent?.Invoke(sender, e);
        }

        private void textPulse_TextChanged(object sender, EventArgs e)
        {
            EditPulseEvent.Invoke(sender, e);
        }

        private void ButPlayallTabs_Click(object sender, EventArgs e)
        {
            ButPlayallTabsEvent.Invoke(sender, e);
        }

        private void ButStopAll_Click(object sender, EventArgs e)
        {
            ButStopAllEvent.Invoke(sender, e);
        }
    }
}