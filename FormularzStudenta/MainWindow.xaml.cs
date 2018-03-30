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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FormularzStudenta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] uczelnie = { "Politechnika Gdańska", "Politechnika Poznańska", "Politechnika Koszalińska", "Politechnika Warszawska", "Politechnika Wrocławska", "Politechnika Łódźka" };
        private string[] jezyki = { "język angielski", "język francuski", "język niemiecki", "język włoski", "język rosyjski" };


        public MainWindow()
        {
            InitializeComponent();
            this.reset();

        }

        private void reset()
        {
            this.imie.Text = string.Empty;
            this.nazwisko.Text = string.Empty;

            this.listaUczelni.Items.Clear();

            foreach (var elem in uczelnie)
            {
                listaUczelni.Items.Add(elem);
            }
            this.listaUczelni.Text = this.listaUczelni.Items[0] as string;

            this.listaJezykow.Items.Clear();
            CheckBox cbJezyk = null;
            foreach (string jezyk in jezyki)
            {
                cbJezyk = new CheckBox();
                cbJezyk.Margin = new Thickness(0, 0, 0, 10);
                cbJezyk.Content = jezyk;
                listaJezykow.Items.Add(cbJezyk);
            }
            this.czyEelektrotech.IsChecked = false;
            this.rok1.IsChecked = true;
            this.dataPraktyki.Text = DateTime.Today.ToString(); 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MessageBoxResult key = MessageBox.Show("Czy chcesz zamknąć", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (key == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Dodaj_click(object sender, RoutedEventArgs e)
        {
  

            var context = new FormularzStudentaEntities();
            var odczyt = new praktykanci()
            {
                imie = imie.Text,
                nazwisko = nazwisko.Text,
                uczelnia = listaUczelni.Text,
                //elektrotechnika = czyEelektrotech.IsChecked.ToString(),
                dataPraktyk = Convert.ToDateTime(dataPraktyki.Text),
                //rokStudiow = RadioButton.ContentProperty.ToString()
            };
            context.praktykanci.Add(odczyt);
            context.SaveChanges();

            //string nazwiskoUczelnia = string.Format("Nazwisko Studenta {0} {1} z {2} \nZnajomość języków:", this.imie.Text, this.nazwisko.Text, this.listaUczelni.Text);
            //StringBuilder info = new StringBuilder();
            //info.Append(nazwiskoUczelnia);
            //foreach (CheckBox cb in this.listaJezykow.Items)
            //{
            //    if (cb.IsChecked == true)
            //    {
            //        info.AppendLine(" " + cb.Content.ToString());
            //    }
            //}
            MessageBox.Show("Pomyślnie wprowadzono dane studenta");


        }

        private void Wyczysc_click(object sender, RoutedEventArgs e)
        {
            this.reset();
        }

        private void nowyStazysta_Click(object sender, RoutedEventArgs e)
        {
            this.reset();
            zapiszDane.IsEnabled = true;
            imie.IsEnabled = true;
            nazwisko.IsEnabled = true;
            listaJezykow.IsEnabled = true;
            listaUczelni.IsEnabled = true;
            czyEelektrotech.IsEnabled = true;
            dataPraktyki.IsEnabled = true;
            groupBox1.IsEnabled = true;
            wyczysc.IsEnabled = true;
            dodaj.IsEnabled = true;

        }

        private void zapiszDane_Click(object sender, RoutedEventArgs e)
        {
           

            using (StreamWriter writer = new StreamWriter("Stazysta.txt"))
            {
                Console.WriteLine("Imię: {0}", imie.Text);
                Console.WriteLine($"Naziwsko: {nazwisko.Text}");
                Console.WriteLine($"Uczelnia: {listaUczelni.Text}");
                Console.WriteLine($"Elektrotech.: {czyEelektrotech.IsChecked.ToString()}");
                Console.WriteLine($"Języki:");

                foreach (CheckBox cb in listaJezykow.Items)
                {
                    if (cb.IsChecked.Value)
                    {
                        writer.WriteLine(cb.Content.ToString());
                    }
                }
                
                MessageBox.Show(RadioButton.NameProperty.ToString());
            }
        }

        private void wyjscie_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            Informacja dialogInfo = new Informacja();
            dialogInfo.ShowDialog();
        }
    }
}
