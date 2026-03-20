using pract15_trpo.Models;
using pract15_trpo.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public KaramovElectronicShopDbContext db = DBService.Instance.Context;
        public ObservableCollection<Product> products { get; set; } = new();
        public string searchQuery { get; set; } = null;
        public string filterPriceFrom { get; set; } = null;
        public string filterPriceTo { get; set; } = null;
        public MainPage(bool manager)
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            products.Clear();
            foreach (var product in db.Products.ToList())
                products.Add(product);
        }
    }
}
