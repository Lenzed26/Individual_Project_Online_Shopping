using System.Windows;
using System.Windows.Controls;
using ShopBusiness;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for AdminOrdersPage.xaml
    /// </summary>
    public partial class AdminOrdersPage : Window
    {
        private OrderDetailsManager _odm = new OrderDetailsManager();
        private OrderManager _om = new OrderManager();
        public AdminOrdersPage()
        {
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

        public void PopulateListView()
        {
            OrdersListView.ItemsSource = _odm.RetrieveAllDetailedOrders();
        }

        private void BackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMainPage admin = new AdminMainPage();
            admin.Show();
            this.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersListView.SelectedItem != null)
            {
                _odm.SetSelectedOrderDetail(_odm.ConvertToOrderDetail((DetailedOrder)OrdersListView.SelectedItem));
                _om.SetSelectedOrder(_om.ConvertToOrder((DetailedOrder)OrdersListView.SelectedItem));
            }
            DeleteOrder.IsEnabled = true;
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListView.SelectedItem != null)
            {
                DeleteOrder.IsEnabled = false;
                _odm.Delete(_odm.SelectedOrderDetail.OrderDetailsId);
                _om.Delete(_om.SelectedOrder.OrderId);
            }
            OrdersListView.ItemsSource = null;
            PopulateListView();
            OrdersListView.SelectedItem = null;

        }
    }
}
