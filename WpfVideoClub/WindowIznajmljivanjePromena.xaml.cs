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
    /// Interaction logic for WindowIznajmljivanjePromena.xaml
    /// </summary>
    public partial class WindowIznajmljivanjePromena : Window
    {
        public int Promena { get; set; }
        private ClanDal cDal = new ClanDal();
        private FilmDal fDal = new FilmDal();
        public WindowIznajmljivanjePromena()
        {
            InitializeComponent();
        }
        private bool Validacija()
        {
            if (ComboBoxClan.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati clana", "Poruka");
                return false;
            }

            if (ComboBoxFilm.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati film", "Poruka");
                return false;
            }

            if (DatePicker1.SelectedDate == null)
            {
                MessageBox.Show("Morate odabrati datum uzimanja", "Poruka");
                return false;
            }
            if (DatePicker2.SelectedDate == null)
            {
                MessageBox.Show("Morate odabrati datum vracanja", "Poruka");
                return false;
            }



            if (!decimal.TryParse(TextBoxCena.Text, out decimal cena))
            {
                MessageBox.Show("Morate uneti cenu", "Poruka");
                return false;
            }
            return true;
        }

        private void PrikaziClanove()
        {
            ComboBoxClan.ItemsSource = null;
            ComboBoxClan.ItemsSource = cDal.VratiClanove();
            ComboBoxClan.SelectedValuePath = "ClanId";
        }

        private void PrikaziFilmove()
        {
            ComboBoxFilm.ItemsSource = null;
            ComboBoxFilm.ItemsSource = fDal.VratiFilmove();
            ComboBoxFilm.SelectedValuePath = "FilmId";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Promena==0)
            {
                DatePicker1.SelectedDate = DateTime.Now;
                DatePicker2.SelectedDate = DateTime.Now.AddDays(7);
            }
                PrikaziClanove();
                PrikaziFilmove(); 
        }

        private void ButtonPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Validacija())
            {
                DialogResult = true;
            }
        }


        private void ButtonOdustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
