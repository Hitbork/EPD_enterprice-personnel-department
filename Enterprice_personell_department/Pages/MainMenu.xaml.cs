using System;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            DataGridEmployees.ItemsSource = EPDEntities.GetContext().Сотрудник.ToList();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddEditPageEmployee((sender as Button).DataContext as Сотрудник));
        }

        private void AddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEditPageEmployee(null));
        }

        private void DeleteEmployees_Click(object sender, RoutedEventArgs e)
        {
            var employeesForRemoving = DataGridEmployees.SelectedItems.Cast<Сотрудник>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {employeesForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);

            try
            {
                var getContext = EPDEntities.GetContext();
                getContext.Образование.RemoveRange(getContext.Образование.Where(x => employeesForRemoving.Select(i => i.id_Сотрудника).Contains(x.id_Сотрудника)));
                getContext.ЧленыСемьиСотрудника.RemoveRange(getContext.ЧленыСемьиСотрудника .Where(x => employeesForRemoving.Select(i => i.id_Сотрудника).Contains(x.id_Сотрудника)));
                getContext.Сотрудник.RemoveRange(employeesForRemoving);
                getContext.SaveChanges();
                MessageBox.Show("Данные успешно удалены!");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void UserPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new UserPage());
        }
    }
}
