using Guitar.Models;
using Guitar.Presenter;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Views
{
    public partial class MainFormGuitar : Form, IButtonNeckView, IButtonDeckView, ITablatureTextView
    {
        public PictureBox[,] pictureButtonNecks { get; set; }
        public PictureBox[] pictureButtonDecks { get; set; }
        public TextBox[,] Texttabs { get; set; }
        private EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.AutoReset);

        public MainFormGuitar()
        {
            InitializeComponent();
            pictureButtonDecks = new PictureBox[6];
            pictureButtonNecks = new PictureBox[28, 6];
        }

        private StateGuitar stateGuitar;
        private StateGuitarPresenter stateGuitarPresenter;
        private PaintNeckModel paintNeckModel;
        private PaintDeckModel paintDeckModel;

        private void MainFormGuitar_Load(object sender, EventArgs e)
        {
            paintNeckModel = new PaintNeckModel();
            paintDeckModel = new PaintDeckModel();
            stateGuitar = new StateGuitar();
            stateGuitarPresenter = new StateGuitarPresenter(stateGuitar, this, this, paintDeckModel, paintNeckModel, ewh);
            ButtonNeckPresenter neckPresenter = new ButtonNeckPresenter(this, stateGuitarPresenter);
            ButtonDeckPresenter deckPresenter = new ButtonDeckPresenter(this, stateGuitarPresenter);
            TextTabsPresenter tabsPresenter = new TextTabsPresenter(this);
            neckPresenter.PaintButtonNeck(panel_grif);
            deckPresenter.PaintButtonDeck(panel_deg);
            tabsPresenter.PaintTabs(flowLayoutPanel1);
            stateGuitarPresenter.StartTread();
            stateGuitarPresenter.EventEditPicture += EditPicture;
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
            stateGuitarPresenter.SeachStareDispose();
        }
    }
}