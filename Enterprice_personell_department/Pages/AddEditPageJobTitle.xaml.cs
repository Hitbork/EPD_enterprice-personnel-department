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
    /// Логика взаимодействия для AddEditPageJobTitle.xaml
    /// </summary>
    public partial class AddEditPageJobTitle : Page
    {
        public AddEditPageJobTitle()
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
            
        }

        private void Passport_details_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPagePassportDetails());
        }
    }
}
