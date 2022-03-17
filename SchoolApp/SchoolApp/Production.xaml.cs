using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchoolApp
{
    /// <summary>
    /// Логика взаимодействия для Production.xaml
    /// </summary>
    public partial class Production : Page
    {
        PageList PageList;
        static NoscovSchoolEntities3 context = new NoscovSchoolEntities3();
        //List<Product> sort = context.Products.ToList();

        public Production()
        {
            InitializeComponent();
            DGridProduct.ItemsSource = NoscovSchoolEntities3.GetContext().Products.ToList();

            var allManufactures = NoscovSchoolEntities3.GetContext().Manufactures.ToList();
            allManufactures.Insert(0, new Manufacture
            {
                Название_производителя = "Все производители"
            });
            cb.ItemsSource = allManufactures;
            cb.SelectedIndex = 0;


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            update();

            //PageList = new PageList(context.Products.ToList());
            //PageList = new PageList(sort);
        }

        private void update()
        {
            var currentProduct = context.Products.ToList();
            var sort = currentProduct.ToList();

            currentProduct = currentProduct.Where(p =>
            p.Наименование_товара.ToLower().Contains(textname.Text.ToLower())).ToList();

            
            switch (Price.SelectedIndex)
            {
                case 0: { sort = currentProduct.OrderBy(s => s.Цена).ToList(); break; }
                case 1: { sort = currentProduct.OrderByDescending(s => s.Цена).ToList(); break; }
                case 2: { sort = currentProduct.ToList(); break; }
            }


            PageList = new PageList(sort);
            DGridProduct.ItemsSource = PageList.OffsetProduct;

            int k = NoscovSchoolEntities3.GetContext().Products.Count();
            int n = sort.Count();
            Col.Text = "Количество записей: " + n.ToString() + ". Всего записей: " + k.ToString();

            // DGridProduct.ItemsSource = sort.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ii.DataContext = DGridProduct.SelectedItem;
                Product editproduct = context.Products.Find(int.Parse(ii.Text));
                Manager.MainFrame.Navigate(new ProductionEditing(int.Parse(ii.Text)));
            }
            catch
            {
                MessageBox.Show("Выберите товар для реактирования");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductionEditing(0));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ii.DataContext = DGridProduct.SelectedItem;
                int i = int.Parse(ii.Text);
                Product delproduct = context.Products.Find(i);
                if (delproduct.ProductSales.Count != 0)
                {
                    MessageBox.Show("Невозможно удалить продукт, т.к. есть связанные данные.");
                    return;
                }


                if (MessageBox.Show("Вы действительно хотите удалить продукт?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;

                context.Products.Remove(delproduct);
                context.SaveChanges();


                PageList.Products.Remove(delproduct);
                DGridProduct.ItemsSource = PageList.OffsetProduct;
                MessageBox.Show("Продукт успешно удален");
            }
            catch
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void lastPageButton_Click(object sender, RoutedEventArgs e)
        {
            PageList.CurrentPage--;
            DGridProduct.ItemsSource = PageList.OffsetProduct;
            lastPageButton.IsEnabled = PageList.IsFirstPage;
            nextPageButton.IsEnabled = PageList.IsLastPage;
        }

        private void ChangeOffsetClick(object sender, RoutedEventArgs e)
        {
            PageList.CountInPage = Convert.ToInt32((sender as RadioButton).Content);
            DGridProduct.ItemsSource = PageList.OffsetProduct;
            lastPageButton.IsEnabled = PageList.IsFirstPage;
            nextPageButton.IsEnabled = PageList.IsLastPage;
        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            PageList.CurrentPage++;
            DGridProduct.ItemsSource = PageList.OffsetProduct;
            lastPageButton.IsEnabled = PageList.IsFirstPage;
            nextPageButton.IsEnabled = PageList.IsLastPage;
        }

        private SolidColorBrush gg = new SolidColorBrush(Colors.White);
        private SolidColorBrush hh = new SolidColorBrush(Colors.LightGray);

        private void textname_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void x2_Click(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void ProductSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ii.DataContext = DGridProduct.SelectedItem;
                int i = int.Parse(ii.Text);

                Manager.MainFrame.Navigate(new HistorySalePage(context.Products.Find(i)));
            }
            catch
            {
                MessageBox.Show("Выберите продукт");
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            NoscovSchoolEntities3.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridProduct.ItemsSource = NoscovSchoolEntities3.GetContext().Products.ToList();
        }
    }
}
