using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ascensore
{   
    public class Piano
    {
        public int Numero
        {
            get;
            set;
        }

        public int Bottom
        {
            get;
            set;
        }

        public int Top { get; set; }

        public Piano(int numero, int bottom)
        {
            Numero = numero;
            Bottom = bottom;
        }
    }
}
