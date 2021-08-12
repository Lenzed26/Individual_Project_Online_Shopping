using System.Windows;
using System.Windows.Controls;
using ShopBusiness;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for AdminProductsPage.xaml
    /// </summary>
    public partial class AdminProductsPage : Window
    {
        private ProductManager _productManager = new ProductManager();
        public AdminProductsPage()
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

        private void PopulateListView()
        {
            ProductsListView.ItemsSource = _productManager.RetrieveAllProducts();
        }


        private void CreateProduct_Window(object sender, RoutedEventArgs e)
        {
            CreateProductWindow productWindow = new CreateProductWindow();
            productWindow.Show();
            this.Close();
        }

        private void UpdateProduct_Window(object sender, RoutedEventArgs e)
        {
            UpdateProductWindow productWindow = new UpdateProductWindow(_productManager);
            productWindow.Show();
            this.Close();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            _productManager.Delete(_productManager.SelectedProduct.ProductName);
            ProductsListView.ItemsSource = null;
            PopulateListView();
            ProductsListView.SelectedItem = null;
        }

        private void BackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminMainPage mainPage = new AdminMainPage();
            mainPage.Show();
            this.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ProductsListView.SelectedItem != null)
            {
                _productManager.SetSelectedProducted(ProductsListView.SelectedItem);
            }
        }
    }
}
