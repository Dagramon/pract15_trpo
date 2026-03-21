using pract15_trpo.Models;
using pract15_trpo.Service;
using System;
using System.Collections;
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
    /// Логика взаимодействия для AddProductForm.xaml
    /// </summary>
    public partial class AddProductForm : Page
    {
        public KaramovElectronicShopDbContext db = DBService.Instance.Context;
        public List<Category> categoryList { get; set; }
        public List<Brand> brandList { get; set; }
        public ObservableCollection<Tag> tagList { get; set; }
        private ProductService _service = new();
        public Product product { get; set; } = new();
        bool isEdit = false;
        public AddProductForm(Product? _editProduct = null)
        {
            InitializeComponent();
            if (_editProduct != null)
            {
                product = _editProduct;
                isEdit = true;

                tagList = new ObservableCollection<Tag>();
                foreach (Tag tag in db.Tags)
                {
                    if (!product.Tags.Contains(tag))
                    {
                        tagList.Add(tag);
                    }
                }
                Label.Content = "Редактирование товара";
            }
            else
            {
                tagList = new ObservableCollection<Tag>(db.Tags.ToList());
            }

            brandList = db.Brands.ToList();
            categoryList = db.Categories.ToList();
            DataContext = this;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public bool CheckErrors()
        {
            StringBuilder errors = new StringBuilder();

            if (product.Name == string.Empty || NameTextBox.Text != product.Name)
                errors.AppendLine("Неккоректное название");
            if (product.Description == string.Empty || DescriptionTextBox.Text != product.Description)
                errors.AppendLine("Неккоректное описание");
            if (product.Price == null || PriceTextBox.Text != product.Price.ToString())
                errors.AppendLine("Неккоректная цена");
            if (product.Stock == null || StockTextBox.Text != product.Stock.ToString())
                errors.AppendLine("Неккоректное количество");
            if (product.Rating == null || RatingTextBox.Text != product.Rating.ToString())
                errors.AppendLine("Неккоректный рейтинг");
            if (product.Brand == null)
                errors.AppendLine("Не выбран бренд");
            if (product.Category == null)
                errors.AppendLine("Не выбрана категория");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }

            return true;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                MessageBoxResult result = MessageBox.Show(
                "Сохранить изменения товара?",
                "Сохранить",
                MessageBoxButton.YesNo
                );
                if (result == MessageBoxResult.Yes)
                {
                    if (!CheckErrors())
                        return;

                    _service.Commit();
                }
            }
            else
            {
                if (!CheckErrors())
                    return;

                product.CreatedAt = DateTime.Now;
                _service.Add(product);
            }
            NavigationService.GoBack();
        }

        private void AddTag(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Tag? tag = button.DataContext as Tag;

            tagList.Remove(tag);
            product.Tags.Add(tag);
        }

        private void RemoveTag(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Tag? tag = button.DataContext as Tag;

            product.Tags.Remove(tag);
            tagList.Add(tag);
        }
    }
}
