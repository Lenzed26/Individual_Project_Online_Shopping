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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CentreScreen();
        }

        private void EnterButton_Clicked(object sender, RoutedEventArgs e)
        {
            MainStorePage msp = new MainStorePage();
            msp.Show();
            this.Close();
        }

        private void AdminButton_Clicked(object sender, RoutedEventArgs e)
        {
            AdminMainPage admin = new AdminMainPage();
            admin.Show();
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
