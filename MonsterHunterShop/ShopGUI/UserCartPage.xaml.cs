using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for UserCartPage.xaml
    /// </summary>
    public partial class UserCartPage : Window
    {
        private OrderDetailsManager _orderDetailsManager = new OrderDetailsManager();
        private HunterManager _hunterManager;
        private OrderManager _orderManager = new OrderManager();
        private ProductManager _productManager;
        public UserCartPage(HunterManager hm, ProductManager pm)
        {
            _hunterManager = hm;
            _productManager = pm;
            InitializeComponent();
            CentreScreen();
            PopulateListView();
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

        private void PopulateListView()
        {
            //And populate User label
            CurrentUser.Content = _hunterManager.SelectedHunter.Name;
            CartListView.ItemsSource = _orderDetailsManager.RetrieveAllDetailedOrders().Where(i => i.HunterName == _hunterManager.SelectedHunter.Name && i.DeliveryDate == null);
        }

        private void BackToMainStore_Click(object sender, RoutedEventArgs e)
        {
            OpenStorePage();
        }

        private void OpenStorePage()
        {
            MainStorePage storePage = new MainStorePage();
            storePage.Show();
            this.Close();
        }

        private void PurchaseCart_Click(object sender, RoutedEventArgs e)
        {
            var list = _orderDetailsManager.RetrieveAllDetailedOrders().Where(i => i.HunterName == _hunterManager.SelectedHunter.Name);
            var deliveryDate = DateTime.Now.Date;
            foreach (var item in list)
            {        
                _orderManager.Update(item.OrderId ?? default(int), deliveryDate);
            }
            CartListView.ItemsSource = null;
            PopulateListView();
            CartListView.SelectedItem = null;
        }

        private void RemoveAll_Click(object sender, RoutedEventArgs e)
        {
            var orderList = _orderManager.RetrieveAllOrders().Where(i => i.HunterId == _hunterManager.SelectedHunter.HunterId && i.DeliveryDate == null);
            foreach (var item in orderList)
            {
                var orderDetailsList = _orderDetailsManager.RetrieveAllOrderDetails().Where(i => i.OrderId == item.OrderId);
                foreach (var i in orderDetailsList)
                {
                    _orderDetailsManager.Delete(i.OrderDetailsId);
                }
                _orderManager.Delete(item.OrderId);
            }
            CartListView.ItemsSource = null;
            PopulateListView();
            CartListView.SelectedItem = null;
        }

        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            if(CartListView.SelectedItem != null)
            {
                _orderDetailsManager.Delete(_orderDetailsManager.SelectedOrderDetail.OrderDetailsId);
                _orderManager.Delete(_orderManager.SelectedOrder.OrderId);                
            }
            CartListView.ItemsSource = null;
            PopulateListView();
            CartListView.SelectedItem = null;
            RemoveSelected.IsEnabled = false;
        }

        private void CartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CartListView.SelectedItem != null)
            {
                _orderDetailsManager.SetSelectedOrderDetail(_orderDetailsManager.ConvertToOrderDetail((DetailedOrder)CartListView.SelectedItem));
                _orderManager.SetSelectedOrder(_orderManager.ConvertToOrder((DetailedOrder)CartListView.SelectedItem));
            }
            RemoveSelected.IsEnabled = true;

            Debug.WriteLine($"[{_orderDetailsManager.SelectedOrderDetail.OrderDetailsId}, {_orderDetailsManager.SelectedOrderDetail.OrderId}, {_orderDetailsManager.SelectedOrderDetail.ProductId}, {_orderDetailsManager.SelectedOrderDetail.Quantity}, {_orderDetailsManager.SelectedOrderDetail.UnitPrice}]");
            Debug.WriteLine($"[{_orderManager.SelectedOrder.OrderId}, {_orderManager.SelectedOrder.HunterId}, {_orderManager.SelectedOrder.OrderDate}, {_orderManager.SelectedOrder.DeliveryDate}]");
        }

        private void ViewOrderHistory_Click(object sender, RoutedEventArgs e)
        {
            ViewUserOrderHistory viewOrder = new ViewUserOrderHistory(_hunterManager, _productManager);
            viewOrder.Show();
            this.Close();
        }
    }
}
