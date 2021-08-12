using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ShopBusiness;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for MainStorePage.xaml
    /// </summary>
    public partial class MainStorePage : Window
    {
        private ProductManager _productManager = new ProductManager();
        private HunterManager _hunterManager = new HunterManager();
        public MainStorePage()
        {
            InitializeComponent();
            CentreScreen();
            PopulateListBox();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            AddToCart.IsEnabled = false;
            GoToCart.IsEnabled = false;
            CurrentUser.ItemsSource = _hunterManager.RetrieveAllHunters();
            CurrentUser.SelectedItem = null;
            CurrentUser.Text = "--SELECT--";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void PopulateListBox()
        {
            ProductListBox.ItemsSource = _productManager.RetrieveAllProducts();
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

        private void AddToCartWindow(object sender, RoutedEventArgs e)
        {
            AddToCart toCart = new AddToCart(_hunterManager, _productManager);
            toCart.Show();
            this.Close();
        }

        private void ChangeCurrentUser(object sender, SelectionChangedEventArgs e)
        {
            _hunterManager.SetSelectedHunter(CurrentUser.SelectedItem);            
            AddToCart.IsEnabled = true;
            GoToCart.IsEnabled = true;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductListBox.SelectedItem != null)
            {
                _productManager.SetSelectedProducted(ProductListBox.SelectedItem);
            }
        }

        private void GoToCart_Window(object sender, RoutedEventArgs e)
        {
            UserCartPage cartPage = new UserCartPage(_hunterManager, _productManager);
            cartPage.Show();
            this.Close();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProductListBox.ItemsSource = null;
            ProductListBox.ItemsSource = _productManager.RetrieveAllProducts().Where(i => 
            i.ProductName.Contains(SearchBar.Text, StringComparison.OrdinalIgnoreCase) ||
            i.Description.Contains(SearchBar.Text, StringComparison.OrdinalIgnoreCase) ||
            i.Category.Contains(SearchBar.Text, StringComparison.OrdinalIgnoreCase) || 
            i.Rarity.ToString().Contains(SearchBar.Text, StringComparison.OrdinalIgnoreCase));
        }
    }
}
