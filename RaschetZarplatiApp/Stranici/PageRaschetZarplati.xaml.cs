using LibraryRaschetZarplati;
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
    /// Interaction logic for PageRaschetZarplati.xaml
    /// </summary>
    public partial class PageRaschetZarplati : Page
    {
        public PageRaschetZarplati()
        {
            InitializeComponent();

            ZagolovokObj.txtZag.Text = "Расчёт зарплаты";

            CmbxIspolniteli.ItemsSource = (from i in PodclucheniyeOdb.podcluchObj.Executor
                                           join p in PodclucheniyeOdb.podcluchObj.User on i.ID equals p.ID
                                           select new { FIO = p.LastName + " " + p.FirstName + " " + p.MiddleName, i.ID }).ToList();
        }

        private void BtnNazad_Click(object sender, RoutedEventArgs e)
        {
            NavigaciyaObj.frmNavig.Navigate(new PageModuliSistemi());
        }

        private void BtnRaschitatiZarplaty_Click(object sender, RoutedEventArgs e)
        {
            Executor ispolnitel = PoluchitIspolnitelya($"{CmbxIspolniteli.SelectedItem}");
            // Менеджер из базы данных
            Manager menegerBD = PodclucheniyeOdb.podcluchObj.Manager.Where(x => x.ID == ispolnitel.ManagerID).ToList()[0];

            Meneger meneger = new Meneger(menegerBD.ID, menegerBD.JuniorMinimum, menegerBD.MiddleMinimum, menegerBD.SeniorMinimum,
                menegerBD.AnalysisCoefficient, menegerBD.InstallationCoefficient, menegerBD.SupportCoefficient, menegerBD.TimeCoefficient,
                menegerBD.DifficultyCoefficient, menegerBD.ToMoneyCoefficient);

            int minZarplata = RaschetZarplati.OpredelitMinimymZarplati(ispolnitel.Grade, meneger);

            List<Zadacha> vipolnennieZadachi = PoluchitSpisVipolnZadach(ispolnitel.ID);

            TbxZarplata.Text = $"{RaschetZarplati.RaschitatZarplaty(minZarplata, meneger, vipolnennieZadachi):0.00} руб.";
        }

        private void CmbxIspolniteli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TbxZarplata.Text = "";
        }

        /// <summary>
        /// Получает выбранный на форме экземпляр исполнителя
        /// </summary>
        /// <param name="tekstDannihIspolnitelya">Текстовое представление выбранного значения на форме</param>
        /// <returns>Экземпляр исполниеля</returns>
        private static Executor PoluchitIspolnitelya(string tekstDannihIspolnitelya)
        {
            string[] dannieIspolnitelya = tekstDannihIspolnitelya.Split(new string[] { "ID = " }, StringSplitOptions.RemoveEmptyEntries);
            int idIspolnitelya = Convert.ToInt32(dannieIspolnitelya[dannieIspolnitelya.Length - 1].Replace(" }", ""));

            Executor ispolnitel = PodclucheniyeOdb.podcluchObj.Executor.Where(x => x.ID == idIspolnitelya).ToList()[0];

            return ispolnitel;
        }

        private static List<Zadacha> PoluchitSpisVipolnZadach(int idIspolnitelya)
        {
            List<Zadacha> vipolnZadachi = new List<Zadacha>();
            DateTime data = DateTime.Now;
            DateTime nachaloMesyaca = new DateTime(data.Year, data.Month, 1);
            DateTime nachaloSledMesyaca = nachaloMesyaca.AddMonths(1);
            List<FailiDannih.Task> vipolnZadachiBD = PodclucheniyeOdb.podcluchObj.Task.Where(x =>
                x.Status == "выполнена" && x.ExecutorID == idIspolnitelya &&
                (x.CompletedDateTime > nachaloMesyaca && x.CompletedDateTime < nachaloSledMesyaca)).ToList();

            foreach (FailiDannih.Task zadacha in vipolnZadachiBD)
            {
                vipolnZadachi.Add(new Zadacha(zadacha.ID, zadacha.ExecutorID, zadacha.Title, zadacha.Description,
                    zadacha.CreateDateTime, zadacha.Deadline, zadacha.Difficulty, zadacha.Time, zadacha.Status,
                    zadacha.WorkType, zadacha.CompletedDateTime, zadacha.IsDeleted));
            }

            return vipolnZadachi;
        }
    }
}
