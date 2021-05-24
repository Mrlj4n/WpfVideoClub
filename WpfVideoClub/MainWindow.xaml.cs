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

namespace WpfVideoClub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            WindowClan wc = new WindowClan();
            wc.Owner = this; // vlasnik WC je Mainwin
            wc.ShowDialog();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            WindowFilm w1 = new WindowFilm();
            w1.Owner = this; // vlasnik WC je Mainwin
            w1.ShowDialog();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            WindowIznajmljivanje w1 = new WindowIznajmljivanje();
            w1.Owner = this;
            w1.ShowDialog();
        }
    }
}
