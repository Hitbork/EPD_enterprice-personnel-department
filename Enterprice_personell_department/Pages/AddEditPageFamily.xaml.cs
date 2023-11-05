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
    /// Логика взаимодействия для AddEditPageFamily.xaml
    /// </summary>
    public partial class AddEditPageFamily : Page
    {
        public AddEditPageFamily()
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

        private void SecondNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintSecondName.Visibility = Visibility.Visible;
            if (!String.IsNullOrEmpty(SecondNameBox.Text))
                hintSecondName.Visibility = Visibility.Hidden;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintName.Visibility = Visibility.Visible;
            if (!String.IsNullOrEmpty(NameBox.Text))
                hintName.Visibility = Visibility.Hidden;
        }

        private void SurnameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintSurname.Visibility = Visibility.Visible;
            if (!String.IsNullOrEmpty(SurnameBox.Text))
                hintSurname.Visibility = Visibility.Hidden;
        }
    }
}
