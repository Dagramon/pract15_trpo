using pract15_trpo.Models;
using pract15_trpo.Service;
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
    /// Логика взаимодействия для BrandList.xaml
    /// </summary>
    public partial class BrandList : Page
    {
        public KaramovElectronicShopDbContext db = DBService.Instance.Context;

        public BrandService BrandService { get; set; } = new();

        public Brand? selected { get; set; } = null;
        public BrandList()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GoForm(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BrandForm());
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (selected != null)
            {
                MessageBoxResult result = MessageBox.Show(
                                "Удалить бренд?",
                                "Удалить",
                                MessageBoxButton.YesNo
                );
                if (result == MessageBoxResult.Yes)
                {
                    BrandService.Remove(selected);
                }
            }
            else
            {
                MessageBox.Show("Бренд не выбран");
            }

        }

        private void GoEdit(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new BrandForm(selected));
        }
    }
}
