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
    /// Логика взаимодействия для pgEditUser.xaml
    /// </summary>
    public partial class pgEditUser : Page
    {

        public pgEditUser(auth PickedUser)
        {
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

            txtName.Text = PickedUser.users.name.ToString();
            dtDr.Text = PickedUser.users.dr.ToString();


        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.GoBack();
        }
    }
}
