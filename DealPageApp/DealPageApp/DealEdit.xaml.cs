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
    /// Логика взаимодействия для DealEdit.xaml
    /// </summary>
    public partial class DealEdit : Page
    {
        private Deal _currentDeal = new Deal();
        public DealEdit(int id)
        {
            InitializeComponent();

            Deal editDeal = BD_SecuritesEntities.GetContext().Deals.Find(id);

            if (editDeal != null)
            {
                _currentDeal = editDeal;
            }

            CBStatus.Items.Add("Продано");
            CBStatus.Items.Add("Отказано");

            ComboAgreement.ItemsSource = BD_SecuritesEntities.GetContext().Agreements.ToList();
            CBEmployee.ItemsSource = BD_SecuritesEntities.GetContext().Employees.ToList();
            CBPlace.ItemsSource = BD_SecuritesEntities.GetContext().DealPlaces.ToList();
            CBDealType.ItemsSource = BD_SecuritesEntities.GetContext().DealTypes.ToList();
            CBTiker.ItemsSource = BD_SecuritesEntities.GetContext().Tikers.ToList();
            CBOrder.ItemsSource = BD_SecuritesEntities.GetContext().Orders.ToList();

            DataContext = _currentDeal;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentDeal.Agreement == null)
                errors.AppendLine("Выберите ценную бумагу!");
            if (_currentDeal.DealType == null)
                errors.AppendLine("Укажите тип сделки!");
            if (_currentDeal.DealPlace == null)
                errors.AppendLine("Укажите место сделки!");
            if (_currentDeal.Employee == null)
                errors.AppendLine("Укажите сотрудника!");
            if (_currentDeal.Tiker == null)
                errors.AppendLine("Укажите Tiker!");
            if (_currentDeal.Order == null)
                errors.AppendLine("Укажите заказ!");
            if (_currentDeal.TotalCost == null)
                errors.AppendLine("Укажите стоимость услуги!");
            if (string.IsNullOrWhiteSpace(_currentDeal.Constractor))
                errors.AppendLine("Укажите контрактора!");
            if (string.IsNullOrWhiteSpace(_currentDeal.Number))
                errors.AppendLine("Укажите номер сделки");
            if (_currentDeal.DealDate == null)
                errors.AppendLine("Укажите дату сделки");
            if (_currentDeal.Quanity == null)
                errors.AppendLine("Укажите количество ценных бумаг");
            if (string.IsNullOrWhiteSpace(_currentDeal.Trader))
                errors.AppendLine("Укажите продавца");
            if (string.IsNullOrWhiteSpace(_currentDeal.Note))
                errors.AppendLine("Укажите статус");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentDeal.ID == 0)
                BD_SecuritesEntities.GetContext().Deals.Add(_currentDeal);

            try
            {
                BD_SecuritesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.Navigate(new DealPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
