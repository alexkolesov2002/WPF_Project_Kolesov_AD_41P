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
    /// Логика взаимодействия для pgLogin.xaml
    /// </summary>
    public partial class pgLogin : Page
    {
        public pgLogin()
        {
            InitializeComponent();
        }

       
        private void btnAutorise_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == txtLogin.Text && x.password == txtPassword.Password);
                if (CurrentUser != null)
                {//сюда напишем алгоритм перехода на страницу в зависимости от роли
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
    }

       
}

