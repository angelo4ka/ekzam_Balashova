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
            TxtColichVipolnayemZadach.Text = $"{PoluchColVipolnZadach()}";
        }

        /// <summary>
        /// Получение количества выполняющихся задач
        /// </summary>
        /// <returns>Количество задач со статусом "исполняется"</returns>
        private int PoluchColVipolnZadach()
        {
            List<FailiDannih.Task> Zadachi = PodclucheniyeOdb.podcluchObj.Task.ToList();

            return Zadachi.Where(x => x.Status.Equals("исполняется")).ToList().Count;
        }

        private void BtnVhod_Click(object sender, RoutedEventArgs e)
        {
            if (TbxLogin.Text == "" || PswbxParol.Password == "")
            {
                MessageBox.Show("Не все поля заполнены.\nДля продолжения авторизации заполните все поля.", "Незаполнены поля", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                bool PodtvergzdeniyeVhoda = false;

                List<User> polzovateli = PodclucheniyeOdb.podcluchObj.User.ToList();
                foreach (User polzovatel in polzovateli)
                {
                    if ((polzovatel.Login == TbxLogin.Text) && (PswbxParol.Password == polzovatel.Password))
                    {
                        PodtvergzdeniyeVhoda = true;
                        PolzovatelObj.Polsovayel = polzovatel;

                        break;
                    }
                }

                if (!PodtvergzdeniyeVhoda)
                {
                    MessageBox.Show("Введены неверный логин и/или пароль.\nПопробуйте ввести данные заново.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    List<int> menedgeri = PodclucheniyeOdb.podcluchObj.Manager.Select(x => x.ID).ToList();

                    // Проверка статуса вошедшего пользователя
                    if (menedgeri.Contains(PolzovatelObj.Polsovayel.ID)) // Менеджер
                    {
                        MessageBox.Show("Выполнен вход под менеджером.", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                        PolzovatelObj.IsSyperPrava = true;

                        SpisokIspolniteleyObj.stplSpisIsp.Visibility = Visibility.Visible;

                        NavigaciyaObj.frmNavig.Navigate(new PageModuliSistemi());
                    }
                    else // Исполнитель
                    {
                        MessageBox.Show("Выполнен вход под исполнителем.", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                        PolzovatelObj.IsSyperPrava = false;

                        Executor ispolnitel = PodclucheniyeOdb.podcluchObj.Executor.Where(x => x.ID == PolzovatelObj.Polsovayel.ID).ToList()[0];
                        PolzovatelObj.Rang = ispolnitel.Grade;

                        InfoPoleIspolnitelyaObj.tbxInfoIspoln.Text = $"{PolzovatelObj.Polsovayel.LastName} " +
                            $"{PolzovatelObj.Polsovayel.FirstName} {PolzovatelObj.Polsovayel.MiddleName} " +
                            $"({ispolnitel.Grade})";
                        InfoPoleIspolnitelyaObj.stplInfoIspoln.Visibility = Visibility.Visible;

                        NavigaciyaObj.frmNavig.Navigate(new PageZadachi());
                    }

                    BlokirovkaObj.stplBlok.Visibility = Visibility.Visible;
                    BlokirovkaObj.stplVihod.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
