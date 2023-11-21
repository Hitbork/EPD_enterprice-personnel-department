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

        private Договор _currentAgreement= new Договор();

        private Сотрудник _currentEmployee = new Сотрудник();

        public bool isRedacting = false;

        public string nameOfPage = "Agreement";
        public AddEditPageJobTitle()
        {
            InitializeComponent();
        }

        public AddEditPageJobTitle(Сотрудник _employee)
        {
            InitializeComponent();

            _currentEmployee = _employee;

            _currentAgreement = EPDEntities.GetContext().Договор.Where(x => x.id_Договора == _currentEmployee.id_Договора).FirstOrDefault();
            DataContext = _currentAgreement;

            isRedacting = true;

            Save.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isRedacting)
                ReadTheFile();
            else
            {
                DateOfPreparationpicker.SelectedDate = _currentAgreement.ДатаСоставления;
                EmploymentDatepicker.SelectedDate = _currentAgreement.ДатаПриема;
                FireDatepicker.SelectedDate = _currentAgreement.ДатаУвольнение;

                switch (_currentAgreement.id_Работы)
                {
                    case 1:
                        TypeOfWorkComboBox.SelectedIndex = 2;
                        break;
                    case 2:
                        TypeOfWorkComboBox.SelectedIndex = 4;
                        break;
                    case 3:
                        TypeOfWorkComboBox.SelectedIndex = 3;
                        break;
                    case 4:
                        TypeOfWorkComboBox.SelectedIndex = 7;
                        break;
                    case 5:
                        TypeOfWorkComboBox.SelectedIndex = 0;
                        break;
                    case 6:
                        TypeOfWorkComboBox.SelectedIndex = 8;
                        break;
                    case 7:
                        TypeOfWorkComboBox.SelectedIndex = 1;
                        break;
                    case 8:
                        TypeOfWorkComboBox.SelectedIndex = 9;
                        break;
                    case 9:
                        TypeOfWorkComboBox.SelectedIndex = 5;
                        break;
                    case 10:
                        TypeOfWorkComboBox.SelectedIndex = 6;
                        break;
                }
            }
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
                if (isRedacting)
                    NavigationService?.Navigate(new AddEditPageEmployee(_currentEmployee));
                else
                {
                    AddToTempFile();
                    NavigationService?.Navigate(new AddEditPageEmployee(null));
                }
            }
        }

        private void Education_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                if (isRedacting)
                {
                    NavigationService?.Navigate(new AddEditPageEducation(_currentEmployee));
                }
                else
                {
                    AddToTempFile();
                    NavigationService?.Navigate(new AddEditPageEducation());
                }
            }
        }

        private void FamilyMembers_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                if (isRedacting)
                {
                    NavigationService?.Navigate(new AddEditPageFamily(_currentEmployee));
                }
                else
                {
                    AddToTempFile();
                    NavigationService?.Navigate(new AddEditPageFamily());
                }
            }
        }

        private void Address_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                if (isRedacting)
                {
                    NavigationService?.Navigate(new AddEditPageAddress(_currentEmployee));
                }
                else
                {
                    AddToTempFile();
                    NavigationService?.Navigate(new AddEditPageAddress());
                }
            }
        }

        private void Job_title_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Passport_details_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrors())
            {
                if (isRedacting)
                {
                    NavigationService?.Navigate(new AddEditPagePassportDetails(_currentEmployee));
                }
                else
                {
                    AddToTempFile();
                    NavigationService?.Navigate(new AddEditPagePassportDetails());
                }
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForErrors())
                return;

            _currentAgreement.ДатаСоставления = (DateTime)DateOfPreparationpicker.SelectedDate;
            _currentAgreement.ДатаПриема = (DateTime)EmploymentDatepicker.SelectedDate;
            _currentAgreement.ДатаУвольнение = (DateTime)FireDatepicker.SelectedDate;

            switch (TypeOfWorkComboBox.SelectedIndex)
            {
                case 0:
                    _currentAgreement.id_Работы = 5;
                    break;
                case 1:
                    _currentAgreement.id_Работы = 7;
                    break;
                case 2:
                    _currentAgreement.id_Работы = 1;
                    break;
                case 3:
                    _currentAgreement.id_Работы = 3;
                    break;
                case 4:
                    _currentAgreement.id_Работы = 2;
                    break;
                case 5:
                    _currentAgreement.id_Работы = 9;
                    break;
                case 6:
                    _currentAgreement.id_Работы = 10;
                    break;
                case 7:
                    _currentAgreement.id_Работы = 4;
                    break;
                case 8:
                    _currentAgreement.id_Работы = 6;
                    break;
                case 9: 
                    _currentAgreement.id_Работы = 5;
                    break;
            }

            EPDEntities.GetContext().SaveChanges();

            MessageBox.Show("Данные в БД успешно обновлены");
        }
    }
}
