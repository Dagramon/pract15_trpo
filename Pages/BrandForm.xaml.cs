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
    /// Логика взаимодействия для BrandForm.xaml
    /// </summary>
    public partial class BrandForm : Page
    {
        public KaramovElectronicShopDbContext db = DBService.Instance.Context;

        public BrandService BrandService { get; set; } = new();
        public Brand brand { get; set; } = new();
        public bool isEdit { get; set; } = false;

        string initialName = "";
        public BrandForm(Brand? _brand = null)
        {
            InitializeComponent();
            if (_brand != null)
            {
                initialName = _brand.Name;
                brand = _brand;
                isEdit = true;
                Label.Content = "Редактирование бренда";
            }
            DataContext = brand;
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            brand.Name = initialName;
            NavigationService.GoBack();
        }
        public bool CheckErrors()
        {
            StringBuilder errors = new StringBuilder();

            if (brand.Name == string.Empty || NameTextBox.Text != brand.Name)
                errors.AppendLine("Неккоректное название");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }

            return true;
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                MessageBoxResult result = MessageBox.Show(
                                "Сохранить изменения?",
                                "Сохранить",
                                MessageBoxButton.YesNo
                );
                if (result == MessageBoxResult.Yes)
                {
                    var existingBrand = BrandService.Brands.FirstOrDefault(
                    b => b.Name == brand.Name && b.Id != brand.Id
                    );
                    if (existingBrand == null)
                    {
                        if (!CheckErrors())
                            return;

                        BrandService.Commit();
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Такой бренд уже существует");
                    }
                }
            }
            else
            {
                var existingBrand = BrandService.Brands.FirstOrDefault(
                    b => b.Name == brand.Name
                    );
                if (existingBrand == null)
                {
                    if (!CheckErrors())
                        return;

                    BrandService.Add(brand);
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Такой бренд уже существует");
                }
            }
        }
    }
}
