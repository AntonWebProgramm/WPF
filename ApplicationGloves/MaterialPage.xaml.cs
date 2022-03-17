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

namespace ApplicationGloves
{
    /// <summary>
    /// Логика взаимодействия для MaterialPage.xaml
    /// </summary>
    public partial class MaterialPage : Page
    {
        PageList PageList;
        public MaterialPage()
        {
            InitializeComponent();
            LVMaterial.ItemsSource = ShopGlovesEntities.GetContext().Materials.ToList();
            update();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void update()
        {
            var currentMaterial = ShopGlovesEntities.GetContext().Materials.ToList();

            currentMaterial = currentMaterial.Where(p =>
            p.Title.ToLower().Contains(textname.Text.ToLower())).ToList();

            var sort = currentMaterial.ToList();

            switch (SortTitle.SelectedIndex)
            {
                case 0: { sort = currentMaterial.ToList(); break; }
                case 1: { sort = currentMaterial.OrderBy(s => s.Title).ToList(); break; }
                case 2: { sort = currentMaterial.OrderByDescending(s => s.Title).ToList(); break; }
                case 3: { sort = currentMaterial.OrderBy(s => s.CountInStock).ToList(); break; }
                case 4: { sort = currentMaterial.OrderByDescending(s => s.CountInStock).ToList(); break; }
                case 5: { sort = currentMaterial.OrderBy(s => s.Cost).ToList(); break; }



                case 6: { sort = currentMaterial.OrderByDescending(s => s.Cost).ToList(); break; }
            }

            //if (CBFiltr.SelectedIndex > 0)
            //sort = currentMaterial.Where(p => p.MaterialTypes.Contains(CBFiltr.SelectedItem as MaterialType)).ToList();

            PageList = new PageList(sort);
            LVMaterial.ItemsSource = PageList.OffsetMaterial;

            int k = ShopGlovesEntities.GetContext().Materials.Count();
            int n = sort.Count();
            Col.Text = "Количество записей: " + n.ToString() + " Всего записей: " + k.ToString();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MaterialEdit(0));
        }

        private void LVMaterial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ii.DataContext = LVMaterial.SelectedItem;
                Material editMaterial = ShopGlovesEntities.GetContext().Materials.Find(int.Parse(ii.Text));
                Manager.MainFrame.Navigate(new MaterialEdit(int.Parse(ii.Text)));
            }
            catch
            {
                MessageBox.Show("Вы не можете редактировать поставщиков!");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // update();
        }

        private void SortTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void textname_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void NextPageClick_Click(object sender, RoutedEventArgs e)
        {
            PageList.CurrentPage++;
            LVMaterial.ItemsSource = PageList.OffsetMaterial;
            LastPageClick.IsEnabled = PageList.IsFirstPage;
            NextPageClick.IsEnabled = PageList.IsLastPage;
            NachaloPageClick.IsEnabled = PageList.IsFirstPage;
            EndPageClick.IsEnabled = PageList.IsLastPage;
        }

        private void LastPageClick_Click(object sender, RoutedEventArgs e)
        {
            PageList.CurrentPage--;
            LVMaterial.ItemsSource = PageList.OffsetMaterial;
            LastPageClick.IsEnabled = PageList.IsFirstPage;
            NextPageClick.IsEnabled = PageList.IsLastPage;
            NachaloPageClick.IsEnabled = PageList.IsFirstPage;
            EndPageClick.IsEnabled = PageList.IsLastPage;
        }

        private void NachaloPageClick_Click(object sender, RoutedEventArgs e)
        {
            PageList.CurrentPage = 0;
            LVMaterial.ItemsSource = PageList.OffsetMaterial;
            LastPageClick.IsEnabled = PageList.IsFirstPage;
            NextPageClick.IsEnabled = PageList.IsLastPage;
            NachaloPageClick.IsEnabled = PageList.IsFirstPage;
            EndPageClick.IsEnabled = PageList.IsLastPage;
        }

        private void EndPageClick_Click(object sender, RoutedEventArgs e)
        {
            PageList.CurrentPage = PageList.EndPage;
            LVMaterial.ItemsSource = PageList.OffsetMaterial;
            LastPageClick.IsEnabled = PageList.IsFirstPage;
            NextPageClick.IsEnabled = PageList.IsLastPage;
            NachaloPageClick.IsEnabled = PageList.IsFirstPage;
            EndPageClick.IsEnabled = PageList.IsLastPage;      
        }

        private void BlackTem_Click(object sender, RoutedEventArgs e)
        {
            BrushConverter BlackGridBottom = new BrushConverter();
            BrushConverter OrangeWrapPanel = new BrushConverter();
            BrushConverter ButtonOrange = new BrushConverter();

            BottomGrid.Background = (Brush)BlackGridBottom.ConvertFrom("#000");
            WrapPanelBottom.Background = (Brush)OrangeWrapPanel.ConvertFrom("#A5E887");
            BtnAdd.Background = (Brush)ButtonOrange.ConvertFrom("#A5E887");
        }

        private void OriginTem_Click(object sender, RoutedEventArgs e)
        {
            BrushConverter BlackGridBottom = new BrushConverter();
            BrushConverter OrangeWrapPanel = new BrushConverter();
            BrushConverter ButtonOrange = new BrushConverter();

            BottomGrid.Background = (Brush)BlackGridBottom.ConvertFrom("#B3F4E9");
            WrapPanelBottom.Background = (Brush)OrangeWrapPanel.ConvertFrom("#A5E887");
            BtnAdd.Background = (Brush)ButtonOrange.ConvertFrom("#A5E887");
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            ShopGlovesEntities.GetContext().SaveChanges();
            MessageBox.Show("Тема сохранена");
        }
    }
}
