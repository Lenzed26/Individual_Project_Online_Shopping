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
    /// Interaction logic for UpdateProductWindow.xaml
    /// </summary>
    public partial class UpdateProductWindow : Window
    {
        ProductManager _productManager;
        public UpdateProductWindow(ProductManager pm)
        {
            _productManager = pm;
            InitializeComponent();
            CentreScreen();
            PopulateTextBox();
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

        private void UpdateSelectedProduct()
        {
            _productManager.SelectedProduct.ProductName = ProductName_TextBox.Text.Trim();
            _productManager.SelectedProduct.Rarity = Convert.ToInt32(ProductRarity_TextBox.Text.Trim());
            _productManager.SelectedProduct.Description = ProductDescription_TextBox.Text.Trim();
            _productManager.SelectedProduct.UnitPrice = Convert.ToDecimal(ProductPrice_TextBox.Text.Trim());
            _productManager.SelectedProduct.Category = ProductCategory_TextBox.Text.Trim();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if(ProductName_TextBox.Text.Equals(_productManager.SelectedProduct.ProductName))
            {
                _productManager.Update(ProductName_TextBox.Text.Trim(), ProductCategory_TextBox.Text.Trim(), Convert.ToInt32(ProductRarity_TextBox.Text.Trim()), Convert.ToDecimal(ProductPrice_TextBox.Text.Trim()), ProductDescription_TextBox.Text.Trim());
                UpdateSelectedProduct();
                OpenProductsPage();
            }
            else
            {
                try
                {
                    _productManager.Update(_productManager.SelectedProduct.ProductName,ProductName_TextBox.Text.Trim(), ProductCategory_TextBox.Text.Trim(), Convert.ToInt32(ProductRarity_TextBox.Text.Trim()), Convert.ToDecimal(ProductPrice_TextBox.Text.Trim()), ProductDescription_TextBox.Text.Trim());
                    UpdateSelectedProduct();
                    OpenProductsPage();
                }
                catch (ArgumentException ex)
                {

                    MessageBox.Show("Unable to change name.\n" + ex.Message);
                }
            }
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
            AdminProductsPage productsPage = new AdminProductsPage();
            productsPage.Show();
            this.Close();
        }

        private void PopulateTextBox()
        {
            ProductName_TextBox.Text = _productManager.SelectedProduct.ProductName;
            ProductRarity_TextBox.Text = _productManager.SelectedProduct.Rarity.ToString();
            ProductDescription_TextBox.Text = _productManager.SelectedProduct.Description;
            ProductPrice_TextBox.Text = _productManager.SelectedProduct.UnitPrice.ToString();
            ProductCategory_TextBox.Text = _productManager.SelectedProduct.Category;

        }
    }
}
