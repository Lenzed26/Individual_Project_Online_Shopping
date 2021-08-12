using System;
using System.Windows;
using ShopBusiness;

namespace ShopGUI
{
    /// <summary>
    /// Interaction logic for UpdateHunterPage.xaml
    /// </summary>
    public partial class UpdateHunterPage : Window
    {
        HunterManager _hunterManager;
        public UpdateHunterPage(HunterManager hm)
        {
            _hunterManager = hm;
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

        private void UpdateHunter_Click(object sender, RoutedEventArgs e)
        {
            if (HunterName_TextBox.Text.Equals(_hunterManager.SelectedHunter.Name))
            {
                _hunterManager.Update(HunterName_TextBox.Text.Trim(), HunterLocation_TextBox.Text.Trim());
                UpdateSelectedHunter();
                OpenHuntersPage();
            }
            else
            {
                try
                {
                    _hunterManager.Update(_hunterManager.SelectedHunter.Name, HunterName_TextBox.Text.Trim(), HunterLocation_TextBox.Text.Trim());
                    UpdateSelectedHunter();
                    OpenHuntersPage();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Unable to change name.\n"+ex.Message);
                }
            }
        }

        private void OpenHuntersPage()
        {
            AdminHuntersPage huntersPage = new AdminHuntersPage();
            huntersPage.Show();
            this.Close();
        }

        private void UpdateSelectedHunter()
        {
            _hunterManager.SelectedHunter.Name = HunterName_TextBox.Text;
            _hunterManager.SelectedHunter.Location = HunterLocation_TextBox.Text;
        }

        private void GoBackToHunters_Click(object sender, RoutedEventArgs e)
        {
            HunterLocation_TextBox.Clear();
            HunterName_TextBox.Clear();
            OpenHuntersPage();
        }

        private void PopulateTextBox()
        {
            HunterLocation_TextBox.Text = _hunterManager.SelectedHunter.Location;
            HunterName_TextBox.Text = _hunterManager.SelectedHunter.Name;
        }
    }
}
