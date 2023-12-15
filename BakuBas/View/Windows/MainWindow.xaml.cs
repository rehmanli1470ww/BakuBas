using BingMapLesson.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace BakuBas
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void MouseDownEvent(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CreditionalLoading(object sender, RoutedEventArgs e)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("AppSettings.json");
            var data = builder.Build();
            var key = data.GetSection("MyKeys")["key_1"];
            mymap.CredentialsProvider = new ApplicationIdCredentialsProvider(key);
        }
     
    }
}
