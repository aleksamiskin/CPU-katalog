using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Zadatak1
{
    /// <summary>
    /// Interaction logic for Procitaj.xaml
    /// </summary>
    public partial class Procitaj : Window
    {
        int p;
        public Procitaj(int parametar)
        {
            InitializeComponent();

            p = parametar;

            nazivCPU.Content = MainWindow.CPU[p].Naziv_CPU;
            brojJezgra.Content = MainWindow.CPU[p].Broj_Jezgra.ToString();
            slika.Source = new BitmapImage(new Uri(MainWindow.CPU[p].Slika_Proizvodjaca));
            datum.Content = MainWindow.CPU[p].Datum_Izlaska;

            MainWindow.CPU[p].Tekstualni_Fajl = MainWindow.CPU[p].Naziv_CPU + ".rtf";

            FileStream fileStream = new FileStream(MainWindow.CPU[p].Tekstualni_Fajl, FileMode.Open, FileAccess.ReadWrite);
            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            range.Load(fileStream, DataFormats.Rtf);
            fileStream.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Click_izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
