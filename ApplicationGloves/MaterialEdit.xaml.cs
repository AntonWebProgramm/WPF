using Microsoft.Win32;
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

namespace ApplicationGloves
{
    /// <summary>
    /// Логика взаимодействия для MaterialEdit.xaml
    /// </summary>
    public partial class MaterialEdit : Page
    {
        private Material _currentMaterial = new Material();
        List<CustomSupplier> customSuppliers;
        List<Supplier> suppliers;

        public MaterialEdit(int id)
        {
            InitializeComponent();

            Material editMaterial = ShopGlovesEntities.GetContext().Materials.Find(id);
            ComboSupplier.ItemsSource = ShopGlovesEntities.GetContext().Suppliers.ToList();

            if (editMaterial != null)
            {
                _currentMaterial = editMaterial;
            }

            CBProducts.ItemsSource = ShopGlovesEntities.GetContext().MaterialTypes.ToList();

            DataContext = _currentMaterial;

            suppliers = ShopGlovesEntities.GetContext().Suppliers.ToList();
            customSuppliers = new List<CustomSupplier>();
            suppliers.ForEach(supplier => customSuppliers.Add(new CustomSupplier(supplier)));
            customSuppliers.ForEach(supplier =>
            {
                if (_currentMaterial.Suppliers.Any(s => s.ID == supplier.ID)) ;
            });
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            if (string.IsNullOrWhiteSpace(_currentMaterial.Title))
            {
                errors.AppendLine("Укажите наименование услуги!");
                TBName.Background = Brushes.LightCoral;
            }

            if (_currentMaterial.MaterialType == null)
            {
                errors.AppendLine("Укажите тип материала");
            }

            if (_currentMaterial.CountInStock == null)
            {
                errors.AppendLine("Укажите количество на складе");
                TBStock.Background = Brushes.LightCoral;
            }
            else if (_currentMaterial.CountInStock < 0)
            {
                errors.AppendLine("Товар на складе не может быть отрицательным");
                TBStock.Background = Brushes.LightCoral;
            }

            if (string.IsNullOrWhiteSpace(_currentMaterial.Unit))
            {
                errors.AppendLine("Укажите единицу измерения");
                TBUnit.Background = Brushes.LightCoral;
            }

            if (_currentMaterial.CountInPack == 0)
            {
                errors.AppendLine("Укажите количество в упаковке");
                TBPack.Background = Brushes.LightCoral;
            }
            else if (_currentMaterial.CountInPack < 1)
            {
                errors.AppendLine("Количество в упаковке не может быть отрицательно");
                TBCount.Background = Brushes.LightCoral;
            }

            if (_currentMaterial.MinCount == 0)
            {
                errors.AppendLine("Укажите минимальное количество");
                TBCount.Background = Brushes.LightCoral;
            }
            else if (_currentMaterial.MinCount < 1)
            {
                errors.AppendLine("Минимальное количество не может быть отрицательным");
                TBCount.Background = Brushes.LightCoral;
            }

            if (_currentMaterial.Cost == 0)
            {
                errors.AppendLine("Укажите стоимость материала");
                TBCost.Background = Brushes.LightCoral;
            }
            else if (_currentMaterial.Cost < 1)
            {
                errors.AppendLine("Стоимость не может быть отрицательной");
                TBCount.Background = Brushes.LightCoral;
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentMaterial.ID == 0)
            {
                ShopGlovesEntities.GetContext().Materials.Add(_currentMaterial);
            }

            try
            {
                ShopGlovesEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                Manager.MainFrame.Navigate(new MaterialPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == true)
                {
                    ImageSource.Source = new BitmapImage(new Uri(op.FileName));
                }

                var FileNameToSave = DateTime.Now.ToFileTime() + Path.GetExtension(op.FileName);
                var ImagePath = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}Materials\\{FileNameToSave}");
                TBAdd.Text = ImagePath;
                TBAdd.Focus();
                File.Copy(op.FileName, ImagePath);
            }
            catch
            {
                MessageBox.Show("Вы не выбрали изображение!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }
        

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MaterialPage materialPage = new MaterialPage();
            materialPage.DataContext = materialPage.LVMaterial.SelectedItem;
            int i = int.Parse(ii.Text);
            Material delproduct = ShopGlovesEntities.GetContext().Materials.Find(i);
            if (delproduct.ProductMaterials.Count != 0)
            {
                MessageBox.Show("Невозможно удалить материал, т.к. есть связанные данные.");
                return;
            }

            {
                if (MessageBox.Show("Вы действительно хотите удалить материал?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;

                ShopGlovesEntities.GetContext().Materials.Remove(delproduct);
                ShopGlovesEntities.GetContext().SaveChanges();
                MessageBox.Show("Материал успешно удален");
                Manager.MainFrame.Navigate(new MaterialPage());
            }
        }
    }
}
