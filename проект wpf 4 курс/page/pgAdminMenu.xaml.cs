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
        List<users> lu1;
        List<users> users;
        PageChange NewPage = new PageChange();
        public pgAdminMenu()
        {
            InitializeComponent();
           
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
            
            lbGenderFilter.ItemsSource = BaseConnect.BaseModel.genders.ToList();
            lbGenderFilter.SelectedValuePath = "id";
            lbGenderFilter.DisplayMemberPath = "gender";
            lu1 = users;
            lbUsersList.ItemsSource = users;
            DataContext = NewPage;

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
            lbUsersList.ItemsSource = users;//в качестве источника данных исходный список
            lu1 = users;
            lbGenderFilter.SelectedIndex = -1; //сбрасываем выбранный элемент списка
            txtNameFilter.Text = "";//сбрасываем фильтр на строку
            txtOT.Text = "";
            txtDO.Text = "";
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            int OT = Convert.ToInt32(txtOT.Text) - 1;//т.к. индексы начинаются с нуля
            int DO = Convert.ToInt32(txtDO.Text);
            lu1 = users.Skip(OT).Take(DO - OT).ToList();
            lbUsersList.ItemsSource = lu1;

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

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            Button btnedit = (Button)sender;
            int index = Convert.ToInt32(btnedit.Uid);
            auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == index);
            BaseConnect.BaseModel.auth.Remove(CurrentUser);
            BaseConnect.BaseModel.SaveChanges();
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
            
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.Navigate(new pgRegister());
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            lu1 = users;
                
            try
            {
                int OT = Convert.ToInt32(txtOT.Text) - 1;
                int DO = Convert.ToInt32(txtDO.Text);
                
                lu1 = users.Skip(OT).Take(DO - OT).ToList();
            }
            catch
            {
                
            }
         
            if (lbGenderFilter.SelectedValue != null)
            {
                lu1 = lu1.Where(x => x.gender == (int)lbGenderFilter.SelectedValue).ToList();
            }

           
            if (txtNameFilter.Text != "")
            {
                lu1 = lu1.Where(x => x.name.Contains(txtNameFilter.Text)).ToList();
            }

            lbUsersList.ItemsSource = lu1;
            NewPage.Countlist = lu1.Count;
        }

        private void UserImage_Loaded(object sender, RoutedEventArgs e)
        {
            Image IMG = sender as Image;
            int ind = Convert.ToInt32(IMG.Uid);
            users U = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);
            BitmapImage BI;
            switch (U.gender)
            {
                case 1:
                    BI = new BitmapImage(new Uri(@"/res/Dog.jpg", UriKind.Relative));
                    break;
                case 2:
                    BI = new BitmapImage(new Uri(@"/res/Panda.jpg", UriKind.Relative));
                    break;
                default:
                    BI = new BitmapImage(new Uri(@"/res/unnamed.jpg", UriKind.Relative));
                    break;
            }

            IMG.Source = BI;
        }

        private void txtPrev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;//определяем, какой текстовый блок был нажат           
            //изменение номера страници при нажатии на кнопку
            switch (tb.Uid)
            {
                case "prev":
                    NewPage.CurrentPage--;
                    break;
                case "next":
                    NewPage.CurrentPage++;
                    break;
                default:
                    NewPage.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }


            //определение списка
            lbUsersList.ItemsSource = lu1.Skip(NewPage.CurrentPage * NewPage.CountPage - NewPage.CountPage).Take(NewPage.CountPage).ToList();

            txtCurrentPage.Text = "Текущая страница: " + (NewPage.CurrentPage).ToString();
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                NewPage.CountPage = Convert.ToInt32(txtPageCount.Text);
            }
            catch
            {
                NewPage.CountPage = lu1.Count;
            }
            NewPage.Countlist = users.Count;
            lbUsersList.ItemsSource = lu1.Skip(0).Take(NewPage.CountPage).ToList();
        }
    }
}
