using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
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

        public string path = "temp.txt";
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

        public bool CheckForPresenceOfPages()
        {
            string[] allStrings = File.ReadAllLines(path);

            string[] keys =
            {
                "Address",
                "Education",
                "Employee",
                "Family",
                "Agreement",
                "Passport"
            };

            string[] namesOfPages =
            {
                "адрес",
                "образование",
                "сотрудник",
                "члены семьи",
                "договор",
                "пасп. данные"
            };

            bool[] values = new bool[keys.Length];

            for (int i = 0; i < keys.Length; i++)
                values[i] = false;

            foreach (string str in allStrings)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (str.IndexOf(keys[i]) != -1)
                    {
                        values[i] = true;
                        break;
                    }
                }
            }

            StringBuilder missingPages = new StringBuilder();

            for (int i = 0; i < values.Length; i++)
            {
                if (i != values.Length - 1)
                {
                    if (!values[i])
                    {
                        missingPages.Append(namesOfPages + ", ");
                    }
                } else
                {
                    if (!values[i])
                    {
                        missingPages.Append(namesOfPages + ".");
                    }
                }
            }

            if (missingPages.Length > 0)
            {
                MessageBox.Show("В этих окнах не были заполнены данные: " + missingPages.ToString());
                return false;
            }

            return true;
        }

        private void SendInfo_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForPresenceOfPages())
            {
                return;
            }

            string[] address = File.ReadAllLines(path).Where(v => v.Trim().IndexOf("Address") != -1).ToArray()[0].Split('|'),
                education = File.ReadAllLines(path).Where(v => v.Trim().IndexOf("Education") != -1).ToArray()[0].Split('|'),
                employee = File.ReadAllLines(path).Where(v => v.Trim().IndexOf("Employee") != -1).ToArray()[0].Split('|'),
                family = File.ReadAllLines(path).Where(v => v.Trim().IndexOf("Family") != -1).ToArray()[0].Split('|'),
                agreement = File.ReadAllLines(path).Where(v => v.Trim().IndexOf("Agreement") != -1).ToArray()[0].Split('|'),
                passport = File.ReadAllLines(path).Where(v => v.Trim().IndexOf("Passport") != -1).ToArray()[0].Split('|');

            EPDEntities db = new EPDEntities();

            // Заносим информацию об адресе
            Адрес адрес = new Адрес()
            {
                Область = address[1],
                Город = address[2],
                Улица = address[3],
                Дом = address[4],
                Квартира = Int32.Parse(address[5])
            };

            var addedAddressEntity =db.Адрес.Add(адрес);

            db.SaveChanges();

            int address_id = addedAddressEntity.id_Адреса; 

            // Заносим информацию о догворе
            Договор договор = new Договор()
            {
                ДатаСоставления = DateTime.Parse(agreement[1]),
                ДатаПриема = DateTime.Parse(agreement[2])
            };

            if (!String.IsNullOrEmpty(agreement[3]))
                договор.ДатаУвольнение = DateTime.Parse(agreement[3]);

            switch (agreement[4])
            {
                case "IT-работы":
                    договор.id_Работы = 5;
                    break;
                case "Бухгалтерские работы":
                    договор.id_Работы = 7;
                    break;
                case "Геодезические работы":
                    договор.id_Работы = 1;
                    break;
                case "Земляные работы":
                    договор.id_Работы = 3;
                    break;
                case "Подготовительные работы":
                    договор.id_Работы = 2;
                    break;
                case "Работы с БД":
                    договор.id_Работы = 9;
                    break;
                case "Руководящие работы":
                    договор.id_Работы = 10;
                    break;
                case "Свайные работы":
                    договор.id_Работы = 4;
                    break;
                case "Устройство ПК":
                    договор.id_Работы = 6;
                    break;
                case "Финансовые работы":
                    договор.id_Работы = 8;
                    break;
            }

            var addedAgreementEntity = db.Договор.Add(договор);

            db.SaveChanges();

            int agreement_id = addedAgreementEntity.id_Договора;

            //Заносим информацию о паспортных данных
            ПаспортныеДанные паспортныеДанные = new ПаспортныеДанные()
            {
                НомерСерияПаспорта = passport[1],
                КемВыдан = passport[2],
                ДатаВыдачи = DateTime.Parse(passport[3]),
                КодПодразделения = passport[4],
                МестоРождения = passport[5],
                МестоЖительства = passport[6]
            };

            var addedPassportEntity = db.ПаспортныеДанные.Add(паспортныеДанные);

            db.SaveChanges();

            int passport_id = addedPassportEntity.id_Паспорта;

            //Заносим информацию о сотруднике
            Сотрудник сотрудник = new Сотрудник()
            {
                Фамилия = employee[1],
                Имя = employee[2],
                ДатаРождения = DateTime.Parse(employee[4]),
                Пол = employee[5],
                Гражданство = employee[6],
                Телефон = employee[7],
                id_Адреса = address_id,
                id_Договора = agreement_id,
                id_Паспорта = passport_id
            };

            if (!String.IsNullOrEmpty(employee[3]))
                сотрудник.Отчество = employee[3];

            switch (employee[8])
            {
                case "Бухгалтер":
                    сотрудник.id_Должности = 1;
                    break;
                case "Техник":
                    сотрудник.id_Должности = 2;
                    break;
                case "Техник-технолог":
                    сотрудник.id_Должности = 3;
                    break;
                case "Инженер-технолог":
                    сотрудник.id_Должности = 4;
                    break;
                case "Геолог":
                    сотрудник.id_Должности = 5;
                    break;
                case "Главный геолог":
                    сотрудник.id_Должности = 6;
                    break;
                case "Начальник отдела":
                    сотрудник.id_Должности = 7;
                    break;
                case "Слесарь":
                    сотрудник.id_Должности = 8;
                    break;
                case "Врач":
                    сотрудник.id_Должности = 9;
                    break;
                case "Контролер качества":
                    сотрудник.id_Должности = 10;
                    break;
            }

            var addedEmployeeEntity = db.Сотрудник.Add(сотрудник);

            db.SaveChanges();

            int employee_id = addedEmployeeEntity.id_Сотрудника;

            //Заносим информацию об образовании
            Образование образование = new Образование()
            {
                id_Сотрудника = employee_id,
                НазваниеУчебнойОрганизации = education[1],
                УровеньОбразование = education[2],
                СерияНомерДиплома = education[3],
                ДатаВыдачи = DateTime.Parse(education[4]),
                Специальность = education[5],
                Квалификация= education[6]
            };

            var addedEducationEntitiy = db.Образование.Add(образование);

            db.SaveChanges();

            int education_id = addedEducationEntitiy.id_Образования;


            //Заносим информацию о членах семьи
            ЧленыСемьиСотрудника членыСемьиСотрудника = new ЧленыСемьиСотрудника()
            {
                id_Сотрудника = employee_id,
                Фамилия = family[1],
                Имя = family[2]
            };

            if (String.IsNullOrEmpty(family[3]))
                членыСемьиСотрудника.Отчество = family[3];

            switch (family[4])
            {
                case "Муж":
                    членыСемьиСотрудника.id_ВидаРодства = 1;
                    break;
                case "Жена":
                    членыСемьиСотрудника.id_ВидаРодства = 2;
                    break;
                case "Сын":
                    членыСемьиСотрудника.id_ВидаРодства = 4;
                    break;
                case "Дочь":
                    членыСемьиСотрудника.id_ВидаРодства = 3;
                    break;
                case "Отец":
                    членыСемьиСотрудника.id_ВидаРодства = 5;
                    break;
                case "Мать":
                    членыСемьиСотрудника.id_ВидаРодства = 6;
                    break;
                case "Внук":
                    членыСемьиСотрудника.id_ВидаРодства = 7;
                    break;
                case "Внучка":
                    членыСемьиСотрудника.id_ВидаРодства = 8;
                    break;
                case "Дедушка":
                    членыСемьиСотрудника.id_ВидаРодства = 9;
                    break;
                case "Бабушка":
                    членыСемьиСотрудника.id_ВидаРодства = 10;
                    break;
            }

            var addedFamilyEntity = db.ЧленыСемьиСотрудника.Add(членыСемьиСотрудника);

            db.SaveChanges();
        }
    }
}
