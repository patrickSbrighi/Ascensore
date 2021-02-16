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
            bottom = 277;
            semaforo = new Semaphore(0, 1);
            semaforo.Release(1);
            top = 249;
            pianoScelto = int.MinValue;
            AbilitaTastiera(false);
            ImmaginiOpache();
        }
        private int bottom;// 2 parametro movimento
        private Semaphore semaforo;
        private int top;//1 parametro movimento
        private int controlloStop;//valore di bottom fino al quale si deve muovere
        private int pianoScelto;//piano scelto dopo essere saliti sull'ascensore
        
        
        //Reende opache le immagini
        private void ImmaginiOpache()
        {
            uomo1.Opacity = 0;
            uomo2.Opacity = 0;
            uomo3.Opacity = 0;
            uomo_1.Opacity = 0;
            uomo_2.Opacity = 0;
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
                        bottom += 5;
                        top -= 5;
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));

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
                        bottom -= 5;
                        top += 5;
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, top, 290, bottom);
                        }));
                    }
                }
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    AbilitaTastiera(true);
                }));
            }
            else
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    AbilitaTastiera(false);
                }));
                PortaPersona();
                if (bottom < controlloStop)
                {
                    while (bottom < controlloStop)
                    {
                        bottom += 5;
                        top -= 5;
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));

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
                        bottom -= 5;
                        top += 5;
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            imgAscensore.Margin = new Thickness(420, top, 290, bottom);
                        }));
                    }
                }
                pianoScelto = int.MinValue;
            }
        }

        private void piano1_Click(object sender, RoutedEventArgs e)
        {
            uomo1.Opacity = 100;
            Thread t = new Thread(new ThreadStart(Thread1));
            t.Start();
        }

        private void Thread1()//Metodo del movimeto piano1
        {
            semaforo.WaitOne();
            controlloStop = 277;
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
            Thread t = new Thread(new ThreadStart(Thread2));
            t.Start();
        }

        private void Thread2()//Metodo del movimento piano2
        {
            semaforo.WaitOne();
            controlloStop = 386;
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
            Thread t = new Thread(new ThreadStart(Thread3));
            t.Start();
        }

        
        private void Thread3()//Metodon del movimento piano3
        {
            semaforo.WaitOne();
            controlloStop = 493;
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
            Thread t = new Thread(new ThreadStart(ThreadM2));
            t.Start();
        }

        private void ThreadM2() //Metodo del movimento piano-2
        {
            semaforo.WaitOne();
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
            Thread t = new Thread(new ThreadStart(ThreadM1));
            t.Start();
        }


        private void ThreadM1()//Metodo del movimento piano-1
        {
            semaforo.WaitOne();
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
                controlloStop = 493;
            }
            else if (pianoScelto == 2)
            {
                controlloStop = 386;
            }
            else if(pianoScelto == 1)
            {
                controlloStop = 277;
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
