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
    /// Interaction logic for CreatHunterWindow.xaml
    /// </summary>
    public partial class CreateHunterPage : Window
    {
        private HunterManager _hunterManager = new HunterManager();
        public CreateHunterPage()
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

        private void CreateHunter_Click(object sender, RoutedEventArgs e)
        {
            if(HunterName_TextBox.Text == null || HunterLocation_TextBox == null || HunterName_TextBox.Text == String.Empty || HunterLocation_TextBox.Text == String.Empty)
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                try
                {
                    _hunterManager.Create(HunterName_TextBox.Text, HunterLocation_TextBox.Text);
                    HunterName_TextBox.Clear();
                    HunterLocation_TextBox.Clear();
                    AdminHuntersPage huntersPage = new AdminHuntersPage();
                    huntersPage.Show();
                    this.Close();
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show("User already exists.\nPlease enter a new name");
                }
            }
        }

        private void GoBackToHunters_Click(object sender, RoutedEventArgs e)
        {
            HunterName_TextBox.Clear();
            HunterLocation_TextBox.Clear();
            AdminHuntersPage huntersPage = new AdminHuntersPage();
            huntersPage.Show();
            this.Close();
        }
    }
}
