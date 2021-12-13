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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RegAndAuth
{
    /// <summary>
    /// Логика взаимодействия для PersonalArea.xaml
    /// </summary>
    public partial class PersonalArea : Window
    {
        
      
        SignUpandInEntities dataEntities = new SignUpandInEntities();
        public PersonalArea()
        {
            InitializeComponent();
            
            UserRole();
            GetElem();


        }
        public void UserRole()
        {


            using (SignUpandInEntities dataEntities = new SignUpandInEntities())
            {
                var query = from users in dataEntities.Users
                            join role in dataEntities.UserRole on users.RoleID equals role.ID
                            where users.Login == Global.loginTextUser
                            select new
                            {                             
                                role = role.Role
                            };

                
                foreach (var item in query)

                    if (item.role == "user")
                    {
                        textBox1.Visibility = Visibility.Hidden;
                    }
                    else if (item.role == "admin")
                    {
                        textBox2.Visibility = Visibility.Hidden;
                        
                    }
                    else
                    {
                        textBox3.Visibility = Visibility.Hidden;
                    }



            }
            
        }
        public void GetElem()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                // Navigate to Url
                driver.Navigate().GoToUrl("https://https://spb-rtk.ru/%d1%80%d0%b0%d1%81%d0%bf%d0%b8%d1%81%d0%b0%d0%bd%d0%b8%d0%b5/.com");

                // Get all the elements available with tag name 'p'
                IList<IWebElement> elements = driver.FindElements(By.TagName("table"));
                foreach (IWebElement e in elements)
                {
                    MessageBox.Show(e.Text);
                }

            }
            finally
            {
                driver.Quit();
            }
        }
    }
    

}
