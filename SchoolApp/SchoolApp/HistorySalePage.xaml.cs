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

namespace SchoolApp
{
    /// <summary>
    /// Логика взаимодействия для HistorySalePage.xaml
    /// </summary>
    public partial class HistorySalePage : Page
    {
        NoscovSchoolEntities3 context = new NoscovSchoolEntities3();
        private Product MyProduct;
        public HistorySalePage(Product product)
        {
            InitializeComponent();

            MyProduct = product;
            if (product.ProductSales.Count == 0) {
                MessageBox.Show("Нет продуктов на складе");

            }
            else
            {
                var table = context.ProductSales.Select(a =>
                new
                {
                    id = a.ID,
                    productid = a.ID_товара,
                    prod = "Наименование товара: " + a.Product.Наименование_товара,
                    data = (DateTime?)a.Дата_и_время_продажи,
                    col = "Количество: " + a.Количество
                })
                    .Where(s => s.productid == MyProduct.ID)
                    .ToList();

                Lv.ItemsSource = table;
            }
        }
    }
}
