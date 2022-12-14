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
    /// Interaction logic for PageModuliSistemi.xaml
    /// </summary>
    public partial class PageModuliSistemi : Page
    {
        public PageModuliSistemi()
        {
            InitializeComponent();
        }

        private void BtnIspoliteli_Click(object sender, RoutedEventArgs e)
        {
            InfoNetModula();
        }

        private void BtnZadachi_Click(object sender, RoutedEventArgs e)
        {
            NavigaciyaObj.frmNavig.Navigate(new PageZadachi());
        }

        private void BtnYpravleniyeCoefficientami_Click(object sender, RoutedEventArgs e)
        {
            InfoNetModula();
        }

        private void BtnRaschetZarplati_Click(object sender, RoutedEventArgs e)
        {
            InfoNetModula();
        }

        private void InfoNetModula()
        {
            MessageBox.Show("Функционал всё ещё в разработке.", "Нет раздела", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
