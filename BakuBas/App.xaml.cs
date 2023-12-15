using BakuBas.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BakuBas
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Main(object sender, StartupEventArgs e)
        {
            var window = new MainWindow();
            window.DataContext = new ViewModelMainWindow();
            window.ShowDialog();
        }
    }
}
