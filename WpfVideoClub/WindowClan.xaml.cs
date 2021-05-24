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
using System.Windows.Shapes;

namespace WpfVideoClub
{
    /// <summary>
    /// Interaction logic for WindowClan.xaml
    /// </summary>
    public partial class WindowClan : Window
    {
        private ClanDal cDal = new ClanDal();
        public WindowClan()
        {
            InitializeComponent();
        }

        private void PrikaziClanove()
        {
            ListBox1.ItemsSource = null;
            ListBox1.ItemsSource = cDal.VratiClanove();
            ListBox1.SelectedValuePath = "ClanId"; // na osnovu Id selectedItem
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziClanove();
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox1.SelectedIndex > -1)
            {
                Clan c = ListBox1.SelectedItem as Clan;
                TextBoxIme.Text = c.Ime;
                TextBoxPrezime.Text = c.Prezime;
                TextBoxJmbg.Text = c.Jmbg;
                TextBoxAdresa.Text = c.Adresa;
                TextBoxTelefon.Text = c.Telefon;

            }

        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            ClanPromena w1 = new ClanPromena();
            w1.Owner = this;
            w1.Title = "Unesi podatke o clanu";

            if (w1.ShowDialog() == true)
            {
                Clan c1 = new Clan
                {
                    Ime = w1.TextBoxIme.Text,
                    Prezime = w1.TextBoxPrezime.Text,
                    Jmbg = w1.TextBoxJmbg.Text,
                    Adresa = w1.TextBoxAdresa.Text,
                    Telefon = w1.TextBoxTelefon.Text
                };

                int rez = cDal.UbaciClana(c1);

                if (rez == 0)
                {
                    PrikaziClanove();
                    ListBox1.SelectedValue = c1.ClanId; //zbog SelectValuePath
                }
                else
                {
                    MessageBox.Show("Greska pri cuvanju podataka");
                }

            }
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox1.SelectedIndex > -1)
            {
                Clan c = ListBox1.SelectedItem as Clan;
                ClanPromena w1 = new ClanPromena();
                w1.Owner = this;
                w1.Title = c.ClanId + " " + c.Ime;
                w1.TextBoxIme.Text = c.Ime;
                w1.TextBoxPrezime.Text = c.Prezime;
                w1.TextBoxJmbg.Text = c.Jmbg;
                w1.TextBoxAdresa.Text = c.Adresa;
                w1.TextBoxTelefon.Text = c.Telefon;

                if (w1.ShowDialog()==true)
                {
                    c.Ime = w1.TextBoxIme.Text;
                    c.Prezime = w1.TextBoxPrezime.Text;
                    c.Jmbg = w1.TextBoxJmbg.Text;
                    c.Adresa = w1.TextBoxAdresa.Text;
                    c.Telefon = w1.TextBoxTelefon.Text;

                    int rez = cDal.PromeniClana(c);

                    if (rez == 0)
                    {
                        PrikaziClanove();
                        ListBox1.SelectedValue = c.ClanId;
                        MessageBox.Show("Podaci promenjeni");
                    }
                    else
                    {
                        MessageBox.Show("Greska pri promeni clana");
                    }
                }

            }
            else
            {
                MessageBox.Show("Odaberite clana");
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox1.SelectedIndex > -1)
            {
                Clan c = ListBox1.SelectedItem as Clan;

                MessageBoxResult rez = MessageBox.Show("Da li ste sigurni", c.Ime + " " + c.Prezime, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (rez==MessageBoxResult.No)
                {
                    return;
                }

                int rezultat = cDal.ObrisiClana(c);

                if (rezultat==0)
                {
                    PrikaziClanove();
                    MessageBox.Show("Podaci obrisani");
                    ListBox1.SelectedIndex = -1;
                    TextBoxIme.Clear();
                    TextBoxPrezime.Clear();
                    TextBoxJmbg.Clear();
                    TextBoxAdresa.Clear();
                    TextBoxTelefon.Clear();
                }
                else
                {
                    MessageBox.Show("Greska pri brisanju podataka");
                }
            }
            else
            {
                MessageBox.Show("Odaberite clana");

            }
        }
    }
}
