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

namespace Enterprice_personell_department.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPageAddress.xaml
    /// </summary>
    public partial class AddEditPageAddress : Page
    {
        public AddEditPageAddress()
        {
            InitializeComponent();
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPageEmployee(null));
        }

        private void Education_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPageEducation());
        }

        private void FamilyMembers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPageFamily());
        }

        private void Address_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Job_title_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPageJobTitle());
        }

        private void Passport_details_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPagePassportDetails());
        }

        private void RegionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintRegion.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(hintRegion.Text))
                hintRegion.Visibility= Visibility.Hidden;
        }

        private void CityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintCity.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(CityBox.Text))
                hintCity.Visibility= Visibility.Hidden;
        }

        private void StreetBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintStreet.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(StreetBox.Text))
                hintStreet.Visibility= Visibility.Hidden;
        }

        private void HouseNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintHouseNumber.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(HouseNumberBox.Text))
                hintHouseNumber.Visibility= Visibility.Hidden;
        }

        private void FlatNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintFlatNumber.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(FlatNumberBox.Text))
                hintFlatNumber.Visibility= Visibility.Hidden;
        }
    }
}
