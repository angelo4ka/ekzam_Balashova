﻿using RaschetZarplatiApp.FailiDannih;
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
        }
    }
}
