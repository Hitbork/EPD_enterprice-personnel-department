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
    /// Логика взаимодействия для AddEditPageEducation.xaml
    /// </summary>
    public partial class AddEditPageEducation : Page
    {
        public AddEditPageEducation()
        {
            InitializeComponent();
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPageEmployee(null));
        }

        private void Education_Click(object sender, RoutedEventArgs e)
        {
            
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
            NavigationService?.Navigate(new AddEditPagePassportDetails());
        }

        private void EducationalOrganizationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintEducationalOrganization.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(EducationalOrganizationBox.Text))
                hintEducationalOrganization.Visibility = Visibility.Hidden;
        }

        private void LevelOfEducationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintLevelOfEducation.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(LevelOfEducationBox.Text))
                hintLevelOfEducation.Visibility= Visibility.Hidden;
        }

        private void SeriesAndNumberOfDiplomaBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintSeriesAndNumberOfDiploma.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(SeriesAndNumberOfDiplomaBox.Text))
                hintSeriesAndNumberOfDiploma.Visibility= Visibility.Hidden;
        }

        private void SpecialityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintSpeciality.Visibility= Visibility.Visible;
            if(!String.IsNullOrEmpty(SpecialityBox.Text))
                hintSpeciality.Visibility= Visibility.Hidden;
        }

        private void QualificationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintQualification.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(QualificationBox.Text))
                hintQualification.Visibility= Visibility.Hidden;
        }
    }
}
