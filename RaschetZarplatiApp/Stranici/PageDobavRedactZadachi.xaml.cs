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
    /// Interaction logic for PageDobavRedactZadachi.xaml
    /// </summary>
    public partial class PageDobavRedactZadachi : Page
    {
        FailiDannih.Task _zadacha;

        public PageDobavRedactZadachi(FailiDannih.Task zadacha)
        {
            InitializeComponent();

            _zadacha = zadacha;
            var Ispolniteli = new List<int>(PodclucheniyeOdb.podcluchObj.Executor.Select(x => x.ID).ToList());
            CmbxExecutor.ItemsSource = (from i in PodclucheniyeOdb.podcluchObj.Executor
                                        join p in PodclucheniyeOdb.podcluchObj.User on i.ID equals p.ID
                                        select new { FIO = p.LastName + " " + p.FirstName + " " + p.MiddleName, i.ID }).ToList();
            DtprCreateDateTime.SelectedDate = DateTime.Today;
            DtprDeadline.SelectedDate = DateTime.Today;
            if (zadacha != null)
            {
                BtnYdalut.Visibility = Visibility.Visible;

                // Заполнение данных формы при редактировании
                ZagolovokObj.txtZag.Text = "Редактирование задачи";
                CmbxExecutor.SelectedIndex = Ispolniteli.IndexOf(zadacha.ExecutorID);
                TbxTitle.Text = zadacha.Title;
                TbxDescription.Text = zadacha.Description;
                DtprCreateDateTime.Text = Convert.ToString(zadacha.CreateDateTime);
                DtprDeadline.Text = Convert.ToString(zadacha.Deadline);
                TbxDifficulty.Text = Convert.ToString(zadacha.Difficulty);
                TbxTime.Text = Convert.ToString(zadacha.Time);
                CmbxStatus.SelectedIndex = SpravochnayaInformaciya.Statusi[zadacha.Status];
                CmbxWorkType.SelectedIndex = SpravochnayaInformaciya.HaracterZadachi[zadacha.WorkType];
                DtprCompletedDateTime.Text = Convert.ToString(zadacha.CompletedDateTime);
                CkbxPriznakUdalennosti.IsChecked = zadacha.IsDeleted;
            }
            else
            {
                ZagolovokObj.txtZag.Text = "Добавление задачи";
            }
        }

        private void BtnSohranit_Click(object sender, RoutedEventArgs e)
        {
            string tekstDannihIspolnitelya = $"{CmbxExecutor.SelectedItem}";
            string[] dannieIspolnitelya = tekstDannihIspolnitelya.Split(new string[] { "ID = " }, StringSplitOptions.RemoveEmptyEntries);
            int idIspolnitelya = Convert.ToInt32(dannieIspolnitelya[dannieIspolnitelya.Length - 1].Replace(" }", ""));
            bool isYdalIspolnitel = PodclucheniyeOdb.podcluchObj.User.Where(x => x.ID == idIspolnitelya).ToList()[0].IsDeleted;

            string dataVipolneniya = $"{DtprCompletedDateTime.SelectedDate}";
            if (dataVipolneniya == "01.01.0001 0:00:00")
            {
                dataVipolneniya = null;
            }

            if (isYdalIspolnitel)
            {
                MessageBox.Show("Выбранный исполнитель удалён. Выберите другого исполнителя для задачи.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (_zadacha != null) // Редактирование
                {
                    try
                    {
                        IEnumerable<FailiDannih.Task> zadachi = PodclucheniyeOdb.podcluchObj.Task.Where(x => x.ID == _zadacha.ID).AsEnumerable().Select(x =>
                        {
                            x.ExecutorID = idIspolnitelya;
                            x.Title = TbxTitle.Text;
                            x.Description = TbxDescription.Text;
                            x.CreateDateTime = (DateTime)DtprCreateDateTime.SelectedDate;
                            x.Deadline = (DateTime)DtprDeadline.SelectedDate;
                            x.Difficulty = Convert.ToInt32(TbxDifficulty.Text);
                            x.Time = Convert.ToInt32(TbxTime.Text);
                            x.Status = CmbxStatus.Text;
                            x.WorkType = CmbxWorkType.Text;
                            if (dataVipolneniya != null)
                            {
                                x.CompletedDateTime = (DateTime)DtprCompletedDateTime.SelectedDate;
                            }
                            x.IsDeleted = (bool)CkbxPriznakUdalennosti.IsChecked;

                            return x;
                        });

                        foreach (FailiDannih.Task zadacha in zadachi)
                        {
                            PodclucheniyeOdb.podcluchObj.Entry(zadacha).State = System.Data.Entity.EntityState.Modified;
                        }

                        PodclucheniyeOdb.podcluchObj.SaveChanges();
                        MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.Message.ToString());
                    }
                }
                else // Добавление
                {
                    try
                    {
                        FailiDannih.Task zadacha;
                        if (dataVipolneniya == null)
                        {
                            zadacha = new FailiDannih.Task()
                            {
                                ExecutorID = idIspolnitelya,
                                Title = TbxTitle.Text,
                                Description = TbxDescription.Text,
                                CreateDateTime = (DateTime)DtprCreateDateTime.SelectedDate,
                                Deadline = (DateTime)DtprDeadline.SelectedDate,
                                Difficulty = Convert.ToInt32(TbxDifficulty.Text),
                                Time = Convert.ToInt32(TbxTime.Text),
                                Status = CmbxStatus.Text,
                                WorkType = CmbxWorkType.Text,
                                IsDeleted = (bool)CkbxPriznakUdalennosti.IsChecked
                            };
                        }
                        else
                        {
                            zadacha = new FailiDannih.Task()
                            {
                                ExecutorID = idIspolnitelya,
                                Title = TbxTitle.Text,
                                Description = TbxDescription.Text,
                                CreateDateTime = (DateTime)DtprCreateDateTime.SelectedDate,
                                Deadline = (DateTime)DtprDeadline.SelectedDate,
                                Difficulty = Convert.ToInt32(TbxDifficulty.Text),
                                Time = Convert.ToInt32(TbxTime.Text),
                                Status = CmbxStatus.Text,
                                WorkType = CmbxWorkType.Text,
                                CompletedDateTime = Convert.ToDateTime(dataVipolneniya),
                                IsDeleted = (bool)CkbxPriznakUdalennosti.IsChecked
                            };
                        }


                        PodclucheniyeOdb.podcluchObj.Task.Add(zadacha);
                        PodclucheniyeOdb.podcluchObj.SaveChanges();
                        MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.Message.ToString());
                    }
                }
            }
        }

        private void BtnYdalut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PodclucheniyeOdb.podcluchObj.Task.Remove(_zadacha);
                PodclucheniyeOdb.podcluchObj.SaveChanges();
                NavigaciyaObj.frmNavig.GoBack();

                MessageBox.Show("Данные успешно удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }

        private void BtnNazad_Click(object sender, RoutedEventArgs e)
        {
            NavigaciyaObj.frmNavig.Navigate(new PageZadachi());
        }
    }
}
