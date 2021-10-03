using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Views
{
    public interface IPageUppdate
    {
        int NumericPageValue { get; set; }
        int NumericPageMin { get; set; }
        int NumericPageMax { get; set; }
        bool NumericEnable { get; set; }
    }
}