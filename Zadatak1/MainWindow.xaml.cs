using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zadatak1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static BindingList<CPU> CPU { get; set; }
        public MainWindow()
        {
            CPU = new BindingList<CPU>();
            DataContext = this;
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Click_izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Click_procitaj(object sender, RoutedEventArgs e)
        {
            Procitaj p = new Procitaj(tabelaCPU.SelectedIndex);
            p.ShowDialog();
        }
        private void Click_izmeni(object sender, RoutedEventArgs e)
        {
            int i = tabelaCPU.SelectedIndex;
            Dodaj d = new Dodaj(i);
            d.ShowDialog();
        }
        private void Click_obrisi(object sender, RoutedEventArgs e)
        {
            MainWindow.CPU[tabelaCPU.SelectedIndex].Tekstualni_Fajl = MainWindow.CPU[tabelaCPU.SelectedIndex].Naziv_CPU + ".rtf";
            File.Delete(MainWindow.CPU[tabelaCPU.SelectedIndex].Tekstualni_Fajl);
            CPU.RemoveAt(tabelaCPU.SelectedIndex);
        }

        private void Click_dodaj(object sender, RoutedEventArgs e)
        {
            Dodaj d = new Dodaj(-1);
            d.ShowDialog();
        }
    }
}
