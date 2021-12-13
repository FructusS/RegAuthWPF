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

namespace RegAndAuth
{
    /// <summary>
    /// Логика взаимодействия для addUserPage.xaml
    /// </summary>
    public partial class addUserPage : Window
    {
        SignUpandInEntities dataEntities = new SignUpandInEntities();
        public addUserPage()
        {
            InitializeComponent();
            var query = dataEntities.UserRole.Select(x => x.Role);
            roleCombobox.ItemsSource = query.ToList();

             
        }

        private void btnAddUSer(object sender, RoutedEventArgs e)
        {
            //получаем логин и пароль пользователя
            string login = UserLogin.Text.Trim();
            string password = USerPassword.Text;

            //создаеи запрос на проверку уникальности данных
            var query = dataEntities.Users.Where(x =>
                                   x.Login.Equals(login)).FirstOrDefault();

            if (query == null) /*если в базе данных нет записей с логином пользователя,то добавляем в базу данных*/
            {
                dataEntities.Users.Add(new Users() /*добавление логина и пароля в базу данных*/
                {
                    Login = login,
                    Password = password,
                    RoleID = roleCombobox.SelectedIndex + 1

                });
                dataEntities.SaveChanges();
                MessageBox.Show("Данные успешно добвалены");
            }
            else
            {
                MessageBox.Show("Произошла ошибка");
            }

        }
        private void btnBack(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }
    }
}
