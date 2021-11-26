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
    /// Логика взаимодействия для pgInfo.xaml
    /// </summary>
    public partial class pgInfo : Page
    {
        public int puf;
        public pgInfo(auth CurrentUser)
        {
            InitializeComponent();
            try
            {
                puf = CurrentUser.id;
                tbName.Text = CurrentUser.users.name;
                tbDR.Text = CurrentUser.users.dr.ToString("yyyy MMMM dd");
                tbGender.Text = CurrentUser.users.genders.gender;
                //список из качеств личности авторизованного пользователя
                List<users_to_traits> LUTT = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == CurrentUser.id).ToList();
                foreach (users_to_traits UT in LUTT)
                {
                    tbTraits.Text += UT.traits.trait + "  ";
                }
               
                
            }
            catch
            (Exception exp)
            {
                MessageBox.Show("Возникла  ошибка" + exp.Message);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.GoBack();
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Image IMG = sender as System.Windows.Controls.Image;
            int ind = Convert.ToInt32(puf);
            users U = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);//запись о текущем пользователе
            usersimage UI = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id_user == ind && x.avatar == true);//получаем запись о картинке для текущего пользователя
            BitmapImage BI = new BitmapImage();
            if (UI != null )//если для текущего пользователя существует запись о его катринке
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
    }
}
