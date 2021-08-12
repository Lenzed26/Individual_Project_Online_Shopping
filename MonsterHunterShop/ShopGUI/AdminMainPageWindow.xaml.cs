using System.Windows;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AdminMainPage : Window
    {
        public AdminMainPage()
        {
            InitializeComponent();
            CentreScreen();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void HuntersButton_Clicked(object sender, RoutedEventArgs e)
        {
            AdminHuntersPage huntersPage = new AdminHuntersPage();
            huntersPage.Show();
            this.Close();
        }

        private void OrdersButton_Clicked(object sender, RoutedEventArgs e)
        {
            AdminOrdersPage ordersPage = new AdminOrdersPage();
            ordersPage.Show();
            this.Close();
        }

        private void ProductsButton_Clicked(object sender, RoutedEventArgs e)
        {
            AdminProductsPage productsPage = new AdminProductsPage();
            productsPage.Show();
            this.Close();
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
    }
}
