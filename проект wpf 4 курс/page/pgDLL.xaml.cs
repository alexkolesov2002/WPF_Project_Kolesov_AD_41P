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
        public  List<DateTime> dates;
        public pgDLL()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
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
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = Class1.ListUserow(users, txtName.Text);
        }

        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //senser содержит объект, который вызвал данное событие, но только у него объектный тип, надо преобразовать
                ListBox lb = (ListBox)sender;//lb содержит ссылку на тот список, который вызвал это событие
                int index = Convert.ToInt32(lb.Uid);//получаем ID элемента списка. в данном случае оно совпадает с id user
                lb.ItemsSource = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == index).ToList();
                lb.DisplayMemberPath = "traits.trait";//показываем пользователю текстовое описание качества
            }
            catch (Exception exp)
            {
                MessageBox.Show("Возникла  ошибка" + exp.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.GoBack();
        }
    }
}

    