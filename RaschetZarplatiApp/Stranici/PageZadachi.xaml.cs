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
            }
            else
            {
                ZagolovokObj.txtZag.Text = "Мои задачи";
            }

            var Zadachi = FiltraciyaZadach(PodclucheniyeOdb.podcluchObj.Task.ToList());
            DtgdZadachi.ItemsSource = Zadachi;

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

            DtgdZadachi.ItemsSource = Zadachi;
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
    }
}
