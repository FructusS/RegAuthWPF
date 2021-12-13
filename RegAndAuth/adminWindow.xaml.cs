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
    /// Логика взаимодействия для adminWindow.xaml
    /// </summary>
    public partial class adminWindow : Window
    {
        SignUpandInEntities dataEntities = new SignUpandInEntities();
        public adminWindow()
        {
            InitializeComponent();
            var query = from user in dataEntities.Users
                        join userrole in dataEntities.UserRole on user.RoleID equals userrole.ID
                        select new
                        {
                            User = user.Login,
                            Password = user.Password,
                            Role = userrole.Role
                           
                        };

            userDGrid.ItemsSource = query.ToList();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            new addUserPage().Show();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //var query = dataEntities.Users.Select(x => x.Login).AsEnumerable().ToList();
            //try
            //{
            //    dataEntities.Users.RemoveRange(query);
            //    dataEntities.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
           

        }
    }
}
