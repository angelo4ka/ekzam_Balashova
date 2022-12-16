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
    /// Interaction logic for PageYpravleniyeKoefficientami.xaml
    /// </summary>
    public partial class PageYpravleniyeKoefficientami : Page
    {
        public PageYpravleniyeKoefficientami()
        {
            InitializeComponent();

            ZagolovokObj.txtZag.Text = "Управление коэффициентами";
            
            Manager menedger = PodclucheniyeOdb.podcluchObj.Manager.Where(x => x.ID == PolzovatelObj.Polsovayel.ID).ToList()[0];
            TbxGarantMinZpJun.Text = Convert.ToString(menedger.JuniorMinimum);
            TbxGarantMinZpMiddle.Text = Convert.ToString(menedger.MiddleMinimum);
            TbxGarantMinZpSenior.Text = Convert.ToString(menedger.SeniorMinimum);
            TbxAnalizProectir.Text = Convert.ToString(menedger.AnalysisCoefficient);
            TbxYstanovkaOboryd.Text = Convert.ToString(menedger.InstallationCoefficient);
            TbxTehobslyjgSoprovojgd.Text = Convert.ToString(menedger.SupportCoefficient);
            TbxSlojgnosti.Text = Convert.ToString(menedger.DifficultyCoefficient);
            TbxVremya.Text = Convert.ToString(menedger.TimeCoefficient);
            TbxAbstractVesVDengi.Text = Convert.ToString(menedger.ToMoneyCoefficient);
        }

        private void BtnSohranit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IEnumerable<Manager> menedgeri = PodclucheniyeOdb.podcluchObj.Manager.Where(x => x.ID == PolzovatelObj.Polsovayel.ID).AsEnumerable().Select(x =>
                {
                    x.JuniorMinimum = Convert.ToInt32(TbxGarantMinZpJun.Text);
                    x.MiddleMinimum = Convert.ToInt32(TbxGarantMinZpMiddle.Text);
                    x.SeniorMinimum = Convert.ToInt32(TbxGarantMinZpSenior.Text);
                    x.AnalysisCoefficient = Convert.ToDouble(TbxAnalizProectir.Text);
                    x.InstallationCoefficient = Convert.ToDouble(TbxYstanovkaOboryd.Text);
                    x.SupportCoefficient = Convert.ToDouble(TbxTehobslyjgSoprovojgd.Text);
                    x.DifficultyCoefficient = Convert.ToDouble(TbxSlojgnosti.Text);
                    x.TimeCoefficient = Convert.ToDouble(TbxVremya.Text);
                    x.ToMoneyCoefficient = Convert.ToDouble(TbxAbstractVesVDengi.Text);

                    return x;
                });

                foreach (Manager menedger in menedgeri)
                {
                    PodclucheniyeOdb.podcluchObj.Entry(menedger).State = System.Data.Entity.EntityState.Modified;
                }

                PodclucheniyeOdb.podcluchObj.SaveChanges();
                MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }

        private void BtnNazad_Click(object sender, RoutedEventArgs e)
        {
            NavigaciyaObj.frmNavig.Navigate(new PageModuliSistemi());
        }
    }
}
