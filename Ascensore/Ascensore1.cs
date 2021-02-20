using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascensore
{
    public class Ascensore1
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

        public Ascensore1(int persone, int bottom, int top, List<Richiesta> richieste)
        {
            NPersone = persone;
            ActualBottom = bottom;
            ActualTop = top;
            Richieste = richieste;
        }

        public List<Richiesta> Richieste
        {
            get;
            set;
        }

        public Richiesta ProssimaDaFare(Piano actualPiano)
        {
            int indice = -1;
            int differenza = int.MaxValue;

            for (int i = 0; i < Richieste.Count; i++)
            {
                Richieste[i].Balzi++;
                if (Richieste[i].PresoUomo && Richieste[i].Balzi < 3)
                {
                    if (Math.Abs(actualPiano.Numero - Richieste[i].PianoArrivo.Numero) < differenza)
                    {
                        differenza = Math.Abs(actualPiano.Numero - Richieste[i].PianoArrivo.Numero);
                        indice = i;
                    }
                }
                else if (!Richieste[i].PresoUomo && Richieste[i].Balzi < 3)
                {
                    if (Math.Abs(actualPiano.Numero - Richieste[i].PianoIniziale.Numero) < differenza)
                    {
                        differenza = Math.Abs(actualPiano.Numero - Richieste[i].PianoIniziale.Numero);
                        indice = i;
                    }
                }
                else
                {
                    indice = i;
                    break;
                }
            }

            if (indice != -1)
            {
                Richiesta r = Richieste[indice];
                Richieste.RemoveAt(indice);
                return r;
            }
            else
                throw new Exception("Non ci sono richieste");
        }
    }
}
