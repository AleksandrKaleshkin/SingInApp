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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void ClearLog()
        {
            textBoxLogin.ToolTip = null;
            textBoxLogin.Background = Brushes.Transparent;

            passBox.ToolTip = null;
            passBox.Background = Brushes.Transparent;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = passBox.Password.Trim();

            ClearLog();

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
            else
            {
                ClearLog();

                User authUser = null;
                using (AppContext db = new AppContext())
                {
                    authUser = db.Users.Where(b => b.Login == login && b.Pass == password).FirstOrDefault();
                }

                if (authUser != null)
                {
                    UserPageWindow userPageWindow = new UserPageWindow();
                    userPageWindow.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButton.OK );

            }
        }

        public void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }


    }
}
