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
            posizioneInizio2 = 277;
            semaforo = new Semaphore(0, 1);
            semaforo.Release(1);
            posizioneInizio1 = 249;
        }
        private int posizioneInizio2;
        private Semaphore semaforo;
        private int posizioneInizio1;
        private int controlloStop;
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
            semaforo.Release();
        }

        private void AscensorePiano()
        {
            if (posizioneInizio2 < controlloStop)
            {
                while (posizioneInizio2 < controlloStop)
                {
                    posizioneInizio2 += 5;
                    posizioneInizio1 -= 5;
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        imgAscensore.Margin = new Thickness(420, posizioneInizio1, 290, posizioneInizio2);
                    }));
                }
            }
            else
            {
                while (posizioneInizio2 > controlloStop)
                {
                    posizioneInizio2 -= 5;
                    posizioneInizio1 += 5;
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        imgAscensore.Margin = new Thickness(420, posizioneInizio1, 290, posizioneInizio2);
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
            semaforo.Release();
        }
    }
}
