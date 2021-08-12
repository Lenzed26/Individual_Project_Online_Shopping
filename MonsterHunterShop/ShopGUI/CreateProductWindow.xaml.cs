using System;
using System.Windows;
using ShopBusiness;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for CreateProductPage.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {
        private ProductManager _productManager = new ProductManager();
        public CreateProductWindow()
        {
            InitializeComponent();
            CentreScreen();
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

        private void CreateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfEmpty())
            {
                MessageBox.Show("Please fill in Name, Category and Rarity.\nDescription can be left empty");
            }
            else
            {
                try
                {
                    _productManager.Create(ProductName_TextBox.Text.Trim(), ProductCategory_TextBox.Text.Trim(), Convert.ToInt32(ProductRarity_TextBox.Text.Trim()), Convert.ToDecimal(ProductPrice_TextBox.Text.Trim()), ProductPrice_TextBox.Text != null ? ProductPrice_TextBox.Text.Trim() : null);
                    OpenProductsPage();
                }
                catch (ArgumentException ex)
                {

                    MessageBox.Show("Item already exists, please try again");
                }
            }
        }

        private bool CheckIfEmpty()
        {
            return ProductName_TextBox == null || ProductRarity_TextBox == null ||
                   ProductCategory_TextBox == null || ProductPrice_TextBox == null ||
                   ProductPrice_TextBox.Text == String.Empty || ProductName_TextBox.Text == String.Empty ||
                   ProductRarity_TextBox.Text == String.Empty || ProductCategory_TextBox.Text == String.Empty;
        }

        private void BackToProducts_Click(object sender, RoutedEventArgs e)
        {
            OpenProductsPage();
        }

        private void OpenProductsPage()
        {
            ProductName_TextBox.Clear();
            ProductRarity_TextBox.Clear();
            ProductCategory_TextBox.Clear();
            ProductDescription_TextBox.Clear();
            ProductPrice_TextBox.Clear();
            CreateAdminProductsWindow();
        }

        private void CreateAdminProductsWindow()
        {
            AdminProductsPage productsPage = new AdminProductsPage();
            productsPage.Show();
            this.Close();
        }
    }
}
