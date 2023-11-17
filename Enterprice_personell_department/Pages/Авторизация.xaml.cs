using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using System.Configuration;
using System.Security.Cryptography;

namespace Enterprice_personell_department.Pages
{
    /// <summary>
    /// Логика взаимодействия для Авторизация.xaml
    /// </summary>
    public partial class Авторизация : Page
    {
        bool LightTheme = true;

        public Авторизация()
        {
            InitializeComponent();
        }

        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintLogin.Visibility = Visibility.Visible;
            if (LoginBox.Text.Length > 0)
                txtHintLogin.Visibility = Visibility.Hidden;
        }

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (String.IsNullOrEmpty(LoginBox.Text) || String.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            string connectionString = "data source=EGA\\SQLEXPRESS;Initial Catalog=Enterprice_persennol_department;Integrated Security=true";

            string queryString = $"SELECT * from Пользователь WHERE Логин = '{LoginBox.Text}' AND Пароль = '{GetHash(PasswordBox.Password)}'";

            using (SqlConnection connection =
           new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        MessageBox.Show("Добро пожаловать в систему!");

                        NavigationService?.Navigate(new MainMenu());
                    }
                    else
                    {
                        reader.Close();
                        string queryStringForSearchingLogins = $"SELECT * from Пользователь WHERE Логин = '{LoginBox.Text}'";
                        SqlCommand commandForSearching = new SqlCommand(queryStringForSearchingLogins, connection);
                        SqlDataReader reader1 = commandForSearching.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            MessageBox.Show("Неправильный пароль!");
                        } else
                        {
                            MessageBox.Show("Пользователь с таким логином не найден!");
                        }
                        reader1.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Theme_Button_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("WindowStyle.xaml", UriKind.Relative);

            if (LightTheme)
            {
                // определяем путь к файлу ресурсов
                uri = new Uri("WindowStyle.xaml", UriKind.Relative);
                LightTheme = false;
            } else
            {
                // определяем путь к файлу ресурсов
                uri = new Uri("Dictionary.xaml", UriKind.Relative);
                LightTheme = true;
            }
            
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Registration());
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtHintPassword.Visibility = Visibility.Visible;
            if (PasswordBox.Password.Length > 0)
                txtHintPassword.Visibility = Visibility.Hidden;
        }
    }
}
