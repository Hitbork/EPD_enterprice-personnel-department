using System;
using System.Collections.Generic;
using System.IO;
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

        public string nameOfPage = "Agreement";

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
            try
            {
                DateOfPreparationpicker.SelectedDate = DateTime.Parse(strings[1]);
            }
            catch
            {

            }

            try
            {
                EmploymentDatepicker.SelectedDate = DateTime.Parse(strings[2]);
            }
            catch
            {

            }

            try
            {
                FireDatepicker.SelectedDate = DateTime.Parse(strings[3]);
            }
            catch
            {

            }
            
            switch (strings[4])
            {
                case "IT-работы":
                    TypeOfWorkComboBox.SelectedIndex = 0;
                    break;
                case "Бухгалтерские работы":
                    TypeOfWorkComboBox.SelectedIndex = 1;
                    break;
                case "Геодезические работы":
                    TypeOfWorkComboBox.SelectedIndex = 2;
                    break;
                case "Земляные работы":
                    TypeOfWorkComboBox.SelectedIndex = 3;
                    break;
                case "Подготовительные работы":
                    TypeOfWorkComboBox.SelectedIndex = 4;
                    break;
                case "Работы с БД":
                    TypeOfWorkComboBox.SelectedIndex = 5;
                    break;
                case "Руководящие работы":
                    TypeOfWorkComboBox.SelectedIndex = 6;
                    break;
                case "Свайные работы":
                    TypeOfWorkComboBox.SelectedIndex = 7;
                    break;
                case "Устройство ПК":
                    TypeOfWorkComboBox.SelectedIndex = 8;
                    break;
                case "Финансовые работы":
                    TypeOfWorkComboBox.SelectedIndex = 9;
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
                writer.WriteAsync($"{DateOfPreparationpicker.SelectedDate}|");
                writer.WriteAsync($"{EmploymentDatepicker.SelectedDate}|");
                writer.WriteAsync($"{FireDatepicker.SelectedDate}|");
                writer.WriteAsync($"{TypeOfWorkComboBox.Text}\n");
            }
        }

        public bool CheckForErrors()
        {
            if (String.IsNullOrEmpty(DateOfPreparationpicker.Text) ||
                String.IsNullOrEmpty(EmploymentDatepicker.Text) ||
                String.IsNullOrEmpty(FireDatepicker.Text) ||
                String.IsNullOrEmpty(TypeOfWorkComboBox.Text))
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }

            return true;
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                AddToTempFile();
                NavigationService?.Navigate(new AddEditPageEmployee(null));
            }
        }

        private void Education_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                AddToTempFile();
                NavigationService?.Navigate(new AddEditPageEducation());
            }
        }

        private void FamilyMembers_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                AddToTempFile();
                NavigationService?.Navigate(new AddEditPageFamily());
            }
        }

        private void Address_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                AddToTempFile();
                NavigationService?.Navigate(new AddEditPageAddress());
            }
        }

        private void Job_title_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Passport_details_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                AddToTempFile();
                NavigationService?.Navigate(new AddEditPagePassportDetails());
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReadTheFile();
        }
    }
}
