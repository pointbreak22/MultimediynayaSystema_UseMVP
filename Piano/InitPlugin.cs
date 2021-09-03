using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piano
{
    public class InitPlugin
    {
        public string PluginName { get { return "Пианино"; } }

        public void Show()
        {
            Form1 form1 = new Form1();
            form1.Text = PluginName;
            form1.Show();
        }
    }
}