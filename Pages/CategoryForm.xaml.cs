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
    /// Логика взаимодействия для CategoryForm.xaml
    /// </summary>
    public partial class CategoryForm : Page
    {
        public KaramovElectronicShopDbContext db = DBService.Instance.Context;

        public CategoryService CategoryService { get; set; } = new();
        public Category category { get; set; } = new();
        public bool isEdit { get; set; } = false;

        string initialName = "";
        public CategoryForm(Category? _category = null)
        {
            InitializeComponent();
            if (_category != null)
            {
                initialName = _category.Name;
                category = _category;
                isEdit = true;
                Label.Content = "Редактирование категории";
            }
            DataContext = category;
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            category.Name = initialName;
            NavigationService.GoBack();
        }
        public bool CheckErrors()
        {
            StringBuilder errors = new StringBuilder();

            if (category.Name == string.Empty || NameTextBox.Text != category.Name)
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
                    var existingCategory = CategoryService.Categories.FirstOrDefault(
                    c => c.Name == category.Name && c.Id != category.Id
                    );
                    if (existingCategory == null)
                    {
                        if (!CheckErrors())
                            return;

                        CategoryService.Commit();
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Такая категория уже существует");
                    }
                }
            }
            else
            {
                var existingCategory = CategoryService.Categories.FirstOrDefault(
                    c => c.Name == category.Name
                    );
                if (existingCategory == null)
                {
                    if (!CheckErrors())
                        return;

                    CategoryService.Add(category);
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Такая категория уже существует");
                }
            }
        }
    }
}
