using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
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
        List<usersimage> USIM;
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
            sz.IsChecked = false;
            szz.IsChecked = false;
            szzz.IsChecked = false;
            RBReverse.IsChecked = false;
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
            System.Windows.Controls.Image IMG = sender as System.Windows.Controls.Image;
            int ind = Convert.ToInt32(IMG.Uid);
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

        private void AddAvatar_Click(object sender, RoutedEventArgs e)
        {

            Button BTN = (Button)sender;
            int ind = Convert.ToInt32(BTN.Uid);
            USIM = BaseConnect.BaseModel.usersimage.Where(x => x.id_user == ind).ToList();
            MessageBox.Show(Convert.ToString(USIM.Count()));
            if (USIM.Count == 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".jpg"; // задаем расширение по умолчанию
                openFileDialog.Filter = "Изображения |*.jpg;*.png"; // задаем фильтр на форматы файлов
                var result = openFileDialog.ShowDialog();
                if (result == true)//если файл выбран
                {
                    System.Drawing.Image UserImage = System.Drawing.Image.FromFile(openFileDialog.FileName);//создаем изображение
                    ImageConverter IC = new ImageConverter();//конвертер изображения в массив байт
                    byte[] ByteArr = (byte[])IC.ConvertTo(UserImage, typeof(byte[]));//непосредственно конвертация
                    usersimage UI = new usersimage() { id_user = ind, image = ByteArr };//создаем новый объект usersimage
                    BaseConnect.BaseModel.usersimage.Add(UI);//добавляем его в модель
                    BaseConnect.BaseModel.SaveChanges();//синхронизируем с базой
                    MessageBox.Show("Картинка пользователя добавлена в базу");
                }
                else
                {
                    MessageBox.Show("Вы не выбрали фото");
                }
            }
            else if (USIM.Count == 1)
            {
                usersimage usim = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id_user == ind);
                BaseConnect.BaseModel.usersimage.Remove(usim);
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".jpg"; // задаем расширение по умолчанию
                openFileDialog.Filter = "Изображения |*.jpg;*.png"; // задаем фильтр на форматы файлов
                var result = openFileDialog.ShowDialog();
                if (result == true)//если файл выбран
                {
                    System.Drawing.Image UserImage = System.Drawing.Image.FromFile(openFileDialog.FileName);//создаем изображение
                    ImageConverter IC = new ImageConverter();//конвертер изображения в массив байт
                    byte[] ByteArr = (byte[])IC.ConvertTo(UserImage, typeof(byte[]));//непосредственно конвертация
                    usersimage UI = new usersimage() { id_user = ind, image = ByteArr };//создаем новый объект usersimage
                    BaseConnect.BaseModel.usersimage.Add(UI);//добавляем его в модель
                    BaseConnect.BaseModel.SaveChanges();//синхронизируем с базой
                    MessageBox.Show("Картинка пользователя добавлена в базу");
                }
                else
                {
                    MessageBox.Show("Вы не выбрали фото");
                }
            }
          //  UserImage_Loaded(BTN, null);




        }
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            RadioButton RB = (RadioButton)sender;
            switch (RB.Uid)
            {
                case "name":
                    lu1 = lu1.OrderBy(x => x.name).ToList();
                    break;
                case "DR":
                    lu1 = lu1.OrderBy(x => x.dr).ToList();
                    break;
            }
            if (RBReverse.IsChecked == true) 
            {
                lu1.Reverse();
            }
            lbUsersList.ItemsSource = lu1;
        }

       

        private void TEST_Click_1(object sender, RoutedEventArgs e)
        {
            Button BTN = (Button)sender;
            int ind = Convert.ToInt32(BTN.Uid);
            USIM = BaseConnect.BaseModel.usersimage.Where(x => x.id_user == ind).ToList();
            MessageBox.Show(Convert.ToString(USIM.Count()));
        }
    }
}
