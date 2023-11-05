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
    /// Логика взаимодействия для AddEditPagePassportDetails.xaml
    /// </summary>
    public partial class AddEditPagePassportDetails : Page
    {
        public AddEditPagePassportDetails()
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
            NavigationService?.Navigate(new AddEditPageAddress());
        }

        private void Job_title_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPageJobTitle());
        }

        private void Passport_details_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SeriesAndNumberPassportBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintSeriesAndNumberPassport.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(SeriesAndNumberPassportBox.Text))
                hintSeriesAndNumberPassport.Visibility = Visibility.Hidden;
        }

        private void IssuedByBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintIssuedBy.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(IssuedByBox.Text))
                hintIssuedBy.Visibility = Visibility.Hidden;
        }

        private void DivisionCodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintDivisionCode.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(DivisionCodeBox.Text))
                hintDivisionCode.Visibility = Visibility.Hidden;
        }

        private void PlaceOfBirthBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintPlaceOfBirth.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(PlaceOfBirthBox.Text))
                hintPlaceOfBirth.Visibility = Visibility.Hidden;
        }

        private void LocationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintLocation.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(LocationBox.Text))
                hintLocation.Visibility = Visibility.Hidden;
        }
    }
}
