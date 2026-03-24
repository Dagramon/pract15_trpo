using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pract15_trpo.Models;
using pract15_trpo.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        public KaramovElectronicShopDbContext db = DBService.Instance.Context;
        public CategoryService CategoryService { get; set; } = new();
        public BrandService BrandService { get; set; } = new();
        public ProductService ProductService { get; set; } = new();
        public ICollectionView productsView { get; set; }
        private string _searchQuery = null;
        public string searchQuery 
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(); ;
            }
        }
        public string selectedSortTag { get; set; }

        private string _filterPriceFrom = null;
        private string _filterPriceTo = null;

        public string filterPriceFrom
        {
            get => _filterPriceFrom;
            set
            {
                _filterPriceFrom = value;
                PriceClass.priceFrom = value;
                OnPropertyChanged();
            }
        }

        public string filterPriceTo
        {
            get => _filterPriceTo;
            set
            {
                _filterPriceTo = value;
                PriceClass.priceTo = value;
                OnPropertyChanged();
            }
        }

        public string brandSort { get; set; }
        public string categorySort { get; set; }

        public bool manager = false;
        public Product? product { get; set; } = null;

        public MainPage(bool _manager)
        {
            productsView = CollectionViewSource.GetDefaultView(ProductService.Products);
            productsView.Filter = FilterForms;
            InitializeComponent();
            DataContext = this;

            manager = _manager;

            if (manager)
                ManagerPanel.Visibility = Visibility.Visible;
            else
                ManagerPanel.Visibility = Visibility.Hidden;
        }

        public bool FilterForms(object obj)
        {
            if (obj is not Product)
                return false;

            var product = (Product)obj;

            if (searchQuery != null && !product.Name.Contains(searchQuery,
                StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (!string.IsNullOrEmpty(filterPriceFrom) && Convert.ToDecimal(filterPriceFrom) > product.Price)
                return false;
            if (!string.IsNullOrEmpty(filterPriceTo) && Convert.ToDecimal(filterPriceTo) < product.Price)
                return false;
            if (!string.IsNullOrEmpty(brandSort) && product.Brand != null && product.Brand.Name != brandSort)
                return false;
            if (!string.IsNullOrEmpty(categorySort) && product.Category != null && product.Category.Name != categorySort)
                return false;
            if (product.Category == null && !string.IsNullOrEmpty(categorySort))
                return false;
            if (product.Brand == null && !string.IsNullOrEmpty(brandSort))
                return false;
            return true;
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            productsView.Refresh();
            try
            {
                if (Convert.ToDecimal(filterPriceFrom) > Convert.ToDecimal(filterPriceTo))
                {
                    ErrorPrice.Text = "Неверно введён разброс цены";
                }
                else
                {
                    ErrorPrice.Text = "";
                }
            }
            catch
            {

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductService.GetAll();
        }

        public void Sort(object sender, RoutedEventArgs e)
        {
            productsView.SortDescriptions.Clear();
            switch (selectedSortTag)
            {
                case "Price":
                    if (AscendingSort.IsChecked == true)
                        productsView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
                    else if (DescendingSort.IsChecked == true)
                        productsView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
                    break;
                case "Stock":
                    if (AscendingSort.IsChecked == true)
                        productsView.SortDescriptions.Add(new SortDescription("Stock", ListSortDirection.Ascending));
                    else if (DescendingSort.IsChecked == true)
                        productsView.SortDescriptions.Add(new SortDescription("Stock", ListSortDirection.Descending));
                    break;
            }
            productsView.Refresh();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            searchQuery = "";
            brandSort = "";
            categorySort = "";
            selectedSortTag = "";
            filterPriceFrom = "";
            filterPriceTo = "";
            CategoryComboBox.SelectedIndex = -1;
            BrandComboBox.SelectedIndex = -1;
            productsView.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void RemoveProduct(object sender, RoutedEventArgs e)
        {
            if (product == null)
            {
                MessageBox.Show("Товар не выбран");
                return;
            }
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить товар?",
                "Удалить",
                MessageBoxButton.YesNo
                );
            if (result == MessageBoxResult.Yes)
            {
                ProductService.Remove(product);
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductForm());
            PriceClass.priceTo = "";
            PriceClass.priceFrom = "";
        }

        private void EditProduct(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddProductForm(product));
            PriceClass.priceTo = "";
            PriceClass.priceFrom = "";
        }

        private void GoTags(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TagList());
            PriceClass.priceTo = "";
            PriceClass.priceFrom = "";
        }

        private void GoBrands(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BrandList());
            PriceClass.priceTo = "";
            PriceClass.priceFrom = "";
        }

        private void GoCategories(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryList());
            PriceClass.priceTo = "";
            PriceClass.priceFrom = "";
        }
    }
}