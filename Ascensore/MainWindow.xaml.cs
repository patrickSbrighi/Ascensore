using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Drawing;

namespace Ascensore
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Inizializzazione di tutto
            InitializeComponent();
            semaforo = new Semaphore(0, 1);
            semaforo.Release(1);
            pianoScelto = int.MinValue;
            AbilitaTastiera(false);
            uomoInMovimento = int.MinValue;
            posUomini = InizializzaValori();
            richiestaPiani = new List<int>();
            pianoAttuale = int.MinValue;
            personeDentro = 0;

            _ascensore = new Ascensore(0, 277, 249);
            _piani = new Dictionary<int, Piano>();
            CreaPiani();
            CreaUomini();
        }

        private Semaphore semaforo;
        private int controlloStop;//valore di bottom fino al quale si deve muovere
        private int pianoScelto;//piano scelto dopo essere saliti sull'ascensore
        private const int LEFT_UOMO = 439;//Constante che indica la left dell'uomo
        private const int RIGHT_UOMO = 315;//Constante che indica la right dell'uomo
        private int uomoInMovimento;//Uomo che si sta muovendo
        private Dictionary<int, int[]> posUomini;
        private int topUomo;//top degli uomini
        private int bottomUomo;//bottom degli uomini
        private int pianoAttuale;//piano dove si trova l'ascensore
        private int personeDentro;//il numero di persone dentro l'ascensore

        private List<int> richiestaPiani;//piani a cui è stato richiesto l'ascensore;

        private Ascensore _ascensore;
        private Dictionary<int, Piano> _piani;
        private List<Uomo> _uomini;
        private List<Richiesta> _richieste;
        private Piano _actualPiano;
        private void CreaPiani()
        {
            _piani.Add(-2, new Piano(-2, 59));

            _piani.Add(-1, new Piano(-1, 169));

            _piani.Add(1, new Piano(1, 274));

            _piani.Add(2, new Piano(2, 380));

            _piani.Add(3, new Piano(3, 488));
        }

        private void CreaUomini()
        {
            _uomini.Add(new Uomo(uomo1, false));
            _uomini.Add(new Uomo(uomo2, false));
            _uomini.Add(new Uomo(uomo3, false));
            _uomini.Add(new Uomo(uomo_1, false));
            _uomini.Add(new Uomo(uomo_2, false));
        }

        private Dictionary<int, int[]> InizializzaValori()
        {
            int[] pos = new int[4];
            //int[] pos = new int[4];
            Dictionary<int, int[]> toReturn = new Dictionary<int, int[]>();

            //uomo 1
            pos[0] = 531;
            pos[1] = 316;
            pos[2] = 225;
            pos[3] = 279;
            toReturn.Add(1, pos);

            //uomo 2
            pos = new int[4];
            pos[0] = 531;
            pos[1] = 216;
            pos[2] = 225;
            pos[3] = 385;
            toReturn.Add(2, pos);

            //uomo 3
            pos = new int[4];
            pos[0] = 531;
            pos[1] = 112;
            pos[2] = 225;
            pos[3] = 493;
            toReturn.Add(3, pos);

            //uomo -1
            pos = new int[4];
            pos[0] = 531;
            pos[1] = 433;
            pos[2] = 225;
            pos[3] = 171;
            toReturn.Add(-1, pos);

            //uomo -2
            pos = new int[4];
            pos[0] = 531;
            pos[1] = 543;
            pos[2] = 225;
            pos[3] = 64;
            toReturn.Add(-2, pos);

            return toReturn;
        }

        //Rende opaco l'uomo
        /*private void OpacaUomo(int n, bool opaca)
        {
            if (opaca)
            {
                if (uomoInMovimento == 1)
                {
                    uomo1.Opacity = 0;
                }
                else if (uomoInMovimento == 2)
                {
                    uomo2.Opacity = 0;
                }
                else if (uomoInMovimento == 3)
                {
                    uomo3.Opacity = 0;
                }
                else if (uomoInMovimento == -1)
                {
                    uomo_1.Opacity = 0;
                }
                else if (uomoInMovimento == -2)
                {
                    uomo_2.Opacity = 0;
                }
            }
            else
            {
                if (uomoInMovimento == 1)
                {
                    uomo1.Opacity = 100;
                }
                else if (uomoInMovimento == 2)
                {
                    uomo2.Opacity = 100;
                }
                else if (uomoInMovimento == 3)
                {
                    uomo3.Opacity = 100;
                }
                else if (uomoInMovimento == -1)
                {
                    uomo_1.Opacity = 100;
                }
                else if (uomoInMovimento == -2)
                {
                    uomo_2.Opacity = 100;
                }
            }
            
        }*/

        //Metodo del movimento generale, ascensore, uomini, apparizione e sparizione
        private void AscensorePiano()
        {
            if (!_richieste[0].PresoUomo)
            {
                if (_ascensore.ActualBottom < controlloStop)//se l'ascensore è più in altro
                {
                    while (_ascensore.ActualBottom < controlloStop)
                    {
                        _ascensore.ActualBottom += 2;
                        _ascensore.ActualTop -= 2;
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, _ascensore.ActualTop, 290, _ascensore.ActualBottom);
                        }));
                    }
                }
                else//se l'ascensore è più in basso
                {
                    while (_ascensore.ActualBottom > controlloStop)
                    {
                        _ascensore.ActualBottom -= 2;
                        _ascensore.ActualTop += 2;
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, _ascensore.ActualTop, 290, _ascensore.ActualBottom);
                        }));
                    }
                }

                MuoviLateralmenteUomo(_richieste[0].Uomo.Left, _richieste[0].Uomo.Right, true);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    AbilitaTastiera(true);
                }));
            }
            else//l'ascensore è pieno
            {
                //copre l'uomo
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _richieste[0].Uomo.Immagine.Opacity = 0;
                }));

                //Toglie la possibilità di usare la tastiers
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    AbilitaTastiera(false);
                }));


                if (_ascensore.ActualBottom < controlloStop)//l'uomo sta entrando
                {
                    while (_ascensore.ActualBottom < controlloStop)
                    {
                        _richieste[0].Uomo.Bottom += 2;
                        _richieste[0].Uomo.Top -= 2;
                        _ascensore.ActualBottom += 2;
                        _ascensore.ActualTop -= 2;
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, _ascensore.ActualTop, 290, _ascensore.ActualBottom);
                        }));

                    }

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _richieste[0].Uomo.Immagine.Opacity = 100;
                        _richieste[0].Uomo.Immagine.Margin = new Thickness(LEFT_UOMO, _richieste[0].Uomo.Top, RIGHT_UOMO, _richieste[0].Uomo.Bottom);
                    }));

                    MuoviLateralmenteUomo(LEFT_UOMO, RIGHT_UOMO, false);
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                }
                else //l'uomo deve uscire
                {

                    while (_ascensore.ActualBottom > controlloStop)//Ascensore va nel piano scelto
                    {
                        _ascensore.ActualBottom -= 2;
                        _ascensore.ActualTop += 2;
                        _richieste[0].Uomo.Bottom -= 2;
                        _richieste[0].Uomo.Top += 2;
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, _ascensore.ActualTop, 290, _ascensore.ActualBottom);
                        }));
                    }

                    //appare l'uomo coperto
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _richieste[0].Uomo.Immagine.Opacity = 100;
                        _richieste[0].Uomo.Immagine.Margin = new Thickness(LEFT_UOMO, _richieste[0].Uomo.Top, RIGHT_UOMO, _richieste[0].Uomo.Bottom);//viene messo nella posizione dell'ascensore
                    }));

                    MuoviLateralmenteUomo(LEFT_UOMO, RIGHT_UOMO, false);//l'uomo esce
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                };

                ProssimoPianoDaAndare();
                pianoScelto = int.MinValue;
                //uomoInMovimento = int.MinValue;
            }
        }

        //Metodo che fa muovere l'uomo a destra o sinistra
        private void MuoviLateralmenteUomo(int left, int right, bool sinistra)
        {
            if (sinistra)
            {
                while (_richieste[0].Uomo.Left > LEFT_UOMO)
                {
                    _richieste[0].Uomo.Left -= 5;
                    _richieste[0].Uomo.Right += 5;
                    Thread.Sleep(TimeSpan.FromMilliseconds(50));

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _richieste[0].Uomo.Immagine.Margin = new Thickness(_richieste[0].Uomo.Left, 316, _richieste[0].Uomo.Right, 279);

                    }));
                }
            }
            else
            {
                while (_richieste[0].Uomo.Left < 614)//valore di defalut
                {
                    _richieste[0].Uomo.Left += 5;
                    _richieste[0].Uomo.Right -= 5;
                    Thread.Sleep(TimeSpan.FromMilliseconds(50));

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _richieste[0].Uomo.Immagine.Margin = new Thickness(_richieste[0].Uomo.Left, topUomo, _richieste[0].Uomo.Right, bottomUomo);
                    }));
                }

                //Copre l'uomo
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _richieste[0].Uomo.Immagine.Opacity = 0;
                }));
            }
        }

        private void piano1_Click(object sender, RoutedEventArgs e)
        {
            int indiceImmagine = -1;

            for (int i = 0; i < _uomini.Count; i++)
            {
                if (!_uomini[i].Occupato)
                {
                    _uomini[i].Immagine.Opacity = 100;
                    _uomini[i].Occupato = true;
                    indiceImmagine = i;
                    break;
                }
            }

            if (indiceImmagine > -1)
            {
                _uomini[indiceImmagine].Immagine.Margin = new Thickness(posUomini[1][0], posUomini[1][1], posUomini[1][2], posUomini[1][3]);

                _uomini[indiceImmagine].Left = posUomini[1][0];
                _uomini[indiceImmagine].Top = posUomini[1][1];
                _uomini[indiceImmagine].Left = posUomini[1][2];
                _uomini[indiceImmagine].Left = posUomini[1][3];

                Richiesta r = new Richiesta(_piani[1], _uomini[indiceImmagine], 0);
                _richieste.Add(r);

                if (pianoAttuale == int.MinValue)
                {
                    ProssimoPianoDaAndare();
                    pianoAttuale = 1;
                }
            }
            else
            {
                MessageBox.Show("Tutti gli uomini sono occupati");
            }
        }

        private void Partenza()
        {
            semaforo.WaitOne();

            if (_richieste[0].PresoUomo == false)
            {
                controlloStop = _richieste[0].PianoIniziale.Bottom;
                _actualPiano = _richieste[0].PianoIniziale;
            }
            else
            {
                controlloStop = _richieste[0].PianoArrivo.Bottom;
                _actualPiano = _richieste[0].PianoArrivo;
            }

            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();

            if (_richieste[0].PresoUomo == false)
            {
                while (pianoScelto == int.MinValue)
                    Thread.Sleep(100);
                _richieste[0].PianoArrivo = _piani[pianoScelto];
                _richieste[0].PresoUomo = true;
            }
            else
            {
                _richieste.RemoveAt(0);
            }

            semaforo.Release();
        }

        /*private void Thread1()//Metodo del movimeto piano1
        {
            semaforo.WaitOne();
            controlloStop = 274;

            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();

            while (pianoScelto == int.MinValue)
                Thread.Sleep(100);
            AscensorePiano();
            personeDentro--;
            semaforo.Release();

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                piano1.IsEnabled = true;
            }));
        }*/

        private void piano2_Click(object sender, RoutedEventArgs e)
        {
            int indiceImmagine = -1;

            for (int i = 0; i < _uomini.Count; i++)
            {
                if (!_uomini[i].Occupato)
                {
                    _uomini[i].Immagine.Opacity = 100;
                    _uomini[i].Occupato = true;
                    indiceImmagine = i;
                    break;
                }
            }

            if (indiceImmagine > -1)
            {
                _uomini[indiceImmagine].Immagine.Margin = new Thickness(posUomini[2][0], posUomini[2][1], posUomini[2][2], posUomini[2][3]);

                _uomini[indiceImmagine].Left = posUomini[2][0];
                _uomini[indiceImmagine].Top = posUomini[2][1];
                _uomini[indiceImmagine].Left = posUomini[2][2];
                _uomini[indiceImmagine].Left = posUomini[2][3];

                Richiesta r = new Richiesta(_piani[2], _uomini[indiceImmagine], 0);
                _richieste.Add(r);

                if (pianoAttuale == int.MinValue)
                {
                    ProssimoPianoDaAndare();
                    pianoAttuale = 2;
                }
            }
            else
            {
                MessageBox.Show("Tutti gli uomini sono occupati");
            }
        }

        /*private void Thread2()//Metodo del movimento piano2
        {
            semaforo.WaitOne();
            uomoInMovimento = 2;
            controlloStop = 380;

            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();

            t1.Join();
            while (pianoScelto == int.MinValue)
                Thread.Sleep(100);
            AscensorePiano();
            personeDentro--;
            semaforo.Release();

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                piano2.IsEnabled = true;
            }));
        }*/

        private void piano3_Click(object sender, RoutedEventArgs e)
        {
            int indiceImmagine = -1;

            for (int i = 0; i < _uomini.Count; i++)
            {
                if (!_uomini[i].Occupato)
                {
                    _uomini[i].Immagine.Opacity = 100;
                    _uomini[i].Occupato = true;
                    indiceImmagine = i;
                    break;
                }
            }

            if (indiceImmagine > -1)
            {
                _uomini[indiceImmagine].Immagine.Margin = new Thickness(posUomini[3][0], posUomini[3][1], posUomini[3][2], posUomini[3][3]);

                _uomini[indiceImmagine].Left = posUomini[3][0];
                _uomini[indiceImmagine].Top = posUomini[3][1];
                _uomini[indiceImmagine].Left = posUomini[3][2];
                _uomini[indiceImmagine].Left = posUomini[3][3];

                Richiesta r = new Richiesta(_piani[3], _uomini[indiceImmagine], 0);
                _richieste.Add(r);

                if (pianoAttuale == int.MinValue)
                {
                    ProssimoPianoDaAndare();
                    pianoAttuale = 3;
                }
            }
            else
            {
                MessageBox.Show("Tutti gli uomini sono occupati");
            }
        }


        /*private void Thread3()//Metodon del movimento piano3
        {
            semaforo.WaitOne();
            uomoInMovimento = 3;
            controlloStop = 488;

            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();

            while (pianoScelto == int.MinValue)
                Thread.Sleep(100);
            AscensorePiano();
            personeDentro--;
            semaforo.Release();

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                piano3.IsEnabled = true;
            }));
        }*/

        private void piano_2_Click(object sender, RoutedEventArgs e)
        {
            int indiceImmagine = -1;

            for (int i = 0; i < _uomini.Count; i++)
            {
                if (!_uomini[i].Occupato)
                {
                    _uomini[i].Immagine.Opacity = 100;
                    _uomini[i].Occupato = true;
                    indiceImmagine = i;
                    break;
                }
            }

            if (indiceImmagine > -1)
            {
                _uomini[indiceImmagine].Immagine.Margin = new Thickness(posUomini[-2][0], posUomini[-2][1], posUomini[-2][2], posUomini[-2][3]);

                _uomini[indiceImmagine].Left = posUomini[-2][0];
                _uomini[indiceImmagine].Top = posUomini[-2][1];
                _uomini[indiceImmagine].Left = posUomini[-2][2];
                _uomini[indiceImmagine].Left = posUomini[-2][3];

                Richiesta r = new Richiesta(_piani[-2], _uomini[indiceImmagine], 0);
                _richieste.Add(r);

                if (pianoAttuale == int.MinValue)
                {
                    ProssimoPianoDaAndare();
                    pianoAttuale = -2;
                }
            }
            else
            {
                MessageBox.Show("Tutti gli uomini sono occupati");
            }
        }

        /*private void ThreadM2() //Metodo del movimento piano-2
        {
            semaforo.WaitOne();
            uomoInMovimento = -2;
            controlloStop = 59;

            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();

            while (pianoScelto == int.MinValue)
                Thread.Sleep(100);
            AscensorePiano();
            personeDentro--;
            semaforo.Release();

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                piano_2.IsEnabled = true;
            }));
        }*/

        private void piano_1_Click(object sender, RoutedEventArgs e)
        {
            int indiceImmagine = -1;

            for (int i = 0; i < _uomini.Count; i++)
            {
                if (!_uomini[i].Occupato)
                {
                    _uomini[i].Immagine.Opacity = 100;
                    _uomini[i].Occupato = true;
                    indiceImmagine = i;
                    break;
                }
            }

            if (indiceImmagine > -1)
            {
                _uomini[indiceImmagine].Immagine.Margin = new Thickness(posUomini[-1][0], posUomini[-1][1], posUomini[-1][2], posUomini[-1][3]);

                _uomini[indiceImmagine].Left = posUomini[-1][0];
                _uomini[indiceImmagine].Top = posUomini[-1][1];
                _uomini[indiceImmagine].Left = posUomini[-1][2];
                _uomini[indiceImmagine].Left = posUomini[-1][3];

                Richiesta r = new Richiesta(_piani[-1], _uomini[indiceImmagine], 0);
                _richieste.Add(r);

                if (pianoAttuale == int.MinValue)
                {
                    ProssimoPianoDaAndare();
                    pianoAttuale = -1;
                }
            }
            else
            {
                MessageBox.Show("Tutti gli uomini sono occupati");
            }

        }

        /*private void ThreadM1()//Metodo del movimento piano-1
        {
            semaforo.WaitOne();
            uomoInMovimento = -1;
            controlloStop = 169;

            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();

            while (pianoScelto == int.MinValue)
                Thread.Sleep(500);
            AscensorePiano();
            personeDentro--;
            semaforo.Release();

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                piano_1.IsEnabled = true;
            }));
        }*/

        //Metodo che stabilisce fino a dove è da portare una persona
        /*private void PortaPersona()
        {
            if (pianoScelto == 3)
            {
                controlloStop = 488;
            }
            else if (pianoScelto == 2)
            {
                controlloStop = 380;
            }
            else if (pianoScelto == 1)
            {
                controlloStop = 274;
            }
            else if (pianoScelto == -1)
            {
                controlloStop = 169;
            }
            else if (pianoScelto == -2)
            {
                controlloStop = 59;
            }
        }*/

        //Metodo che definisce i valore di top e botton uomo per poterlo m ettere nell'ascensore all'uscita
        /*private void aletzzeUomoMovimento()
        {
            if (uomoInMovimento == 1)
            {
                topUomo = posUomini[1][1];
                bottomUomo = posUomini[1][3];
            }
            else if (uomoInMovimento == 2)
            {
                topUomo = posUomini[2][1];
                bottomUomo = posUomini[2][3];
            }
            else if (uomoInMovimento == 3)
            {
                topUomo = posUomini[3][1];
                bottomUomo = posUomini[3][3];
            }
            else if (uomoInMovimento == -1)
            {
                topUomo = posUomini[-1][1];
                bottomUomo = posUomini[-1][3];
            }
            else if (uomoInMovimento == -2)
            {
                topUomo = posUomini[-2][1];
                bottomUomo = posUomini[-2][3];
            }
        }*/


        //Metodo che abilita il tastierino
        private void AbilitaTastiera(bool abilita)
        {
            if (abilita)
            {
                Chiama1.IsEnabled = true;
                Chiama2.IsEnabled = true;
                Chiama3.IsEnabled = true;
                Chiama_2.IsEnabled = true;
                Chiama_1.IsEnabled = true;
            }
            else
            {
                Chiama1.IsEnabled = false;
                Chiama2.IsEnabled = false;
                Chiama3.IsEnabled = false;
                Chiama_2.IsEnabled = false;
                Chiama_1.IsEnabled = false;
            }
        }


        private void ProssimoPianoDaAndare()
        {
            int indice = -1;
            int differenza = int.MaxValue;

            for(int i = 0; i < _richieste.Count; i++)
            {
                _richieste[i].Balzi++;
                if (_richieste[i].PresoUomo && _richieste[i].Balzi < 3 )
                {
                    if (Math.Abs(_actualPiano.Numero - _richieste[i].PianoArrivo.Numero) < differenza)
                    {
                        differenza = Math.Abs(_actualPiano.Numero - _richieste[i].PianoArrivo.Numero);
                        indice = i;
                    }
                }
                else if(!_richieste[i].PresoUomo && _richieste[i].Balzi < 3)
                {
                    if (Math.Abs(_actualPiano.Numero - _richieste[i].PianoIniziale.Numero) < differenza)
                    {
                        differenza = Math.Abs(_actualPiano.Numero - _richieste[i].PianoArrivo.Numero);
                        indice = i;
                    }
                }
                else
                {
                    indice = i;
                    break;
                }
            }

            _richieste[indice].Balzi--;

            Richiesta r = _richieste[indice];
            _richieste[indice] = _richieste[0];
            _richieste[0] = _richieste[indice];

            Thread t = new Thread(new ThreadStart(Partenza));
            t.Start();
            t.Join();
        }

        /*private void PartiDaPiano()
        {
            if (richiestaPiani[0] == 1)
            {
                Thread t = new Thread(new ThreadStart(Thread1));
                t.Start();
            }
            else if (richiestaPiani[0] == 2)
            {
                Thread t = new Thread(new ThreadStart(Thread2));
                t.Start();
            }
            else if (richiestaPiani[0] == 3)
            {
                Thread t = new Thread(new ThreadStart(Thread3));
                t.Start();
            }
            else if (richiestaPiani[0] == -1)
            {
                Thread t = new Thread(new ThreadStart(ThreadM1));
                t.Start();
            }
            else if (richiestaPiani[0] == -2)
            {
                Thread t = new Thread(new ThreadStart(ThreadM2));
                t.Start();
            }
        }*/

        //Chiamate dei vari piani da tastierino
        private void Chiama1_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = 1;
            //pianoAttuale = 1;
        }

        private void Chiama_1_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = -1;
            //pianoAttuale = -1;
        }

        private void Chiama_2_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = -2;
            //pianoAttuale = -2;
        }

        private void Chiama3_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = 3;
            //pianoAttuale = 3;
        }

        private void Chiama2_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = 2;
            //pianoAttuale = 2;
        }
    }
}
