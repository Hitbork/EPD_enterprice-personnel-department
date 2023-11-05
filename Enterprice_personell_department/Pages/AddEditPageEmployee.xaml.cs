using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data;
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
using System.Data.SqlClient;

namespace Enterprice_personell_department.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPageEmployee.xaml
    /// </summary>
    public partial class AddEditPageEmployee : Page
    {
        private Сотрудник _currentEmployee = new Сотрудник();

        

        public AddEditPageEmployee(Сотрудник selectedEmployee)
        {
            InitializeComponent();

            if (selectedEmployee != null)
                _currentEmployee = selectedEmployee;

            DataContext = _currentEmployee;
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {

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

        private void CititzenshipBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintCitizenship.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(CititzenshipBox.Text))
                hintCitizenship.Visibility= Visibility.Hidden;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentEmployee.Фамилия))
                errors.AppendLine("Введите фамилию!");

            if (string.IsNullOrWhiteSpace(_currentEmployee.Имя))
                errors.AppendLine("Введите имя!");

            if (_currentEmployee.Пол == null)
                errors.AppendLine("Выберите пол!");

            if (string.IsNullOrWhiteSpace(_currentEmployee.Отчество))
                errors.AppendLine("Введите отчество!");

            //if (DateOfBirthDatepicker.SelectedDate == null)
            //    errors.AppendLine("Укажите дату рождения!");
            //else
            //    _currentEmployee.ДатаРождения = DateOfBirthDatepicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(_currentEmployee.Гражданство))
                errors.AppendLine("Введите гражданство!");

            if (string.IsNullOrWhiteSpace(_currentEmployee.Телефон))
                errors.AppendLine("Укажите телефон!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentEmployee.id_Адреса = 1;
            _currentEmployee.id_Должности = 1;
            _currentEmployee.id_Паспорта = 3;
            _currentEmployee.id_Договора = 1;
        }

        private void PhonenumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintPhonenumber.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(PhonenumberBox.Text))
                hintPhonenumber.Visibility= Visibility.Hidden;
        }
    }
}
