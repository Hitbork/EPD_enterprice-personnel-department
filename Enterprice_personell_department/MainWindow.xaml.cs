using Enterprice_personell_department.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace Enterprice_personell_department
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;

        public MainWindow()
        {
            InitializeComponent();
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { CurrentTime.Content = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите закрыть?", "Exit window", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;

            if (page is Pages.Авторизация)
                ButtonBack.Visibility = Visibility.Hidden;
            else
                ButtonBack.Visibility = Visibility.Visible;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if(MainFrame.CanGoBack) MainFrame.GoBack();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "export";
            sfd.Filter = "Text Files|*.txt";
            
            if (sfd.ShowDialog() == false)
            {
                MessageBox.Show("Файл не сохранен!");
                return;
            }

            string path = sfd.FileName;

            StreamWriter sw = new StreamWriter(path);

            using (var db = new DatabaseEntities())
            {
                string IDLine = String.Join(":", db.Пользователь.Select(x => x.id_Пользователя));
                sw.Write(":");
                sw.WriteLine(IDLine);

                string LoginLine = String.Join(":", db.Пользователь.Select(x => x.Логин));
                sw.Write(":");
                sw.WriteLine(LoginLine);

                string PasswordLine = String.Join(":", db.Пользователь.Select(x => x.Пароль));
                sw.Write(":");
                sw.WriteLine(PasswordLine);

                string TokenLine = String.Join(":", db.Пользователь.Select(x => x.Токен));
                sw.Write(":");
                sw.WriteLine(TokenLine);
            }

            sw.Close();

            Process.Start("notepad.exe", path);
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                StreamReader sr = new StreamReader(ofd.FileName);
                while (!sr.EndOfStream)
                {
                    string[] lines = new string[4];
                    for (int i = 0; i < 4; i++)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(':');
                        line = "";
                        if (data.Length>=2)
                        {
                            for (int j = 1; j < data.Length; j++)
                            {
                                line += " ";
                                line += data[j];
                            }
                        }
                        lines[i] = line;
                    }
                    MessageBox.Show("Данные в файле: \nID: " + lines[0] + "\nЛогин: " + lines[1] + "\nПароль: " + lines[2] + "\nТокен: " + lines[3]);
                }
            } else
            {
                MessageBox.Show("Файл для импорта не выбран!");
            }
        }
    }
}
