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

namespace проект_wpf_4_курс
{
    /// <summary>
    /// Логика взаимодействия для pgAdminMenu.xaml
    /// </summary>
    public partial class pgAdminMenu : Page
    {
        List<users> users;
        public pgAdminMenu()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
            
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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnedit = (Button)sender;
                int index = Convert.ToInt32(btnedit.Uid);
                auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == index);
                MessageBox.Show("" + index);
                LoadPages.switchPage.Navigate(new pgEditUser(CurrentUser));
            }
            catch (Exception exp)
            {
                MessageBox.Show("Возникла  ошибка" + exp.Message);
            }
        }
    }
}
