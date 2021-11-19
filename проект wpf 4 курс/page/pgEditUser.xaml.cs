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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace проект_wpf_4_курс
{
    /// <summary>
    /// Логика взаимодействия для pgEditUser.xaml
    /// </summary>
    public partial class pgEditUser : Page
    {
        public auth UserCur;
        public pgEditUser(auth PickedUser)
        {
            UserCur = PickedUser;
            InitializeComponent();
            txtPol.Text += PickedUser.users.genders.gender;
            listGenders.ItemsSource = BaseConnect.BaseModel.genders.Where(x => x.id != PickedUser.users.gender).ToList();
            listGenders.SelectedValuePath = "id";//индексы пунктов списка
            listGenders.DisplayMemberPath = "gender";//выбор источника данных 



            string[] traits1 = new string[3];
            List<traits> traits2 = BaseConnect.BaseModel.traits.ToList();
            int i = 0;
            foreach (traits traits in traits2)
            {
                traits1[i] = traits.trait;
                i++;
            }
            ch1st.Content = traits1[0];
            ch2d.Content = traits1[1];
            ch3d.Content = traits1[2];

            List<users_to_traits> users_To_Traits = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == PickedUser.id).ToList();

            foreach (users_to_traits UTT in users_To_Traits)
            {
                if (ch1st.Content == UTT.traits.trait)
                {
                    ch1st.IsChecked = true;
                }
                if (ch2d.Content == UTT.traits.trait)
                {
                    ch2d.IsChecked = true;
                }
                if (ch3d.Content == UTT.traits.trait)
                {
                    ch3d.IsChecked = true;
                }
            }
            txtLogin.Text = PickedUser.login.ToString();
            txtPass.Text = PickedUser.password.ToString();
            txtName.Text = PickedUser.users.name.ToString();
            dtDr.Text = PickedUser.users.dr.ToString();


        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            UserCur.users.dr = (DateTime)dtDr.SelectedDate;
            UserCur.users.name = txtName.Text;
            UserCur.login = txtLogin.Text;
            UserCur.password = txtPass.Text;
            users_to_traits trait1 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == UserCur.id && x.id_trait == 1);
            users_to_traits trait2 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == UserCur.id && x.id_trait == 2);
            users_to_traits trait3 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == UserCur.id && x.id_trait == 3);
            try
            {
                if (listGenders.SelectedItem != null)
                {
                    UserCur.users.gender = (int)listGenders.SelectedValue;
                }
                if (ch1st.IsChecked == false && trait1 != null)
                {
                    BaseConnect.BaseModel.users_to_traits.Remove(trait1);
                }
                else if (ch1st.IsChecked == true && trait1 == null)
                {
                    CreateTrait(UserCur, 1);
                }
                if (ch2d.IsChecked == false && trait2 != null)
                {
                    BaseConnect.BaseModel.users_to_traits.Remove(trait2);
                }
                else if (ch2d.IsChecked == true && trait2 == null)
                {
                    CreateTrait(UserCur, 2);
                }
                if (ch3d.IsChecked == false && trait3 != null)
                {
                    BaseConnect.BaseModel.users_to_traits.Remove(trait3);
                }
                else if (ch3d.IsChecked == true && trait3 == null)
                {
                    CreateTrait(UserCur, 3);
                }
                BaseConnect.BaseModel.SaveChanges();
                MessageBox.Show("Данные пользователя изменены");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public static void CreateTrait(auth User, int i)
        {
            users_to_traits UTT = new users_to_traits();
            UTT.id_user = User.id;
            UTT.id_trait = i;
            BaseConnect.BaseModel.users_to_traits.Add(UTT);
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.Navigate(new pgAdminMenu());
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Image IMG = sender as System.Windows.Controls.Image;
            int ind = Convert.ToInt32(UserCur.id);
            users U = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);//запись о текущем пользователе
            usersimage UI = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id_user == ind);//получаем запись о картинке для текущего пользователя
            BitmapImage BI = new BitmapImage();
            if (UI != null)//если для текущего пользователя существует запись о его катринке
            {
                if (UI.path != null)//если присутствует путь к картинке
                {
                    BI = new BitmapImage(new Uri(UI.path, UriKind.Relative));
                }
                else//если присутствуют двоичные данные
                {
                    BI.BeginInit();//начать инициализацию BitmapImage (для помещения данных из какого-либо потока)
                    BI.StreamSource = new MemoryStream(UI.image);//помещаем в источник данных двоичные данные из потока
                    BI.EndInit();//закончить инициализацию
                }
            }
            else
            {

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
            }



            IMG.Source = BI;
        }

        private void listPhoto_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    


}

        
    

