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
    /// Interaction logic for WindowIznajmljivanje.xaml
    /// </summary>
    public partial class WindowIznajmljivanje : Window
    {
        private IznajmljivanjeDal iDal = new IznajmljivanjeDal();
        public WindowIznajmljivanje()
        {
            InitializeComponent();
        }

        private void PrikaziIznajmljivanja()
        {
            DataGrid1.ItemsSource = null;
            DataGrid1.ItemsSource = iDal.VratiIznajmljivanja();
            DataGrid1.SelectedValuePath = "IznajmljivanjeId";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziIznajmljivanja();
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            WindowIznajmljivanjePromena w1 = new WindowIznajmljivanjePromena();
            w1.Owner = this;
            w1.Title = "Ubaci podatke o iznajmljivanju";
            w1.Promena = 0;

            if (w1.ShowDialog() ==true)
            {
                Clan c = w1.ComboBoxClan.SelectedItem as Clan;
                Film f = w1.ComboBoxFilm.SelectedItem as Film;

                Iznajmljivanje i = new Iznajmljivanje
                {
                    ClanId = c.ClanId,
                    FilmId = f.FilmId,
                    DatumIznajmljivanja = w1.DatePicker1.SelectedDate.Value,
                    DatumVracanja  = w1.DatePicker2.SelectedDate.Value,
                    Cena = decimal.Parse(w1.TextBoxCena.Text)
                };

                int rez = iDal.UbaciIznajmljivanje(i);

                if (rez==0)
                {
                    PrikaziIznajmljivanja();
                    DataGrid1.Focus();
                    DataGrid1.SelectedValue = i.IznajmljivanjeId;
                    DataGrid1.ScrollIntoView(DataGrid1.SelectedItem);
                    MessageBox.Show("Podaci sacuvani");
                }
                else
                {
                    MessageBox.Show("Greska pri cuvanju podataka");
                }
            }
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            int id = DataGrid1.SelectedIndex;
            if (id > -1)
            {
                ViewIznajmljivanja v = DataGrid1.SelectedItem as ViewIznajmljivanja;
                int id1 = v.IznajmljivanjeId;

                Iznajmljivanje iz1 = iDal.VratiIznajmljivanje(id1);

                WindowIznajmljivanjePromena w1 = new WindowIznajmljivanjePromena();
                w1.Title = v.NazivFilma;
                w1.Owner = this;
                w1.Promena = 1;
                w1.ComboBoxClan.SelectedValue = iz1.ClanId;
                w1.ComboBoxFilm.SelectedValue = iz1.FilmId;
                w1.DatePicker1.SelectedDate = iz1.DatumIznajmljivanja;
                w1.DatePicker2.SelectedDate = iz1.DatumVracanja;
                w1.TextBoxCena.Text = iz1.Cena.ToString();

                if (w1.ShowDialog()==true)
                {
                    Clan c = w1.ComboBoxClan.SelectedItem as Clan;
                    Film f = w1.ComboBoxFilm.SelectedItem as Film;

                    iz1.ClanId = c.ClanId;
                    iz1.FilmId = f.FilmId;
                    iz1.DatumIznajmljivanja = w1.DatePicker1.SelectedDate.Value;
                    iz1.DatumVracanja = w1.DatePicker2.SelectedDate.Value;
                    iz1.Cena = decimal.Parse(w1.TextBoxCena.Text);

                    int rez = iDal.PromeniIznajmljivanje(iz1);

                    if (rez == 0)
                    {
                        PrikaziIznajmljivanja();
                        DataGrid1.Items.Refresh();
                        DataGrid1.Focus();
                        DataGrid1.SelectedIndex = id;
                        DataGrid1.ScrollIntoView(id1);
                        MessageBox.Show("Podaci promenjeni");
                    }
                    else
                    {
                        MessageBox.Show("Greska pri promeni podataka");
                    }
                }
            }
            else
            {
                MessageBox.Show("Odaberite iznajmljivanje");
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedIndex > -1)
            {
                ViewIznajmljivanja red = DataGrid1.SelectedItem as ViewIznajmljivanja;
                Iznajmljivanje iz = iDal.VratiIznajmljivanje(red.IznajmljivanjeId);

                if (MessageBox.Show("Da li ste sigurni?",red.NazivFilma,MessageBoxButton.YesNo,MessageBoxImage.Warning)== MessageBoxResult.Yes)
                {
                    int rez = iDal.ObrisiIznajmljivanje(iz);

                    if (rez ==0)
                    {
                        PrikaziIznajmljivanja();
                        MessageBox.Show("Podaci obrisani");
                    }
                    else
                    {
                        MessageBox.Show("Greska pri brisanju podataka");
                    }
                }
            }
            else
            {
                MessageBox.Show("Odaberite iznajmljivanje");
            }
        }
    }
}
