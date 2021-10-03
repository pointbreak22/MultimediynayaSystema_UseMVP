using Guitar.Models;
using Guitar.Presenter;
using Guitar.Views;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Views
{
    public partial class MainFormGuitar : Form, IButtonNeckView, IButtonDeckView, ITablatureTextView, IKeysEvent, ISelectedMidi, IButtonTabsEditEvents, ITabsPlay, IPictureIn, IFormClosing, IPulsUppdate, IPageUppdate
    {
        public PictureBox[,] PictureButtonNecks { get; set; }
        public PictureBox[] PictureButtonDecks { get; set; }
        public TextBox[,] Texttabs { get; set; }

        public Panel PanelDeck { get { return panel_deg; } set { panel_deg = value; } }
        public Panel PanelNeck { get { return panel_grif; } set { panel_grif = value; } }
        public FlowLayoutPanel LayoutPanel { get { return flowLayoutPanel1; } set { flowLayoutPanel1 = value; } }
        public string SelectInstrument { get { return labelInstruments.Text; } set { labelInstruments.Text = value; } }

        public double PulsePlay { get { double.TryParse(textPulse.Text, out double n); return n; } set { textPulse.Text = value.ToString(); } }

        public int NumericPageValue
        {
            get
            {
                return (int)SelectNumTabs.Value;
            }
            set
            {
                if (SelectNumTabs.InvokeRequired)
                {
                    Action action = () => { SelectNumTabs.Value = value; };
                    Invoke(action);
                }
                else
                {
                    SelectNumTabs.Value = value;
                }
            }
        }

        public int NumericPageMin { get { return (int)SelectNumTabs.Minimum; } set { SelectNumTabs.Minimum = value; } }
        public int NumericPageMax { get { return (int)SelectNumTabs.Maximum; } set { SelectNumTabs.Maximum = value; } }

        public bool PulseEnable
        {
            get { return textPulse.Enabled; }
            set
            {
                if (SelectNumTabs.InvokeRequired)
                {
                    Action action = () => { SelectNumTabs.Enabled = value; };
                    Invoke(action);
                }
                else

                { textPulse.Enabled = value; }
            }
        }

        public bool NumericEnable
        {
            get { return SelectNumTabs.Enabled; }
            set
            {
                if (SelectNumTabs.InvokeRequired)
                {
                    Action action = () => { SelectNumTabs.Enabled = value; };
                    Invoke(action);
                }
                else
                { SelectNumTabs.Enabled = value; }
            }
        }

        public MainFormGuitar()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        // IKeysEvent
        public event KeyEventHandler KDown;

        public event KeyEventHandler KUp;

        public event KeyPressEventHandler KPress;

        //ISelectedMidi
        public event EventHandler ComboPlaysSelectedDropEvent;

        public event EventHandler ComboPlaysSelectedIndexEvent;

        public event EventHandler ValueChanged;

        public event EventHandler ComboGameModeSelectedIndexChanged;

        public event EventHandler ComboGameModeDropDown;

        //IButtonTabsEditEvents
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

        //IFormClosing
        public event EventHandler ClosingForm;

        private void MainFormGuitar_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosingForm?.Invoke(sender, e);
        }

        private void ComboPlays_DropDown(object sender, EventArgs e)
        {
            ComboPlaysSelectedDropEvent?.Invoke(sender, e);
        }

        private void ComboPlays_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboPlaysSelectedIndexEvent?.Invoke(sender, e);
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
            EditPulseEvent?.Invoke(sender, e);
        }

        private void ButPlayallTabs_Click(object sender, EventArgs e)
        {
            ButPlayallTabsEvent?.Invoke(sender, e);
        }

        private void ButStopAll_Click(object sender, EventArgs e)
        {
            ButStopAllEvent?.Invoke(sender, e);
        }

        private void MainFormGuitar_Load(object sender, EventArgs e)
        {
        }
    }
}