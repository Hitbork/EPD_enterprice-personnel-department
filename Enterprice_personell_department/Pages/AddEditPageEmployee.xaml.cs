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
using System.IO;

namespace Enterprice_personell_department.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPageEmployee.xaml
    /// </summary>
    public partial class AddEditPageEmployee : Page
    {
        private Сотрудник _currentEmployee = new Сотрудник();

        public string nameOfPage = "Employee";

        public AddEditPageEmployee(Сотрудник selectedEmployee)
        {
            InitializeComponent();

            if (selectedEmployee != null)
                _currentEmployee = selectedEmployee;
            
            DataContext = _currentEmployee;
        }

        public void ReadTheFile()
        {
            string[] strings = File.ReadAllLines("temp.txt").Where(v => v.Trim().IndexOf(nameOfPage) != -1).ToArray();

            if (strings.Length == 0)
            {
                return;
            }

            string requiredLine = strings[0];

            strings = requiredLine.Split('|');

            // Something to check
            SecondNameBox.Text = strings[1];
            NameBox.Text = strings[2];
            SurnameBox.Text = strings[3];
            try
            {
                DateOfBirthDatepicker.SelectedDate = DateTime.Parse(strings[4]);
            }
            catch
            {

            }

            switch (strings[5])
            {
                case "Мужской":
                    GenderComboBox.SelectedIndex = 0;
                    break;
                case "Женский":
                    GenderComboBox.SelectedIndex = 1;
                    break;
            }

            CititzenshipBox.Text = strings[6];
            PhonenumberBox.Text = strings[7];

            switch (strings[8])
            {
                case "Бухгалтер":
                    JobTitleComboBox.SelectedIndex = 0;
                    break;
                case "Техник":
                    JobTitleComboBox.SelectedIndex = 1;
                    break;
                case "Техник-технолог":
                    JobTitleComboBox.SelectedIndex = 2;
                    break;
                case "Инженер-технолог":
                    JobTitleComboBox.SelectedIndex = 3;
                    break;
                case "Геолог":
                    JobTitleComboBox.SelectedIndex = 4;
                    break;
                case "Главный геолог":
                    JobTitleComboBox.SelectedIndex = 5;
                    break;
                case "Начальник отдела":
                    JobTitleComboBox.SelectedIndex = 6;
                    break;
                case "Слесарь":
                    JobTitleComboBox.SelectedIndex = 7;
                    break;
                case "Врач":
                    JobTitleComboBox.SelectedIndex = 8;
                    break;
                case "Контролер качества":
                    JobTitleComboBox.SelectedIndex = 9;
                    break;
            }
        }

        public void AddToTempFile()
        {
            string path = "temp.txt";

            File.WriteAllLines(path, File.ReadAllLines(path).Where(v => v.Trim().IndexOf(nameOfPage) == -1).ToArray());

            using (StreamWriter writer = new StreamWriter("temp.txt", true))
            {
                writer.WriteAsync($"{nameOfPage}|");
                writer.WriteAsync($"{SecondNameBox.Text}|");
                writer.WriteAsync($"{NameBox.Text}|");
                writer.WriteAsync($"{SurnameBox.Text}|");
                writer.WriteAsync($"{DateOfBirthDatepicker.SelectedDate}|");
                writer.WriteAsync($"{GenderComboBox.Text}|");
                writer.WriteAsync($"{CititzenshipBox.Text}|");
                writer.WriteAsync($"{PhonenumberBox.Text}|");
                writer.WriteAsync($"{JobTitleComboBox.Text}\n");
            }
        }


        private void Employee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Education_Click(object sender, RoutedEventArgs e)
        {
            AddToTempFile();
            NavigationService?.Navigate(new AddEditPageEducation());
        }

        private void FamilyMembers_Click(object sender, RoutedEventArgs e)
        {
            AddToTempFile();
            NavigationService?.Navigate(new AddEditPageFamily());
        }

        private void Address_Click(object sender, RoutedEventArgs e)
        {
            AddToTempFile();
            NavigationService?.Navigate(new AddEditPageAddress());
        }

        private void Job_title_Click(object sender, RoutedEventArgs e)
        {
            AddToTempFile();
            NavigationService?.Navigate(new AddEditPageJobTitle());
        }

        private void Passport_details_Click(object sender, RoutedEventArgs e)
        {
            AddToTempFile();
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

            if (DateOfBirthDatepicker.SelectedDate == null)
                errors.AppendLine("Укажите дату рождения!");
            else
                _currentEmployee.ДатаРождения = (DateTime)DateOfBirthDatepicker.SelectedDate;

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
                hintPhonenumber.Visibility = Visibility.Hidden;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReadTheFile();
        }
    }
}
