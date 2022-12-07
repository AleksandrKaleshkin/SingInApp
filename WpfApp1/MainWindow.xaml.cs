using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        AppContext db;

        public MainWindow()
        {
            InitializeComponent();

            db = new AppContext();

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 300;
            doubleAnimation.Duration = TimeSpan.FromSeconds(3);
            RegButton.BeginAnimation(Button.WidthProperty, doubleAnimation);
        }

        private void ClearReg()
        {
            textBoxLogin.ToolTip = null;
            textBoxLogin.Background = Brushes.Transparent;

            passBox.ToolTip = null;
            passBox.Background = Brushes.Transparent;

            passBox_2.ToolTip = null;
            passBox_2.Background = Brushes.Transparent;

            textBoxEmail.ToolTip = null;
            textBoxEmail.Background = Brushes.Transparent;
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = passBox.Password.Trim();
            string password_2 = passBox_2.Password.Trim();
            string email = textBoxEmail.Text.ToLower();

            ClearReg();

            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Логин должен быть больше 4 символов";
                textBoxLogin.Background = Brushes.Crimson;
            }
            else if (password.Length < 5)
            {
                passBox.ToolTip = "Пароль должен быть больше 4 символов";
                passBox.Background = Brushes.Crimson;
            }

            else if (password != password_2)
            {
                passBox_2.ToolTip = "Не верно введен пароль";
                passBox_2.Background = Brushes.Crimson;
            }
            else if (email.Length < 5 || !email.Contains("@"))
            {
                textBoxEmail.ToolTip = "Некорректное эл. почта";
                textBoxEmail.Background = Brushes.Crimson;

            }
            else
            {
                ClearReg();

                User user = new User(login, email, password);

                db.Users.Add(user);
                db.SaveChanges();

                UserPageWindow userPageWindow = new UserPageWindow();
                userPageWindow.Show();
                Hide();
            }

        }

        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Hide();
        }
    }
}
