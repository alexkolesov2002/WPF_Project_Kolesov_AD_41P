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
    /// Логика взаимодействия для pgRegister.xaml
    /// </summary>
    public partial class pgRegister : Page
    {
        public pgRegister()
        {
            InitializeComponent();
            listGenders.ItemsSource = BaseConnect.BaseModel.genders.ToList();//выбор источника данных 
            listGenders.SelectedValuePath = "id";//индексы пунктов списка
            listGenders.DisplayMemberPath = "gender";
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

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auth LoginAndPass = new auth() { login = txtLogin.Text, password = txtPass.Password, role = 2 };
                BaseConnect.BaseModel.auth.Add(LoginAndPass);//добавить в модель
                BaseConnect.BaseModel.SaveChanges();

                users User = new users() { name = txtName.Text, id = LoginAndPass.id, gender = (int)listGenders.SelectedValue, dr = (DateTime)dtDr.SelectedDate };
                BaseConnect.BaseModel.users.Add(User);//добавить в модель
                if (ch1st.IsChecked == true)
                {
                    users_to_traits users_To_Traits = new users_to_traits();
                    users_To_Traits.id_user = User.id;
                    users_To_Traits.id_trait = 1;
                    BaseConnect.BaseModel.users_to_traits.Add(users_To_Traits);
                }
                if (ch2d.IsChecked == true)
                {
                    users_to_traits users_To_Traits = new users_to_traits();
                    users_To_Traits.id_user = User.id;
                    users_To_Traits.id_trait = 2;
                    BaseConnect.BaseModel.users_to_traits.Add(users_To_Traits);
                }

                if (ch3d.IsChecked == true)
                {
                    users_to_traits users_To_Traits = new users_to_traits();
                    users_To_Traits.id_user = User.id;
                    users_To_Traits.id_trait = 3;
                    BaseConnect.BaseModel.users_to_traits.Add(users_To_Traits);
                }
                BaseConnect.BaseModel.SaveChanges();
                MessageBox.Show("Вы успешно зарегестрировались");
            }
            catch (Exception exp)
            {
                MessageBox.Show("Возникла  ошибка" + exp.Message);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.GoBack();
        }
    }
}
