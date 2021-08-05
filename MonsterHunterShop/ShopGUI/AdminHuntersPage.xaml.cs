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
    /// Interaction logic for AdminHuntersPage.xaml
    /// </summary>
    public partial class AdminHuntersPage : Window
    {
        private HunterManager _hunterManager = new HunterManager();
        public AdminHuntersPage()
        {
            InitializeComponent();
            CentreScreen();
            PopulateListView();
        }

        private void PopulateListView()
        {
            HuntersListView.ItemsSource = _hunterManager.RetrieveAllHunters();
        }

        private void BackToAdmin_Click(object sender, RoutedEventArgs e)
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

        private void CreateHunter_Window(object sender, RoutedEventArgs e)
        {
            CreateHunterPage hunterWindow = new CreateHunterPage();
            hunterWindow.Show();
            this.Close();
        }

        private void DeleteHunter_Click(object sender, RoutedEventArgs e)
        {
            _hunterManager.Delete(_hunterManager.SelectedHunter.Name);
            HuntersListView.ItemsSource = null;
            PopulateListView();
            HuntersListView.SelectedItem = null;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HuntersListView.SelectedItem != null)
            {
                _hunterManager.SetSelectedHunter(HuntersListView.SelectedItem);
            }
        }

        private void UpdateHunter_Window(object sender, RoutedEventArgs e)
        {
            UpdateHunterPage updateHunter = new UpdateHunterPage(_hunterManager);
            updateHunter.Show();
            this.Close();
        }
    }
}
