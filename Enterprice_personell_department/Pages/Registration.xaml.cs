using System;
using System.Collections.Generic;
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
            if (String.IsNullOrEmpty(LoginBox.Text)  || String.IsNullOrEmpty(PasswordBox.Password) || String.IsNullOrEmpty(ConfirmationPasswordBox.Password) || String.IsNullOrEmpty(TokenBox.Text))
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            if (PasswordBox.Password != ConfirmationPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            DatabaseEntities db = new DatabaseEntities();

            Пользователь user = new Пользователь()
            {
                Логин = LoginBox.Text,
                Пароль = GetHash(PasswordBox.Password),
                Токен = TokenBox.Text
            };

            db.Пользователь.Add(user);

            db.SaveChanges();

            MessageBox.Show("Пользователь создан!");
        }
    }
}
