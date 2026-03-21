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
    /// Логика взаимодействия для CategoryList.xaml
    /// </summary>
    public partial class CategoryList : Page
    {
        public KaramovElectronicShopDbContext db = DBService.Instance.Context;

        public CategoryService CategoryService { get; set; } = new();

        public Category? selected { get; set; } = null;
        public CategoryList()
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
            NavigationService.Navigate(new CategoryForm());
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (selected != null)
            {
                MessageBoxResult result = MessageBox.Show(
                                "Удалить категорию?",
                                "Удалить",
                                MessageBoxButton.YesNo
                );
                if (result == MessageBoxResult.Yes)
                {
                    CategoryService.Remove(selected);
                }
            }
            else
            {
                MessageBox.Show("Категория не выбрана");
            }

        }

        private void GoEdit(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new CategoryForm(selected));
        }
    }
}
