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
    /// Логика взаимодействия для userWindow.xaml
    /// </summary>
    public partial class userWindow : Window
    {
        public userWindow()
        {
            InitializeComponent();
            LoadTask();
        }
        public void LoadTask()
        {
            using (SignUpandInEntities dataEntities = new SignUpandInEntities())
            {
                var query = from task1 in dataEntities.Tasks
                            select new
                            {
                                tasktext = task1.Task
                            };


                foreach (var item in query)
                {
                    
                    TextBlock1.Text = item.tasktext.ToString();                  
                }
            }
            
        }

    }
}
