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
using DLL;
namespace проект_wpf_4_курс
{
    /// <summary>
    /// Логика взаимодействия для pgDLL.xaml
    /// </summary>
    public partial class pgDLL : Page
    {
        Class1 Class1 = new Class1();
        List<users> users;
        List<string> names;
        public  List<DateTime> dates;
        public pgDLL()
        {
            InitializeComponent();
            
        }

        private void btnVozrast_Click(object sender, RoutedEventArgs e)
        {
            dates = new List<DateTime>();
            users = BaseConnect.BaseModel.users.ToList();
            foreach (users userss in users)
            {
                dates.Add(userss.dr);
            }
            MessageBox.Show("Срединй возраст пользователей " + Class1.VozrastUser(dates));
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            Users.Text = "";
            names = new List<string>();
            users = BaseConnect.BaseModel.users.ToList();
            foreach (users userss in users)
            {
                names.Add(userss.name);
            }

            if (txtName.Text == "")
            {
                MessageBox.Show("Введите параметр поиска");
            }
            else
            {

                if (Class1.ListUserow(names, txtName.Text).Count == 0)
                {
                    MessageBox.Show("Ничего не найдено");
                }
                else
                {
                    foreach (string u in Class1.ListUserow(names, txtName.Text))
                    {
                        Users.Text += u + "\n";
                    }
                }
            }
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Users.Text = "";
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.GoBack();
        }
    }
}

    