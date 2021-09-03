﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guitar.Views
{
    public class TablatureTextView
    {
        public TextBox textTabs;

        public TablatureTextView()
        {
            textTabs = new TextBox();
            textTabs.Font = new Font("Microsoft Sans Serif", 7.2f, FontStyle.Bold);
            textTabs.Size = new Size(16, 4);
            textTabs.BackColor = Color.Black;
            textTabs.ForeColor = Color.White;
        }
    }
}