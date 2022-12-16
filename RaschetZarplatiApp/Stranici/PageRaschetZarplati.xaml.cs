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
        }

        private void BtnNazad_Click(object sender, RoutedEventArgs e)
        {
            NavigaciyaObj.frmNavig.Navigate(new PageModuliSistemi());
        }

        private void BtnRaschitatiZarplaty_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmbxIspolnitel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TbxZarplata.Text = "";
        }
    }
}
