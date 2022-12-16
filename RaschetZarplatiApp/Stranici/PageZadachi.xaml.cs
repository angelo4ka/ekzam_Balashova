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
using System.Windows.Threading;

namespace RaschetZarplatiApp.Stranici
{
    /// <summary>
    /// Interaction logic for PageZadachi.xaml
    /// </summary>
    public partial class PageZadachi : Page
    {
        public PageZadachi()
        {
            InitializeComponent();

            if (PolzovatelObj.IsSyperPrava)
            {
                ZagolovokObj.txtZag.Text = "Управление задачами";
                BtnDobavitZadachy.Visibility = Visibility.Visible;
                BtnRedactirovatZadachy.Visibility = Visibility.Visible;
            }
            else
            {
                ZagolovokObj.txtZag.Text = "Мои задачи";
                BtnDobavitZadachy.Visibility = Visibility.Collapsed;
                BtnRedactirovatZadachy.Visibility = Visibility.Collapsed;
            }

            var Zadachi = FiltraciyaZadach(PodclucheniyeOdb.podcluchObj.Task.ToList());
            DtgdZadachi.ItemsSource = ZapolnitDanniyeIspolnitela(Zadachi);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();
        }

        public void UpdateData(object sender, object e)
        {
            // Получаем все записи из БД
            var Zadachi = PodclucheniyeOdb.podcluchObj.Task.ToList();
            Zadachi = FiltraciyaZadach(Zadachi);
            if (SpisokIspolniteleyObj.cmbxSpisIsp.SelectedIndex > -1)
            {
                Zadachi = VivestiZadachiIspolnitelya(Zadachi);
            }

            DtgdZadachi.ItemsSource = ZapolnitDanniyeIspolnitela(Zadachi);
        }

        /// <summary>
        /// Фильтрация задач по их статусу
        /// </summary>
        /// <param name="Zadachi">Список задач</param>
        /// <returns>Список отфильтрованных задач</returns>
        private List<FailiDannih.Task> FiltraciyaZadach(List<FailiDannih.Task> Zadachi)
        {
            List<FailiDannih.Task> FiltrovanniyeZadachi = new List<FailiDannih.Task>();

            if (RbtnVse.IsChecked == true)
            {
                return Zadachi;
            }
            else
            {
                if (RbtnZaplanirovana.IsChecked == true)
                {
                    FiltrovanniyeZadachi.AddRange(Zadachi.Where(x => x.Status.Equals(RbtnZaplanirovana.Content)).ToList());
                }
                if (RbtnIspolnyaetsa.IsChecked == true)
                {
                    FiltrovanniyeZadachi.AddRange(Zadachi.Where(x => x.Status.Equals(RbtnIspolnyaetsa.Content)).ToList());
                }
                if (RbtnVipolnena.IsChecked == true)
                {
                    FiltrovanniyeZadachi.AddRange(Zadachi.Where(x => x.Status.Equals(RbtnVipolnena.Content)).ToList());
                }
                if (RbtnOtmenena.IsChecked == true)
                {
                    FiltrovanniyeZadachi.AddRange(Zadachi.Where(x => x.Status.Equals(RbtnOtmenena.Content)).ToList());
                }
            }

            return FiltrovanniyeZadachi;
        }

        /// <summary>
        /// Фильтрация задач по выбранному исполнителю
        /// </summary>
        /// <param name="Zadachi">Список задач</param>
        /// <returns>Список отфильтрованных задач по исполнителю</returns>
        private List<FailiDannih.Task> VivestiZadachiIspolnitelya(List<FailiDannih.Task> Zadachi)
        {
            string tekstDannihIspolnitelya = $"{SpisokIspolniteleyObj.cmbxSpisIsp.SelectedItem}";
            string[] dannieIspolnitelya = tekstDannihIspolnitelya.Split(new string[] { "ID = " }, StringSplitOptions.RemoveEmptyEntries);
            int idIspolnitelya = Convert.ToInt32(dannieIspolnitelya[dannieIspolnitelya.Length - 1].Replace(" }", ""));

            Zadachi = Zadachi.Where(x => x.ExecutorID.Equals(idIspolnitelya)).ToList();

            return Zadachi;
        }

        /// <summary>
        /// Заполнение данных об исполнителе
        /// </summary>
        /// <param name="Zadachi">Список задач</param>
        /// <returns>Исполнители с заполненной информацией</returns>
        private List<FailiDannih.Task> ZapolnitDanniyeIspolnitela(List<FailiDannih.Task> Zadachi)
        {
            var Polzovateli = PodclucheniyeOdb.podcluchObj.User.ToList();
            var Ispolniteli = PodclucheniyeOdb.podcluchObj.Executor.ToList();

            foreach (var zadacha in Zadachi)
            {
                var polzovatel = Polzovateli.Where(x => x.ID.Equals(zadacha.ExecutorID)).ToList();
                var isponitel = Ispolniteli.Where(x => x.ID.Equals(zadacha.ExecutorID)).ToList();
                zadacha.ExecutorTekst = $"{polzovatel[0].LastName} {polzovatel[0].FirstName} {polzovatel[0].MiddleName} ({isponitel[0].Grade})";
            }

            return Zadachi;
        }

        private void BtnDobavitZadachy_Click(object sender, RoutedEventArgs e)
        {
            NavigaciyaObj.frmNavig.Navigate(new PageDobavRedactZadachi(null));
        }

        private void BtnRedactirovatZadachy_Click(object sender, RoutedEventArgs e)
        {
            FailiDannih.Task zadacha = (FailiDannih.Task)DtgdZadachi.SelectedItem;

            if (zadacha != null)
            {
                NavigaciyaObj.frmNavig.Navigate(new PageDobavRedactZadachi(zadacha));
            }
            else
            {
                MessageBox.Show("Не выделена строка для редактирования.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnNazad_Click(object sender, RoutedEventArgs e)
        {
            NavigaciyaObj.frmNavig.Navigate(new PageModuliSistemi());
        }
    }
}
