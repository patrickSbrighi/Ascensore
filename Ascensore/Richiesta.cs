using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ascensore
{
    public class Richiesta
    {
        public Piano PianoIniziale
        {
            get;
            set;
        }

        public Piano PianoArrivo
        {
            get;
            set;
        }

        public Image Uomo { get; set; }

        private int _balzi;
        public int Balzi
        {
            get => _balzi;
            set
            {
                if (value < 0)
                    throw new Exception("Balzi non validi");
                _balzi = value;
            }
        }

        public Richiesta(Piano inizio, Image uomo, int balzi)
        {
            PianoIniziale = inizio
            Uomo = uomo;
            Balzi = balzi;
        }
    }
}
