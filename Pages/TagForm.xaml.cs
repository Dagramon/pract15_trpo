using pract15_trpo.Models;
using pract15_trpo.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для TagForm.xaml
    /// </summary>
    public partial class TagForm : Page
    {
        public KaramovElectronicShopDbContext db = DBService.Instance.Context;

        public TagService TagService { get; set; } = new();
        public Tag tag { get; set; } = new();
        public bool isEdit { get; set; } = false;

        string initialName = "";
        public TagForm(Tag? _tag = null)
        {
            InitializeComponent();
            if (_tag != null)
            {
                initialName = _tag.Name;
                tag = _tag;
                isEdit = true;
                Label.Content = "Редактирование тега";
            }
            DataContext = tag;
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            tag.Name = initialName;
            NavigationService.GoBack();
        }
        public bool CheckErrors()
        {
            StringBuilder errors = new StringBuilder();

            if (tag.Name == string.Empty || NameTextBox.Text != tag.Name)
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
                    var existingTag = TagService.Tags.FirstOrDefault(
                    t => t.Name == tag.Name && t.Id != tag.Id
                    );
                    if (existingTag == null)
                    {
                        if (!CheckErrors())
                            return;

                        TagService.Commit();
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Такой тег уже существует");
                    }
                }
            }
            else
            {
                var existingTag = TagService.Tags.FirstOrDefault(
                    t => t.Name == tag.Name
                    );
                if (existingTag == null)
                {
                    if (!CheckErrors())
                        return;

                    TagService.Add(tag);
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Такой тег уже существует");
                }
            }
        }
    }
}
