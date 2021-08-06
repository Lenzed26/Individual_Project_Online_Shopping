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
    /// Interaction logic for UserCartPage.xaml
    /// </summary>
    public partial class UserCartPage : Window
    {
        private OrderDetailsManager _orderDetailsManager = new OrderDetailsManager();
        private HunterManager _hunterManager;
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
            CartListView.ItemsSource = _orderDetailsManager.RetrieveAllDetailedOrders();
        }

        private void BackToAdmin_Click(object sender, RoutedEventArgs e)
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

        }
    }
}
