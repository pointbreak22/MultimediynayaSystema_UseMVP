using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchPad
{
    public class InitPlugin
    {
        public string PluginName { get { return "Лаунчпад"; } }

        public void Show()
        {
            Form1 form1 = new Form1();
            form1.Text = PluginName;
            form1.Show();
        }
    }
}