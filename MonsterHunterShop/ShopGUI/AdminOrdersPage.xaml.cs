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
    /// Interaction logic for AdminOrdersPage.xaml
    /// </summary>
    public partial class AdminOrdersPage : Window
    {
        private OrderDetailsManager _odm = new OrderDetailsManager();
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
            OrdersListView.ItemsSource = _odm.RetrieveAllOrderDetails();
        }

        private void BackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMainPage admin = new AdminMainPage();
            admin.Show();
            this.Close();
        }
    }
}
