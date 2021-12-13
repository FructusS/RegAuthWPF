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

namespace RegAndAuth
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignUp(object sender, RoutedEventArgs e) /*открытие окна регистрации*/
        {
            new Registration().Show();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {   /*получаем введенные данные пользователя*/
            string loginText = Login.Text;
            string passwordText = Password.Password;
            //проверка на пустое значение данных 
            if (loginText.Length > 3 && passwordText.Length > 3)
            {
                using (SignUpandInEntities dataEntities = new SignUpandInEntities())
                {//запрос к базе данных для того чтобы найти данные пользователя
                    var query = dataEntities.Users.Where(
                        x => x.Login == loginText && x.Password == passwordText).FirstOrDefault();
                    if (query != null)
                    {
                        Global.loginTextUser = loginText;
                        MessageBox.Show("вы успешно зашли в аккаунт");
                        var query1 = from users in dataEntities.Users
                                     join role in dataEntities.UserRole on users.RoleID equals role.ID
                                     where users.Login == Global.loginTextUser
                                     select new
                                     {
                                         role = role.Role
                                     };
                        foreach (var item in query1)
                        {

                            switch (item.role)
                            {
                                case "Manager":
                                    new managerWindow().Show();
                                    break;
                                case "Admin":
                                    new adminWindow().Show();
                                    break;                     
                                case "Director":
                                    new DirectorWindow().Show();
                                    break;
                            }
                        }
                        this.Close(); /*закрытие окна авторизации*/
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите пароль или пароль");
            }
        }
    }
    public class Global
    {
        public static string loginTextUser;
    }
}
