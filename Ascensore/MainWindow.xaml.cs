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

namespace Ascensore
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bottom = 277;
            semaforo = new Semaphore(0, 1);
            semaforo.Release(1);
            top = 249;
            pianoScelto = int.MinValue;
        }
        private int bottom;
        private Semaphore semaforo;
        private int top;
        private int controlloStop;
        private int pianoScelto;
        private void piano1_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Thread1));
            t.Start();
        }

        private void Thread1()
        {
            semaforo.WaitOne();
            controlloStop = 277;
            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();
            while (pianoScelto == int.MinValue)
                Thread.Sleep(1000);
            PortaPersona();
            pianoScelto = int.MinValue;
            semaforo.Release();
        }

        private void AscensorePiano()
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
        }

        private void piano2_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Thread2));
            t.Start();
        }

        private void Thread2()
        {
            semaforo.WaitOne();
            controlloStop = 386;
            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();
            while (pianoScelto == int.MinValue)
                Thread.Sleep(1000);
            PortaPersona();
            pianoScelto = int.MinValue;
            semaforo.Release();
        }

        private void piano3_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Thread3));
            t.Start();
        }

        private void Thread3()
        {
            semaforo.WaitOne();
            controlloStop = 493;
            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();
            while (pianoScelto == int.MinValue)
                Thread.Sleep(1000);
            PortaPersona();
            pianoScelto = int.MinValue;
            semaforo.Release();
        }

        private void piano_2_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ThreadM2));
            t.Start();
        }

        private void ThreadM2()
        {
            semaforo.WaitOne();
            controlloStop = 59;
            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();
            while (pianoScelto == int.MinValue)
                Thread.Sleep(1000);
            PortaPersona();
            pianoScelto = int.MinValue;
            semaforo.Release();
        }

        private void piano_1_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ThreadM1));
            t.Start();
        }

        private void ThreadM1()
        {
            semaforo.WaitOne();
            controlloStop = 169;
            Thread t1 = new Thread(new ThreadStart(AscensorePiano));
            t1.Start();
            t1.Join();
            while (pianoScelto == int.MinValue)
                Thread.Sleep(500);
            PortaPersona();
            pianoScelto = int.MinValue;
            semaforo.Release();
        }

        private void Chiama3_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = 3;
        }

        private void Chiama2_Click(object sender, RoutedEventArgs e)
        {
            pianoScelto = 2;
        }

        private void PortaPersona()
        {
            if (pianoScelto == 3)
            {
                controlloStop = 493;
                AscensorePiano();
            }
            else if (pianoScelto == 2)
            {
                controlloStop = 386;
                AscensorePiano();
            }
            else if(pianoScelto == 1)
            {
                controlloStop = 277;
                AscensorePiano();
            }
            else if(pianoScelto == -1)
            {
                controlloStop = 169;
                AscensorePiano();
            }
            else if(pianoScelto == -2)
            {
                controlloStop = 59;
                AscensorePiano();
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
    }
}
