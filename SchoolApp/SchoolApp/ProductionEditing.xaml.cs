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
using System.IO;
using Microsoft.Win32;

namespace SchoolApp
{
    /// <summary>
    /// Логика взаимодействия для ProductionEditing.xaml
    /// </summary>
    public partial class ProductionEditing : Page
    {
        private Product _currentProduct = new Product();
        NoscovSchoolEntities3 context = new NoscovSchoolEntities3();
        public ProductionEditing(int id)
        {
            InitializeComponent();
            ComboProiz.ItemsSource = NoscovSchoolEntities3.GetContext().Manufactures.ToList();
            ListViewProduct.ItemsSource = NoscovSchoolEntities3.GetContext().Products.ToList();

            Product editproduct = NoscovSchoolEntities3.GetContext().Products.Find(id);

            if (editproduct != null)
            {
                _currentProduct = editproduct;
                if (_currentProduct.Активен != "Активен") mbut.IsChecked = true;
                
            }

                DataContext = _currentProduct;
        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            if (op.ShowDialog() == true)
            {
                ImageAdd.Source = new BitmapImage(new Uri(op.FileName));
            }

            var FileNameToSave = DateTime.Now.ToFileTime() + Path.GetExtension(op.FileName);
            var ImagePath = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}Товары школы\\{FileNameToSave}");
            TextBoxAdd.Text = ImagePath;
            TextBoxAdd.Focus();
            File.Copy(op.FileName, ImagePath);
        }

        private void BtnSave1000_Click(object sender, RoutedEventArgs e)
        {
            string k = "Активен";
            if (mbut.IsChecked.Value) k = "Не активен";
            _currentProduct.Активен = k.ToString();

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentProduct.Наименование_товара))
                errors.AppendLine("Введите наименование товара");
            if (_currentProduct.Manufactures == null)
                errors.AppendLine("Укажите производителя");
            if (_currentProduct.Цена < 1 || _currentProduct.Цена == null)
                errors.AppendLine("Укажите стоимость");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentProduct.ID == 0)
                NoscovSchoolEntities3.GetContext().Products.Add(_currentProduct);

            try
            {
                NoscovSchoolEntities3.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.Navigate(new Production());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
