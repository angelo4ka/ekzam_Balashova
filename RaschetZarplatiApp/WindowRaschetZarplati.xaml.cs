using RaschetZarplatiApp.FailiDannih;
using RaschetZarplatiApp.Stranici;
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

namespace RaschetZarplatiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PodclucheniyeOdb.podcluchObj = new ekzam_BalashovaEntities();
            ZagolovokObj.txtZag = TxtZagolovok;
            NavigaciyaObj.frmNavig = FrmNavig;
            BlokirovkaObj.stplBlok = StplOpoveshenieBlokirovki;
            BlokirovkaObj.stplVihod = StplVihodIzSistemi;
            SpisokIspolniteleyObj.cmbxSpisIsp = CmbxSpisokIspolniteley;
            SpisokIspolniteleyObj.stplSpisIsp = StplSpisokIspolniteley;
            InfoPoleIspolnitelyaObj.stplInfoIspoln = StplPoleInformaciiIspolnitelya;
            InfoPoleIspolnitelyaObj.tbxInfoIspoln = TbxInfoIspolnitelya;

            var Ispolniteli = new List<int>(PodclucheniyeOdb.podcluchObj.Executor.Select(x => x.ID).ToList());
            CmbxSpisokIspolniteley.ItemsSource = (from i in PodclucheniyeOdb.podcluchObj.Executor
                                                  join p in PodclucheniyeOdb.podcluchObj.User on i.ID equals p.ID
                                                  select new { FIO = p.LastName + " " + p.FirstName + " " + p.MiddleName, i.ID }).ToList();

            FrmNavig.Navigate(new PageAvtorizaciya());
        }

        private void BtnVihod_Click(object sender, RoutedEventArgs e)
        {
            StplOpoveshenieBlokirovki.Visibility = Visibility.Collapsed;
            StplVihodIzSistemi.Visibility = Visibility.Collapsed;
            SpisokIspolniteleyObj.cmbxSpisIsp.SelectedIndex = -1;
            SpisokIspolniteleyObj.stplSpisIsp.Visibility = Visibility.Collapsed;
            InfoPoleIspolnitelyaObj.stplInfoIspoln.Visibility = Visibility.Collapsed;
            InfoPoleIspolnitelyaObj.tbxInfoIspoln.Text = "";
            TxtZagolovok.Text = "Зарплата";

            FrmNavig.Navigate(new PageAvtorizaciya());
        }
    }
}
