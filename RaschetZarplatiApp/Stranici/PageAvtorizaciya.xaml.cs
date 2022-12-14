using RaschetZarplatiApp.FailiDannih;
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

namespace RaschetZarplatiApp.Stranici
{
    /// <summary>
    /// Interaction logic for PageAvtorizaciya.xaml
    /// </summary>
    public partial class PageAvtorizaciya : Page
    {
        public PageAvtorizaciya()
        {
            InitializeComponent();

            TxtData.Text = DateTime.Now.ToString("dd MMMM yyyy");
        }

        private void BtnVhod_Click(object sender, RoutedEventArgs e)
        {
            if (TbxLogin.Text == "" || PswbxParol.Password == "")
            {
                MessageBox.Show("Не все поля заполнены.\nДля продолжения авторизации заполните все поля.", "Незаполнены поля", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (TbxLogin.Text == "1" && PswbxParol.Password == "1111")
                {
                    MessageBox.Show("Выполнен вход под менеджером.", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                    PolzovatelObj.IsSyperPrava = true;
                }
                else if (TbxLogin.Text == "0" && PswbxParol.Password == "0000")
                {
                    MessageBox.Show("Выполнен вход под исполнителем.", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                    PolzovatelObj.IsSyperPrava = false;
                }
                else
                {
                    MessageBox.Show("Введены неверный логин и/или пароль.\nПопробуйте ввести данные заново.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
