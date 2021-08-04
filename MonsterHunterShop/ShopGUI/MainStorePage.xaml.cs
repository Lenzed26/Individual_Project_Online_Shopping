﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShopBusiness;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for MainStorePage.xaml
    /// </summary>
    public partial class MainStorePage : Window
    {
        private ProductManager _productManager = new ProductManager();
        public MainStorePage()
        {
            InitializeComponent();
            PopulateListBox();
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


    }
}