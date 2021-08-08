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
    /// Interaction logic for ViewOrderHistoryPage.xaml
    /// </summary>
    public partial class ViewOrderHistoryPage : Window
    {
        private HunterManager _hunterManager;
        private OrderDetailsManager _orderDetailsManager = new OrderDetailsManager();
        public ViewOrderHistoryPage(HunterManager hm)
        {
            _hunterManager = hm;
            InitializeComponent();
            PopulateListView();
        }

        private void BackToCart_Click(object sender, RoutedEventArgs e)
        {
            MainStorePage storePage = new MainStorePage();
            storePage.Show();
            this.Close();
        }

        private void PopulateListView()
        {
            OrdersListView.ItemsSource = _orderDetailsManager.RetrieveAllDetailedOrders().Where(i => i.HunterName == _hunterManager.SelectedHunter.Name);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
