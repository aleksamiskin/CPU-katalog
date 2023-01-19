using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Dodaj.xaml
    /// </summary>
    public partial class Dodaj : Window
    {
        OpenFileDialog window = new OpenFileDialog();
        int p;

        Color r;
        int kolor = 7;
        public Dodaj(int parametar)
        {
            InitializeComponent();

            if(parametar == -1)
            {
                p = -1;
            }
            else
            {
                p = parametar;
                dodaj.Content = "Izmeni";
                Naziv.Content = "Izmeni";

                textbox_nazivCPU.Text = MainWindow.CPU[p].Naziv_CPU;
                textbox_brojJezgra.Text = MainWindow.CPU[p].Broj_Jezgra.ToString();
                slika.Source = new BitmapImage(new Uri(MainWindow.CPU[p].Slika_Proizvodjaca));
                datepicker.SelectedDate = MainWindow.CPU[p].Datum_Izlaska;

                MainWindow.CPU[p].Tekstualni_Fajl = MainWindow.CPU[p].Naziv_CPU + ".rtf";

                FileStream fileStream = new FileStream(MainWindow.CPU[p].Tekstualni_Fajl, FileMode.Open, FileAccess.ReadWrite);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();

            }

            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            cmbFontColor.ItemsSource = typeof(Colors).GetProperties();


            rtbEditor.Foreground = Brushes.Black;
            cmbFontColor.SelectedItem = typeof(Colors).GetProperties()[7];



            textBox.Text = "Word Count: 0";
            lblCursorPosition.Text = "Line: " + "1" + " Column: " + "1";
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Click_izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validacija()
        {
            bool rezultat = true;


            //NAZIV
            if (textbox_nazivCPU.Text.Trim() == "")
            {
                label_greska_naziv.Content = "Morate popuniti polje!";
                textbox_nazivCPU.BorderBrush = Brushes.Red;
                rezultat = false;
            }
            else
            {
                label_greska_naziv.Content = "";
                textbox_nazivCPU.BorderBrush = Brushes.SlateGray;
            }


            //BROJ JEZGRA
            if (textbox_brojJezgra.Text.Trim() == "")
            {
                label_greska_broj.Content = "Morate popuniti polje!";
                textbox_brojJezgra.BorderBrush = Brushes.Red;
                rezultat = false;
            }
            else
            {
                label_greska_broj.Content = "";
                textbox_nazivCPU.BorderBrush = Brushes.SlateGray;
            }


            //SLIKA
            if (window.FileName == "" && p == -1)   // vec ima sliku, nema smisla raditi validaciju za promenu
            {
                label_greska_slika.Content = "Morate izabrati sliku!";
                label_greska_slika.BorderBrush = Brushes.Red;
                button_slika.BorderBrush = Brushes.Red;
                rezultat = false;
            }
            else
            {
                label_greska_slika.Content = "";
                button_slika.BorderBrush = Brushes.Gray;
            }

            //DATUM
            if (datepicker.SelectedDate == null)
            {
                label_greska_datum.Content = "Morate popuniti polje!";
                datepicker.BorderBrush = Brushes.Red;
                rezultat = false;
            }
            else
            {
                label_greska_datum.Content = "";
                datepicker.BorderBrush = Brushes.SlateGray;


                try
                {

                    DateTime trenutni = DateTime.Now;


                    DateTime unesen_datum = datepicker.SelectedDate.Value.Date;

                    int rez = DateTime.Compare(trenutni, unesen_datum);

                    if (rez >= 0)
                    {

                    }
                    else
                    {
                        label_greska_datum.Content = "Ovaj datum je u buducnosti!";
                        datepicker.BorderBrush = Brushes.Red;
                        rezultat = false;

                    }


                }
                catch (Exception e)
                {
                    // .......
                    rezultat = false;

                }


            }

            //RichTextBox

            string rtbToString (RichTextBox rtb)
            {
                TextRange text = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                return text.Text;
            }

            if (rtbToString(rtbEditor) == "\r\n")
            {
                label_greska_tekst.Content = "Morate popuniti polje!";
                rtbEditor.BorderBrush = Brushes.Yellow;
                rezultat = false;
            }
            else
            {
                label_greska_tekst.Content = "";
                rtbEditor.BorderBrush = Brushes.SlateGray;
            }

            return rezultat;
        }

        private void Click_sacuvaj(object sender, RoutedEventArgs e)
        {
            if (validacija() == true)
            {
                if (p == -1) //Dodaj
                {
                    CPU c = new CPU();

                    c.Naziv_CPU = textbox_nazivCPU.Text;
                    c.Broj_Jezgra = int.Parse(textbox_brojJezgra.Text);

                    c.Datum_Izlaska = datepicker.SelectedDate.Value.Date;

                    c.Slika_Proizvodjaca = slika.Source.ToString();

                    MainWindow.CPU.Add(c);

                    c.Tekstualni_Fajl = c.Naziv_CPU + ".rtf";                    

                    FileStream fileStream = new FileStream(c.Tekstualni_Fajl, FileMode.Create);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Rtf);
                    fileStream.Close();

                    this.Close();
                }
                else //Izmeni
                {
                    CPU c = new CPU();

                    c.Naziv_CPU = textbox_nazivCPU.Text;
                    c.Broj_Jezgra = int.Parse(textbox_brojJezgra.Text);

                    c.Datum_Izlaska = datepicker.SelectedDate.Value.Date;

                    c.Slika_Proizvodjaca = slika.Source.ToString();
                    MainWindow.CPU[p] = c;

                    c.Tekstualni_Fajl = c.Naziv_CPU + ".rtf";

                    FileStream fileStream = new FileStream(c.Tekstualni_Fajl, FileMode.Create);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Rtf);
                    fileStream.Close();

                    this.Close();
                }
            }
            else
            {

                MessageBox.Show("Niste dobro popunili polja!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void Click_slika(object sender, RoutedEventArgs e)
        {
            window.FileName = "";
            window.Title = "Izaberite sliku.";

            // PROMENITI KAD PREMESTIM PROJEKAT...
            window.InitialDirectory = @"Slike_za_projekat";

            window.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            if (window.ShowDialog() == true)
            {
                Uri u = new Uri(window.FileName);
                slika.Source = new BitmapImage(u);  // BitmapImage je za dodelu putanje slici 

            }
        }

        private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }


        private void CmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
            }
            catch
            {
                cmbFontSize.Text = "b";
            }
        }


        private void CmbFontColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontColor.SelectedItem != null)
            {
                kolor = cmbFontColor.SelectedIndex;
                var selectedItem = (PropertyInfo)cmbFontColor.SelectedItem;
                var color = (Color)selectedItem.GetValue(null, null);

                rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, color.ToString());
                r = (Color)System.Windows.Media.ColorConverter.ConvertFromString(color.ToString());
            }
        }

        private void RtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // toggle buttons
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            //comboboxs
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;


            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();

            temp = rtbEditor.Selection.GetPropertyValue(Inline.ForegroundProperty);

            var hexcolor = ((SolidColorBrush)temp).Color.ToString();
            SolidColorBrush sol = (SolidColorBrush)temp;


            string s = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            string[] pom = s.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            textBox.Text = "Word Count: " + pom.Length.ToString();

            TextPointer tp1 = rtbEditor.Selection.Start.GetLineStartPosition(0);
            TextPointer tp2 = rtbEditor.Selection.Start;

            int column = tp1.GetOffsetToPosition(tp2) + 1;

            int someBigNumber = int.MaxValue;
            int lineMoved, currentLineNumber;
            rtbEditor.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);
            currentLineNumber = -lineMoved + 1;

            lblCursorPosition.Text = "Line: " + currentLineNumber.ToString() + " Column: " + column.ToString();
        }
    }
}

