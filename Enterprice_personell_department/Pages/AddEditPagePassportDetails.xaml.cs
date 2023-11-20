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
    /// Логика взаимодействия для AddEditPagePassportDetails.xaml
    /// </summary>
    public partial class AddEditPagePassportDetails : Page
    {
        public AddEditPagePassportDetails()
        {
            InitializeComponent();
        }

        public string nameOfPage = "Passport";

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
            SeriesAndNumberPassportBox.Text = strings[1];
            IssuedByBox.Text = strings[2];
            try
            {
                DateOfBirthDatepicker.SelectedDate = DateTime.Parse(strings[3]);
            }
            catch
            {

            }
            DivisionCodeBox.Text = strings[4];
            PlaceOfBirthBox.Text = strings[5];
            LocationBox.Text = strings[6];
        }

        public void AddToTempFile()
        {
            string path = "temp.txt";

            File.WriteAllLines(path, File.ReadAllLines(path).Where(v => v.Trim().IndexOf(nameOfPage) == -1).ToArray());

            using (StreamWriter writer = new StreamWriter("temp.txt", true))
            {
                writer.WriteAsync($"{nameOfPage}|");
                writer.WriteAsync($"{SeriesAndNumberPassportBox.Text}|");
                writer.WriteAsync($"{IssuedByBox.Text}|");
                writer.WriteAsync($"{DateOfBirthDatepicker.SelectedDate}|");
                writer.WriteAsync($"{DivisionCodeBox.Text}|");
                writer.WriteAsync($"{PlaceOfBirthBox.Text}|");
                writer.WriteAsync($"{LocationBox.Text}\n");
            }
        }

        public bool CheckForErrors()
        {
            string regex = @"^\d{4} \d{6}$";

            if (!Regex.IsMatch(SeriesAndNumberPassportBox.Text, regex))
            {
                MessageBox.Show("Неправильно введёна серия и номер пасспорта!");
                return false;
            }

            regex = @"^\d{3}-\d{3}$";

            if (!Regex.IsMatch(DivisionCodeBox.Text, regex))
            {
                MessageBox.Show("Неправильно введёна серия и номер пасспорта!");
                return false;
            }

            if (String.IsNullOrEmpty(SeriesAndNumberPassportBox.Text) ||
                String.IsNullOrEmpty(IssuedByBox.Text) ||
                String.IsNullOrEmpty(DateOfBirthDatepicker.Text) ||
                String.IsNullOrEmpty(DivisionCodeBox.Text) ||
                String.IsNullOrEmpty(PlaceOfBirthBox.Text) ||
                String.IsNullOrEmpty(LocationBox.Text))
            {
                MessageBox.Show("Не все данные введены!");
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
            if (CheckForErrors())
            {
                AddToTempFile();
                NavigationService?.Navigate(new AddEditPageJobTitle());
            }
        }

        private void Passport_details_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SeriesAndNumberPassportBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintSeriesAndNumberPassport.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(SeriesAndNumberPassportBox.Text))
                hintSeriesAndNumberPassport.Visibility = Visibility.Hidden;
        }

        private void IssuedByBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintIssuedBy.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(IssuedByBox.Text))
                hintIssuedBy.Visibility = Visibility.Hidden;
        }

        private void DivisionCodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintDivisionCode.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(DivisionCodeBox.Text))
                hintDivisionCode.Visibility = Visibility.Hidden;
        }

        private void PlaceOfBirthBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintPlaceOfBirth.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(PlaceOfBirthBox.Text))
                hintPlaceOfBirth.Visibility = Visibility.Hidden;
        }

        private void LocationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintLocation.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(LocationBox.Text))
                hintLocation.Visibility = Visibility.Hidden;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReadTheFile();
        }

        private void SeriesAndNumberPassportBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsNumber(e.Text[0]) || e.Text[0] == ' '))
                e.Handled = true;
        }

        private void DivisionCodeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsNumber(e.Text[0]) || e.Text[0] == '-'))
                e.Handled = true;
        }
    }
}
