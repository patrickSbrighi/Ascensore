using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascensore
{
    class Ascensore
    {
        private int _nPersone;
        public int NPersone
        {
            get => _nPersone;
            set
            {
                if (value < 0)
                    throw new Exception("Persone non valide");
                _nPersone = value;
            }
        }

        public int ActualBottom { get; set; }
        public int ActualTop { get; set; }

        public Ascensore(int persone, int bottom, int top)
        {
            NPersone = persone;
            ActualBottom = bottom;
            ActualTop = top;
        }
    }
}
