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

namespace pract15_trpo.Pages
{
    /// <summary>
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    public partial class EnterPage : Page
    {

        public string pin { get; set; }
        public EnterPage()
        {
            InitializeComponent();
        }

        private void EnterWithPin(object sender, RoutedEventArgs e)
        {
            if (pin.ToString() == "1234")
            {
                NavigationService.Navigate(new MainPage(true));
            }
            else
            {
                MessageBox.Show("Неверный пин-код");
            }
        }

        private void Enter(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(false));
        }
    }
}
