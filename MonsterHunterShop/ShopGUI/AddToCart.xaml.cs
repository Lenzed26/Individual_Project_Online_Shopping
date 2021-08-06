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
using System.Windows.Shapes;
using ShopBusiness;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for AddToCart.xaml
    /// </summary>
    public partial class AddToCart : Window
    {
        private HunterManager _hunterManager;
        private ProductManager _productManager;
        private OrderManager _orderManager = new OrderManager();
        private OrderDetailsManager _orderDetailsManager = new OrderDetailsManager();
        public AddToCart(HunterManager hm, ProductManager pm)
        {
            _hunterManager = hm;
            _productManager = pm;
            InitializeComponent();
            CentreScreen();
            PopulateAllLabelsAndTextBox();
        }

        public void CentreScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void BackToStorePage_Click(object sender, RoutedEventArgs e)
        {
            MainStorePage storePage = new MainStorePage();
            storePage.Show();
            this.Close();
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            //Create Order and OrderDetail
        }

        private void PopulateAllLabelsAndTextBox()
        {
            CurrentUser.Content = _hunterManager.SelectedHunter.Name;
            ProductText.Content = _productManager.SelectedProduct.ProductName;
            PriceText.Content = _productManager.SelectedProduct.UnitPrice +"z";
        }

        private void QuantityText_Changed(object sender, TextChangedEventArgs e)
        {
            if (QuantityText.Text == null || QuantityText.Text == String.Empty || QuantityText == null)
            {
                TotalText.Content = "0z";
            }
            else
            {
                TotalText.Content = Convert.ToDecimal(QuantityText.Text) * Convert.ToDecimal(_productManager.SelectedProduct.UnitPrice) +"z";
            }
        }
    }
}
