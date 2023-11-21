using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Образование _currentEducation = new Образование();

        private Сотрудник _currentEmployee = new Сотрудник();

        public bool isRedacting = false;

        public string nameOfPage = "Education";
        public AddEditPageEducation()
        {
            InitializeComponent();
        }

        public AddEditPageEducation(Сотрудник _employee)
        {
            InitializeComponent();

            _currentEmployee = _employee;

            _currentEducation = EPDEntities.GetContext().Образование.Where(x => x.id_Сотрудника == _currentEmployee.id_Сотрудника).FirstOrDefault();
            DataContext = _currentEducation;

            isRedacting = true;

            Save.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isRedacting)
                ReadTheFile();
            else
                DateOfIssueDatepicker.SelectedDate = (DateTime)_currentEducation.ДатаВыдачи;
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
            EducationalOrganizationBox.Text = strings[1];
            LevelOfEducationBox.Text = strings[2];
            SeriesAndNumberOfDiplomaBox.Text = strings[3];

            try
            {
                DateOfIssueDatepicker.SelectedDate = DateTime.Parse(strings[4]);
            }
            catch
            {

            }

            SpecialityBox.Text = strings[5];
            QualificationBox.Text = strings[6];
        }

        public void AddToTempFile()
        {
            string path = "temp.txt";

            File.WriteAllLines(path, File.ReadAllLines(path).Where(v => v.Trim().IndexOf(nameOfPage) == -1).ToArray());

            using (StreamWriter writer = new StreamWriter("temp.txt", true))
            {
                writer.WriteAsync($"{nameOfPage}|");
                writer.WriteAsync($"{EducationalOrganizationBox.Text}|");
                writer.WriteAsync($"{LevelOfEducationBox.Text}|");
                writer.WriteAsync($"{SeriesAndNumberOfDiplomaBox.Text}|");
                writer.WriteAsync($"{DateOfIssueDatepicker.SelectedDate}|");
                writer.WriteAsync($"{SpecialityBox.Text}|");
                writer.WriteAsync($"{QualificationBox.Text}\n");
            }
        }

        public bool CheckForErrors()
        {
            string regex = @"^\d{6} \d{7}$";

            if (!Regex.IsMatch(SeriesAndNumberOfDiplomaBox.Text, regex))
            {
                MessageBox.Show("Неправильно введёна серия и номер диплома!");
                return false;
            }

            if (String.IsNullOrEmpty(EducationalOrganizationBox.Text) ||
                String.IsNullOrEmpty(LevelOfEducationBox.Text) ||
                String.IsNullOrEmpty(SeriesAndNumberOfDiplomaBox.Text) ||
                String.IsNullOrEmpty(DateOfIssueDatepicker.Text) ||
                String.IsNullOrEmpty(SpecialityBox.Text) ||
                String.IsNullOrEmpty(QualificationBox.Text))
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

        private void SeriesAndNumberOfDiplomaBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsNumber(e.Text[0]) || e.Text[0] == ' ')) {
                e.Handled = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForErrors())
                return;

            _currentEducation.НазваниеУчебнойОрганизации = EducationalOrganizationBox.Text;
            _currentEducation.УровеньОбразование = LevelOfEducationBox.Text;
            _currentEducation.СерияНомерДиплома = SeriesAndNumberOfDiplomaBox.Text;
            _currentEducation.ДатаВыдачи = (DateTime)DateOfIssueDatepicker.SelectedDate;
            _currentEducation.Специальность = SpecialityBox.Text;
            _currentEducation.Квалификация = QualificationBox.Text;

            EPDEntities.GetContext().SaveChanges();

            MessageBox.Show("Данные в БД успешно обновлены");
        }
    }
}
