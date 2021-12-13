using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void ConfirmData_Click(object sender, RoutedEventArgs e)
        {

            //получаем данные пользователя без разделителей
            string loginText = Login.Text.Trim();
            string passwordText = Password.Password;
            string passwordConfirmText = ConfirmPassword.Password;
            //string passwordText2 = passwordTxtBox.Text;
            //string passwordConfirmText2 = passwordconfTxtBox.Text;
            //проверка на пустое значение данных на пустоту
            if (loginText.Length > 4 && passwordText.Length >= 5 && passwordConfirmText.Length >= 5)
            {
                //проверка на символы в логине
                if (CheckLogin(loginText))
                {
                    //проверка на одинаковые пароли
                    if (passwordConfirmText == passwordText)
                    {
                        if (CheckPassword(passwordConfirmText) || CheckPassword(passwordText))
                        {
                            using (SignUpandInEntities dataEntities = new SignUpandInEntities())
                            {
                                //запрос к базе данных для добавления данных пользователя 
                                var query = dataEntities.Users.Where(x =>
                                    x.Login.Equals(loginText)).FirstOrDefault();
                                if (query == null) /*проверка на уникальность данных*/
                                {
                                    dataEntities.Users.Add(new Users() /*добавление логина и пароля в базу данных*/
                                    {
                                        Login = loginText,
                                        Password = passwordText,
                                        RoleID = 2
                                
                                    });
                                    dataEntities.SaveChanges(); /*сохранение данных в базе данных*/
                                    MessageBox.Show("Вы успешно зарегистрировались!");
                                    this.Close(); /*закрытие окна регистрации*/
                                }
                                else
                                {
                                    MessageBox.Show("Такой логин занят");
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Пароль не должен содержать пробел");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли не одинаковае");
                    }
                }
                else

                {
                    
                    MessageBox.Show("Неверные символ в логине");
                }
            }
            else
            {
                MessageBox.Show("пароль или логин короткий");
            }

        }
        private bool CheckLogin(string checkText)
        {
            bool result;
            string pattern = @"^\S[A-Za-z0-9_]{4,50}$";
            if (Regex.IsMatch(checkText, pattern))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        private bool CheckPassword(string checkText)
        {
            bool result;
            string pattern = @"^\S{3,50}$";
            if (Regex.IsMatch(checkText, pattern))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        private void ValidChar(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Допустимые символы : английские буквы,цифры 0 до 9, нижнее подчеркивание.", "Уведомление");
        }
    }
}

