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
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        SignUpandInEntities dataEntities = new SignUpandInEntities();
        public ProductList()
        {
            InitializeComponent();
            var query = from product in dataEntities.Product
                        
                        
                        select new
                        {
                            Title = product.Title,
                            ArticleNumber = product.ArticleNumber,
                            Description = product.Description,
                            Cost = product.Cost

                        };
            ListView1.ItemsSource = query.ToList();
        }
    }
}
