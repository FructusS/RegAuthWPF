using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;
using System.Drawing.Imaging;

namespace RegAndAuth
{
    /// <summary>
    /// Логика взаимодействия для managerWindow.xaml
    /// </summary>
    public partial class managerWindow : Window
    {
        byte[] imageToDB;
        string TitleProd;
        string ArticleNumber;
        string Desc;
        decimal CostProduct;
        public managerWindow()
        {
            InitializeComponent();
        }

        public void LoadData(object sender, RoutedEventArgs e)
        {


            string Title = TitleProduct.Text;
            string ArticleNumber = Article.Text;
            string Desc = Description.Text;
            string CostProd = Cost.Text;
            decimal CostProduct = Convert.ToDecimal(CostProd);

            using (SignUpandInEntities dataEntities = new SignUpandInEntities())
            {
                //запрос к базе данных для добавления данных данных о продукте
                var query = dataEntities.Product.Where(x => x.ArticleNumber == ArticleNumber || x.Title == Title || x.Cost == CostProduct).FirstOrDefault();
                if (query == null) /*проверка на уникальность данных о продукте*/
                {
                    dataEntities.Product.Add(new Product() /*добавление данных о продукте в базу данных*/
                    {
                       Title = Title,
                       ArticleNumber = ArticleNumber,
                       Description = Desc,
                       Cost = CostProduct,
                       Image = imageToDB

                    });
                    dataEntities.SaveChanges(); /*сохранение данных в базе данных*/
                    MessageBox.Show("Вы успешно добавили продукт!");
                    TitleProduct.Clear();
                    Article.Clear();
                    Description.Clear();
                    Cost.Clear();
                    
                }
                else
                {
                    MessageBox.Show("Такой продукт существует");
                }
            }
        }



        private void openCheckProductList(object sender, RoutedEventArgs e)
        {
            new ProductList().Show();
           
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"C:\";
            if (openFileDialog.ShowDialog() == true)
            {
                string pathFIle = System.IO.Path.GetFullPath(openFileDialog.FileName);
                txtEditor.Text = System.IO.Path.GetFileName(pathFIle);
                if (pathFIle != "")
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(pathFIle);
                    
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, ImageFormat.Png);
                        imageToDB = memoryStream.ToArray();
                      
                    }

                }
              
            }

        }
   
    }

}
