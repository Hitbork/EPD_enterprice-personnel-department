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
using Xceed.Wpf.Toolkit;

namespace Enterprice_personell_department.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPageFamily.xaml
    /// </summary>
    public partial class AddEditPageFamily : Page
    {
        private ЧленыСемьиСотрудника _currentFamily = new ЧленыСемьиСотрудника();

        private Сотрудник _currentEmployee = new Сотрудник();

        public bool isRedacting = false;

        public string nameOfPage = "Family";

        public AddEditPageFamily()
        {
            InitializeComponent();
        }

        public AddEditPageFamily(Сотрудник _employee)
        {
            InitializeComponent();

            _currentEmployee = _employee;

            _currentFamily= EPDEntities.GetContext().ЧленыСемьиСотрудника.Where(x => x.id_Сотрудника == _currentEmployee.id_Сотрудника).FirstOrDefault();
            DataContext = _currentFamily;

            isRedacting = true;

            Save.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isRedacting)
                ReadTheFile();
            else
            {
                if (_currentFamily.id_ВидаРодства == 4)
                    KindOfRelationshipComboBox.SelectedIndex = 2;
                else if (_currentFamily.id_ВидаРодства == 3)
                    KindOfRelationshipComboBox.SelectedIndex = 3;
                else
                    KindOfRelationshipComboBox.SelectedIndex = _currentFamily.id_ВидаРодства - 1;
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
            SecondNameBox.Text = strings[1];
            NameBox.Text = strings[2];
            SurnameBox.Text = strings[3];

            switch (strings[4])
            {
                case "Муж":
                    KindOfRelationshipComboBox.SelectedIndex = 0;
                    break;
                case "Жена":
                    KindOfRelationshipComboBox.SelectedIndex = 1;
                    break;
                case "Сын":
                    KindOfRelationshipComboBox.SelectedIndex = 2;
                    break;
                case "Дочь":
                    KindOfRelationshipComboBox.SelectedIndex = 3;
                    break;
                case "Отец":
                    KindOfRelationshipComboBox.SelectedIndex = 4;
                    break;
                case "Мать":
                    KindOfRelationshipComboBox.SelectedIndex = 5;
                    break;
                case "Внук":
                    KindOfRelationshipComboBox.SelectedIndex = 6;
                    break;
                case "Внучка":
                    KindOfRelationshipComboBox.SelectedIndex = 7;
                    break;
                case "Дедушка":
                    KindOfRelationshipComboBox.SelectedIndex = 8;
                    break;
                case "Бабушка":
                    KindOfRelationshipComboBox.SelectedIndex = 9;
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
                writer.WriteAsync($"{KindOfRelationshipComboBox.Text}\n");
            }
        }

        public bool CheckForErrors()
        {
            if (String.IsNullOrEmpty(SecondNameBox.Text) ||
                String.IsNullOrEmpty(NameBox.Text) ||
                String.IsNullOrEmpty(KindOfRelationshipComboBox.Text))
            {
                MessageBox.Show("Не все данные заполнены!");
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
            if (CheckForErrors())
            {
                if (isRedacting)
                {
                    NavigationService?.Navigate(new AddEditPageJobTitle(_currentEmployee));
                }
                else
                {
                    AddToTempFile();
                    NavigationService?.Navigate(new AddEditPageJobTitle());
                }
            }
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForErrors())
                return;

            _currentFamily.Фамилия = SecondNameBox.Text;
            _currentFamily.Имя = NameBox.Text;
            _currentFamily.Отчество = SurnameBox.Text;

            if (KindOfRelationshipComboBox.SelectedIndex == 2)
                _currentFamily.id_ВидаРодства = 4;
            else if (KindOfRelationshipComboBox.SelectedIndex == 3)
                _currentFamily.id_ВидаРодства = 3;
            else
                _currentFamily.id_ВидаРодства = KindOfRelationshipComboBox.SelectedIndex + 1;

            EPDEntities.GetContext().SaveChanges();


            MessageBox.Show("Данные в БД успешно обновлены");
        }
    }
}
