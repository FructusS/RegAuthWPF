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
    /// Логика взаимодействия для moderatorWindow.xaml
    /// </summary>
    public partial class moderatorWindow : Window
    {
        public moderatorWindow()
        {
            InitializeComponent();
        }
        private void Button_Click_Task(object sender, RoutedEventArgs e)
        {
            string task = TaskBox.Text;
            if (task != null)
            {
                using (SignUpandInEntities dataEntities = new SignUpandInEntities())
                {
                    //запрос к базе данных для добавления данных пользователя 
                    //var query = dataEntities.Tas.Where(x =>
                    //    x.Login.Equals(loginText)).FirstOrDefault();
                    var query = dataEntities.Tasks.Where(x =>
                        x.Task.Equals(task)).FirstOrDefault();
                    if (query == null) /*проверка на уникальность данных*/
                    {
                        dataEntities.Tasks.Add(new Tasks() /*добавление логина и пароля в базу данных*/
                        {
                            Task = task,
                            Done = 0
                        });
                        dataEntities.SaveChanges(); /*сохранение данных в базе данных*/
                        MessageBox.Show("Вы успешно сохранили задание!");
                        TaskBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Такое задание есть");
                    }
                }
            }
            else
            {
                MessageBox.Show("Напишите задачу!");
            }
        }
    }
}
