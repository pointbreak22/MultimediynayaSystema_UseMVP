using Guitar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar
{
    public class InitPlugin
    {
        public string PluginName { get { return "Гитара"; } }

        public void Show()
        {
            StartApp startAp = new StartApp();
            startAp.Show(PluginName);
        }
    }
}