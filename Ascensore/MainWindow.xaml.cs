﻿using System;
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
            bottom = 277;
            semaforo = new Semaphore(0, 1);
            semaforo.Release(1);
            top = 249;
            pianoScelto = int.MinValue;
            AbilitaTastiera(false);
            uomoInMovimento = int.MinValue;
            posUomini = InizializzaValori();
        }
        private int bottom;// 2 parametro movimento
        private Semaphore semaforo;
        private int top;//1 parametro movimento
        private int controlloStop;//valore di bottom fino al quale si deve muovere
        private int pianoScelto;//piano scelto dopo essere saliti sull'ascensore
        private const int LEFT_UOMO = 439;//Constante che indica la left dell'uomo
        private const int RIGHT_UOMO = 315;//Constante che indica la right dell'uomo
        private int uomoInMovimento;//Uomo che si sta muovendo
        private Dictionary<int, int[]> posUomini;
        
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
        
        private void OpacaUomo(int n, bool opaca)
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
        }
        
        //Metodo del movimento generale
        private void AscensorePiano()
        {
            if (pianoScelto == int.MinValue)
            {
                if (bottom < controlloStop)
                {
                    while (bottom < controlloStop)
                    {
                        bottom += 2;
                        top -= 2;
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, top, 290, bottom);
                        }));
                    }
                }
                else
                {
                    while (bottom > controlloStop)
                    {
                        bottom -= 2;
                        top += 2;
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, top, 290, bottom);
                        }));
                    }
                }
                MuoviDentroUomo();
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    AbilitaTastiera(true);
                }));
            }
            else
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    OpacaUomo(uomoInMovimento, true);
                }));
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    AbilitaTastiera(false);
                }));
                PortaPersona();

                if (bottom < controlloStop)
                {
                    while (bottom < controlloStop)
                    {

                        bottom += 2;
                        top -= 2;
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, top, 290, bottom);
                        }));
                        
                    }
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                }
                else
                {

                    while (bottom > controlloStop)
                    {
                        bottom -= 2;
                        top += 2;
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, top, 290, bottom);
                        }));
                    }
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                }
                /*MuoviFuoriUomo();*/
                pianoScelto = int.MinValue;
                uomoInMovimento = int.MinValue;
            }
        }

        private void MuoviDentroUomo()
        {
            int left = 531;
            int right = 225;

            while (left > LEFT_UOMO)
            {
                left -= 5;
                right += 5;
                Thread.Sleep(TimeSpan.FromMilliseconds(50));

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (uomoInMovimento == 1)
                    {
                        uomo1.Margin = new Thickness(left, 316, right,279);
                    }
                    else if (uomoInMovimento == 2)
                    {
                        uomo2.Margin = new Thickness(left, 216, right, 385);
                    }
                    else if (uomoInMovimento == 3)
                    {
                        uomo3.Margin = new Thickness(left, 112, right, 493);
                    }
                    else if (uomoInMovimento == -1)
                    {
                        uomo_1.Margin = new Thickness(left, 433, right, 171);
                    }
                    else if (uomoInMovimento == -2)
                    {
                        uomo_2.Margin = new Thickness(left, 543, right, 64);
                    }
                }));
            }
        }

        private void MuoviFuoriUomo()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                OpacaUomo(uomoInMovimento, false);
                if (uomoInMovimento == 1)
                {
                    uomo1.Margin = new Thickness(439, top, 315, controlloStop);
                }
                else if (uomoInMovimento == 2)
                {
                    uomo2.Margin = new Thickness(439, top, 315, controlloStop);
                }
                else if (uomoInMovimento == 3)
                {
                    uomo3.Margin = new Thickness(439, top, 315, controlloStop);
                }
                else if (uomoInMovimento == -1)
                {
                    uomo_1.Margin = new Thickness(439, top, 315, controlloStop);
                }
                else if (uomoInMovimento == -2)
                {
                    uomo_2.Margin = new Thickness(439, top, 315, controlloStop);
                }
            }));
            
        }


        private void piano1_Click(object sender, RoutedEventArgs e)
        {
            uomo1.Opacity = 100;
            uomo1.Margin = new Thickness(posUomini[1][0], posUomini[1][1], posUomini[1][2], posUomini[1][3]);
            Thread t = new Thread(new ThreadStart(Thread1));
            t.Start();
        }

        private void Thread1()//Metodo del movimeto piano1
        {
            semaforo.WaitOne();
            uomoInMovimento = 1;
            controlloStop = 274;
            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();
            while (pianoScelto == int.MinValue)
                Thread.Sleep(100);
            AscensorePiano();
            semaforo.Release();
        }

        private void piano2_Click(object sender, RoutedEventArgs e)
        {
            uomo2.Opacity = 100;
            uomo2.Margin = new Thickness(posUomini[2][0], posUomini[2][1], posUomini[2][2], posUomini[2][3]);
            Thread t = new Thread(new ThreadStart(Thread2));
            t.Start();
        }

        private void Thread2()//Metodo del movimento piano2
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
            semaforo.Release();
        }

        private void piano3_Click(object sender, RoutedEventArgs e)
        {
            uomo3.Opacity = 100;
            uomo3.Margin = new Thickness(posUomini[3][0], posUomini[3][1], posUomini[3][2], posUomini[3][3]);
            Thread t = new Thread(new ThreadStart(Thread3));
            t.Start();
        }

        
        private void Thread3()//Metodon del movimento piano3
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
            semaforo.Release();
        }

        private void piano_2_Click(object sender, RoutedEventArgs e)
        {
            uomo_2.Opacity = 100;
            uomo_2.Margin = new Thickness(posUomini[-2][0], posUomini[-2][1], posUomini[-2][2], posUomini[-2][3]);
            Thread t = new Thread(new ThreadStart(ThreadM2));
            t.Start();
        }

        private void ThreadM2() //Metodo del movimento piano-2
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
            semaforo.Release();
        }

        private void piano_1_Click(object sender, RoutedEventArgs e)
        {
            uomo_1.Opacity = 100;
            uomo_1.Margin = new Thickness(posUomini[-1][0], posUomini[-1][1], posUomini[-1][2], posUomini[-1][3]);
            Thread t = new Thread(new ThreadStart(ThreadM1));
            t.Start();
        }


        private void ThreadM1()//Metodo del movimento piano-1
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
            semaforo.Release();
        }

        //Metodo che stabilisce fino a dove è da portare una persona
        private void PortaPersona()
        {
            if (pianoScelto == 3)
            {
                controlloStop = 488;
            }
            else if (pianoScelto == 2)
            {
                controlloStop = 380;
            }
            else if(pianoScelto == 1)
            {
                controlloStop = 274;
            }
            else if(pianoScelto == -1)
            {
                controlloStop = 169;
            }
            else if(pianoScelto == -2)
            {
                controlloStop = 59;
            }
        }       


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

        private void Chiama1_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = 1;
        }

        private void Chiama_1_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = -1;
        }

        private void Chiama_2_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = -2;
        }

        private void Chiama3_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = 3;
        }

        private void Chiama2_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = 2;
        }
    }
}
