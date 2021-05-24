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
    /// Interaction logic for WindowFilmPromena.xaml
    /// </summary>
    public partial class WindowFilmPromena : Window
    {
        private ZanrDal zDal = new ZanrDal();
        public int Promena { get; set; }
        public WindowFilmPromena()
        {
            InitializeComponent();
        }

        private void PrikaziZanrove()
        {
            ComboBoxZanr.ItemsSource = null;
            ComboBoxZanr.ItemsSource = zDal.VratiZanrove();
            ComboBoxZanr.SelectedValuePath = "ZanrId";
        }
        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNaziv.Text))
            {
                MessageBox.Show("Morate uneti naziv filma", "Poruka");
                TextBoxNaziv.Focus();
                return false;
            }



            if (!int.TryParse(TextBoxGodina.Text, out int godina))
            {
                MessageBox.Show("Godina filma je ceo broj", "Poruka");
                TextBoxGodina.Clear();
                TextBoxGodina.Focus();
                return false;
            }

            if (ComboBoxZanr.SelectedIndex < 0)
            {
                MessageBox.Show("Morate uneti zanr filma", "Poruka");

                return false;
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziZanrove();
        }

        private void ButtonPrihvati_Click(object sender, RoutedEventArgs e)
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
