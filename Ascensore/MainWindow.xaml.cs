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
            posizioneAscensore = 277;
        }
        private int posizioneAscensore;
        private void piano1_Click(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(AscensorePiano1));
            t1.Start();

        }

        private void AscensorePiano1()
        {
            int pos = 246;
            while (posizioneAscensore != 130)
            {
                posizioneAscensore += 10;
                pos -= 10;
                Thread.Sleep(TimeSpan.FromMilliseconds(500));

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgAscensore.Margin = new Thickness(420, pos, 290, posizioneAscensore);
                }));


            }
        }
    }
}
