using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Views
{
    public interface IPulsUppdate
    {
        double PulsePlay { get; set; }
        bool PulseEnable { get; set; }
    }
}