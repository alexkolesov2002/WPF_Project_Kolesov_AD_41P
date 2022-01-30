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
using System.Windows.Threading;

namespace проект_wpf_4_курс
{
    /// <summary>
    /// Логика взаимодействия для pgLogin.xaml
    /// </summary>
    public partial class pgLogin : Page
    {
        Random random = new Random();
        public pgLogin()
        {
            InitializeComponent();
        }

        auth CurrentUser;
        string kode;
        bool flagKode = false;
        string hash;

        private void btnAutorise_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Hash.IsChecked == true)
                { 
                hash = Convert.ToString(Convert.ToBase64String(Encoding.UTF8.GetBytes(txtPassword.Password)));
                CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == txtLogin.Text && x.password == hash);
                }
                else
                {
                    CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == txtLogin.Text && x.password == txtPassword.Password);
                }
                if (CurrentUser != null)
                {//сюда напишем алгоритм перехода на страницу в зависимости от роли
                    if (flagKode == false)
                    {
                        generateKey();
                        flagKode = true;
                    }
                    else if (kode == txtKey.Text)
                    {
                        switch (CurrentUser.role)
                        { 

                        case 1:
                            MessageBox.Show("Вы вошли как администратор");
                            LoadPages.switchPage.Navigate(new pgAdminMenu());
                            break;
                        case 2:
                        default:
                            MessageBox.Show("Вы вошли как обычный пользователь");
                            LoadPages.switchPage.Navigate(new pgInfo(CurrentUser));
                            break;
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Пользователь не зарегестрирован");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Возникла  ошибка" + exp.Message);
            }

        }

        private void btnRegistr_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.Navigate(new pgRegister());
        }

        private void btnDLL_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.Navigate(new pgDLL());
        }

        private void generateKey()
        {
            imgRefresh.IsEnabled = false;
            kode = "";

            for (int i = 0; i < 8; i++)
            {
                kode += ((char)random.Next(33, 122)).ToString();
            }
            txtKey.Text = kode;
            MessageBox.Show(kode, "введите код в соответствующее поле на форме.", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            imgRefresh.IsEnabled = true;
            kode = ((char)random.Next(33, 122)).ToString();
            // MessageBox.Show("время вышло");
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            generateKey();
            flagKode = true;
        }

        private void btnGRAPH_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.Navigate(new Graphxaml());
        }

        private void btnVideo_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.switchPage.Navigate( new pgVideo());
        }
    }
}
