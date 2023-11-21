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
    /// Логика взаимодействия для AddEditPageAddress.xaml
    /// </summary>
    public partial class AddEditPageAddress : Page
    {
        private Адрес _currentAddress = new Адрес();

        private Сотрудник _currentEmployee = new Сотрудник();

        public bool isRedacting = false;

        public string nameOfPage = "Address";

        public AddEditPageAddress()
        {
            InitializeComponent();
        }

        public AddEditPageAddress(Сотрудник _employee)
        {
            InitializeComponent();

            _currentEmployee = _employee;

            _currentAddress = EPDEntities.GetContext().Адрес.Where(x => x.id_Адреса == _currentEmployee.id_Адреса).FirstOrDefault();
            DataContext = _currentAddress;

            isRedacting = true;

            Save.Visibility = Visibility.Visible;
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isRedacting)
                ReadTheFile();
            else
                FlatNumberBox.Text = _currentAddress.Квартира.ToString();
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
            RegionBox.Text = strings[1];
            CityBox.Text = strings[2];
            StreetBox.Text = strings[3];
            HouseNumberBox.Text = strings[4];
            FlatNumberBox.Text = strings[5];
        }

        public void AddToTempFile()
        {
            string path = "temp.txt";

            File.WriteAllLines(path, File.ReadAllLines(path).Where(v => v.Trim().IndexOf(nameOfPage) == -1).ToArray());

            using (StreamWriter writer = new StreamWriter("temp.txt", true))
            {
                writer.WriteAsync($"{nameOfPage}|");
                writer.WriteAsync($"{RegionBox.Text}|");
                writer.WriteAsync($"{CityBox.Text}|");
                writer.WriteAsync($"{StreetBox.Text}|");
                writer.WriteAsync($"{HouseNumberBox.Text}|");
                writer.WriteAsync($"{FlatNumberBox.Text}\n");
            }
        }

        public bool CheckForErrors()
        {
            if (String.IsNullOrEmpty(RegionBox.Text) ||
                String.IsNullOrEmpty(CityBox.Text) ||
                String.IsNullOrEmpty(StreetBox.Text) ||
                String.IsNullOrEmpty(HouseNumberBox.Text) ||
                String.IsNullOrEmpty(FlatNumberBox.Text))
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


        private void RegionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintRegion.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(hintRegion.Text))
                hintRegion.Visibility= Visibility.Hidden;
        }

        private void CityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintCity.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(CityBox.Text))
                hintCity.Visibility= Visibility.Hidden;
        }

        private void StreetBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintStreet.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(StreetBox.Text))
                hintStreet.Visibility= Visibility.Hidden;
        }

        private void HouseNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintHouseNumber.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(HouseNumberBox.Text))
                hintHouseNumber.Visibility= Visibility.Hidden;
        }

        private void FlatNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hintFlatNumber.Visibility= Visibility.Visible;
            if (!String.IsNullOrEmpty(FlatNumberBox.Text))
                hintFlatNumber.Visibility= Visibility.Hidden;
        }

        private void FlatNumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsNumber(e.Text[0]))
                e.Handled = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForErrors())
                return;

            _currentAddress.Область = RegionBox.Text;
            _currentAddress.Город = CityBox.Text;
            _currentAddress.Улица = StreetBox.Text;
            _currentAddress.Дом = HouseNumberBox.Text;
            _currentAddress.Квартира = Int32.Parse(FlatNumberBox.Text);

            EPDEntities.GetContext().SaveChanges();

            MessageBox.Show("Данные в БД успешно обновлены");
        }
    }
}
