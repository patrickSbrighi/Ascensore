using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Ascensore
{
    public class Uomo
    {
        public Uomo(Image immagine, bool occupato = false)
        {
            Immagine = immagine;
            Occupato = occupato;
        }

        public Image Immagine { get; set; }
        public bool Occupato { get; set; }

        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }
}
