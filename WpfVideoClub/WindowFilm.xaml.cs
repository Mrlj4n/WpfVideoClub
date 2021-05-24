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
    /// Interaction logic for WindowFilm.xaml
    /// </summary>
    public partial class WindowFilm : Window
    {
        private FilmDal fDal = new FilmDal();
        private ZanrDal zDal = new ZanrDal();
        public WindowFilm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziZanrove();
            PrikaziFilmove();
        }
        
        private void PrikaziFilmove()
        {
            ListBox1.ItemsSource = fDal.VratiFilmove();
        }

        private void PrikaziZanrove()
        {
            ComboBoxZanr.ItemsSource = null;
            ComboBoxZanr.ItemsSource = zDal.VratiZanrove();
            ComboBoxZanr.SelectedValuePath = "ZanrId";
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox1.SelectedIndex>-1)
            {
                Film f = ListBox1.SelectedItem as Film;
                TextBoxNaziv.Text = f.NazivFilma;
                TextBoxGodina.Text = f.Godina.ToString();
                ComboBoxZanr.SelectedValue = f.ZanrId;
            }
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            WindowFilmPromena w1 = new WindowFilmPromena();
            w1.Promena = 0;
            w1.Owner = this;
            w1.Title = "Unesite podatke o filmu";
            if (w1.ShowDialog() == true)
            {
                Film f = new Film
                {
                    NazivFilma = w1.TextBoxNaziv.Text,
                    Godina = int.Parse(w1.TextBoxGodina.Text),
                    ZanrId = (int)w1.ComboBoxZanr.SelectedValue                    
                };

                int rez = fDal.UbaciFilm(f);

                if (rez==0)
                {
                    PrikaziZanrove();
                    PrikaziFilmove();
                    ListBox1.SelectedValue = f.FilmId;
                    ListBox1.ScrollIntoView(f);
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
            if (ListBox1.SelectedIndex > -1)
            {
                Film f = ListBox1.SelectedItem as Film;
                WindowFilmPromena w1 = new WindowFilmPromena();
                w1.Title = "Promena: "+ f.NazivFilma;
                w1.Promena = 1;
                w1.Owner = this;
                w1.TextBoxNaziv.Text = f.NazivFilma;
                w1.TextBoxGodina.Text = f.Godina.ToString();
                w1.ComboBoxZanr.SelectedValue = f.ZanrId;

                if (w1.ShowDialog() == true)
                {
                    f.NazivFilma = w1.TextBoxNaziv.Text;
                    f.Godina = int.Parse(w1.TextBoxGodina.Text);
                    f.ZanrId = (int)w1.ComboBoxZanr.SelectedValue;

                    int rez = fDal.PromeniFilm(f);

                    if (rez == 0)
                    {
                        PrikaziZanrove();
                        PrikaziFilmove();
                        ListBox1.SelectedValue = f.FilmId;
                        ListBox1.ScrollIntoView(f);
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
                MessageBox.Show("Odaberite film");
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox1.SelectedIndex > -1)
            {
                Film selFilm = ListBox1.SelectedItem as Film;
                MessageBoxResult pitanje = MessageBox.Show("Da li ste sigurni", selFilm.NazivFilma, MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (pitanje==MessageBoxResult.No)
                {
                    return;
                }

                int rez = fDal.ObrisiFilm(selFilm);

                if (rez == 0)
                {
                    PrikaziFilmove();
                    MessageBox.Show("Podaci obrisani");
                    ListBox1.SelectedIndex = -1;
                    TextBoxNaziv.Clear();
                    TextBoxGodina.Clear();
                    ComboBoxZanr.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Greska pri brisanju podataka");
                }
            }
            else
            {
                MessageBox.Show("Odaberite film");
            }
        }
    }
}
