using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {

        public Registration()
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

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Авторизация());
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintLogin.Visibility= Visibility.Visible;
            if (LoginBox.Text.Length > 0) 
                txtHintLogin.Visibility= Visibility.Hidden;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtHintPassword.Visibility= Visibility.Visible;
            if (PasswordBox.Password.Length > 0)
                txtHintPassword.Visibility= Visibility.Hidden;
        }

        private void ConfirmationPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtHintConfiramtionPassword.Visibility= Visibility.Visible;
            if (ConfirmationPasswordBox.Password.Length > 0)
                txtHintConfiramtionPassword.Visibility = Visibility.Hidden;
        }

        private void TokenBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintToken.Visibility= Visibility.Visible;
            if (TokenBox.Text.Length > 0) 
                txtHintToken.Visibility= Visibility.Hidden;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(LoginBox.Text)  || String.IsNullOrEmpty(PasswordBox.Password) 
                || String.IsNullOrEmpty(ConfirmationPasswordBox.Password) || String.IsNullOrEmpty(TokenBox.Text))
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            if (PasswordBox.Password != ConfirmationPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            string connectionString = "data source=EGA\\SQLEXPRESS;Initial Catalog=Enterprice_persennol_department;Integrated Security=true";
            
            using (SqlConnection connection =
           new SqlConnection(connectionString))
            {
                // Checking if there is an existing same login in DB
                bool isThereSameLogin = true;

                string queryExistingLogin = $"SELECT * from Пользователь WHERE Логин = '{LoginBox.Text}'";

                SqlCommand commandExisitngLogin = new SqlCommand(queryExistingLogin, connection);

                try
                {
                    connection.Open();
                    SqlDataReader readerLogin = commandExisitngLogin.ExecuteReader();

                    isThereSameLogin = readerLogin.HasRows;

                    readerLogin.Close();
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (isThereSameLogin)
                {
                    MessageBox.Show("Такой логин уже существует! Введите другой!");
                    return;
                }

                // Checking if there is token in DB
                bool isThereToken = false;

                string queryString = $"SELECT * from Токен WHERE Токен = {TokenBox.Text}";

                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    isThereToken= reader.HasRows;
                    
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (!isThereToken)
                {
                    MessageBox.Show("Такого токена не существует в базе");
                    return;
                }

                // Creating new user
                EPDEntities db = new EPDEntities();

                Пользователь user = new Пользователь()
                {
                    Логин = LoginBox.Text,
                    Пароль = GetHash(PasswordBox.Password),
                    Токен = TokenBox.Text
                };

                db.Пользователь.Add(user);

                db.SaveChanges();

                // Deleting token from existance 
                string queryForDelete = $"DELETE FROM Токен WHERE Токен = '{TokenBox.Text}'";

                SqlCommand commandForDelete = new SqlCommand(queryForDelete, connection);

                commandForDelete.ExecuteNonQuery();

                MessageBox.Show("Пользователь создан!");
            }

        }
    }
}
