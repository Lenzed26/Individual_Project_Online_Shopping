using System.Linq;
using System.Windows;
using ShopBusiness;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for ViewUserOrderHistory.xaml
    /// </summary>
    public partial class ViewUserOrderHistory : Window
    {
        private HunterManager _hunterManager;
        private OrderDetailsManager _orderDetailsManager = new OrderDetailsManager();
        private ProductManager _productManager;
        public ViewUserOrderHistory(HunterManager hm, ProductManager pm)
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

        private void BackToUserCart_Click(object sender, RoutedEventArgs e)
        {
            UserCartPage cartPage = new UserCartPage(_hunterManager, _productManager);
            cartPage.Show();
            this.Close();
        }

        private void PopulateListView()
        {
            CartListView.ItemsSource = _orderDetailsManager.RetrieveAllDetailedOrders().Where(i => i.HunterName == _hunterManager.SelectedHunter.Name);
        }
    }
}
