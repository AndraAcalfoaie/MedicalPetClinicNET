using Services;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDoctorService _doctorService;

        public MainWindow(IDoctorService doctorService)
        {
            _doctorService = doctorService;

            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var doctors = _doctorService.GetAll();
        }
    }
}
