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

namespace DealPageApp
{
    /// <summary>
    /// Логика взаимодействия для DealPage.xaml
    /// </summary>
    public partial class DealPage : Page
    {
        public DealPage()
        {
            InitializeComponent();
            LVDrivers.ItemsSource = BD_SecuritesEntities.GetContext().Deals.ToList();
        }

        private void BtnNewDeal_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new DealEdit(0));
        }

        private void LVDrivers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ii.DataContext = LVDrivers.SelectedItem;
                Deal editMaterial = BD_SecuritesEntities.GetContext().Deals.Find(int.Parse(ii.Text));
                Manager.MainFrame.Navigate(new DealEdit(int.Parse(ii.Text)));
            }
            catch
            {
                MessageBox.Show("Невозможно редактировать");
            }
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBookAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTiker_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEmployee_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
