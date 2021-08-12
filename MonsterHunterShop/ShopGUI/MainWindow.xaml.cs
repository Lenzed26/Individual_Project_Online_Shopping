using System.Windows;

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
