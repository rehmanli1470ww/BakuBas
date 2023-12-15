using BakuBas.commands;
using BakuBas.UserControllers;
using BingMapLesson.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BakuBas.ViewModels
{
    public class ViewModelMainWindow : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberNameAttribute] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        public List<AftoBus>? buses { get; set; }
        public List<string> busesNames { get; set; }
        public ICommand ChangeVisibilityCommand { get; set; }
        public ICommand SearcedCommon { get; set; }
        public ICommand CloseCommand { get; set; }
        public ViewModelMainWindow()
        {
            List<AftoBus>? buses = new((JsonSerializer.Deserialize<BakuBus>(File.ReadAllText("Database//JsonFiles//Buses.json")))?.BUS);
            ChangeVisibilityCommand = new Command(ExecuteChangeVisibilityCommand, CanExecuteChangeVisibilityCommand);
            SearcedCommon = new Command(ExecuteSearcedCommon, CanSearcedCommon);
            CloseCommand = new Command(ExecuteCloseCommand, CanCloseCommand);
            busesNames = new();
            busesNames?.Add("All Busess");
            foreach (var bus in buses)
            {
                bool check = true;
                foreach (var busName in busesNames)
                {
                    if (bus.attributes.DISPLAY_ROUTE_CODE == busName)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                    busesNames?.Add((bus.attributes.DISPLAY_ROUTE_CODE));
            }

        }
        public void ExecuteCloseCommand(object? param)
        {
          ((Window)param!).Close();
        }

        public bool CanCloseCommand(object? param) => true;
        public List<AftoBus> filldata()
        {
            return buses = (JsonSerializer.Deserialize<BakuBus>(File.ReadAllText("Database//JsonFiles//Buses.json")))?.BUS!;
        }
        public void ExecuteChangeVisibilityCommand(object? param)
        {

            var map = param as Map;
            if (map.Visibility == Visibility.Hidden)
                map.Visibility = Visibility.Visible;
            else
                map.Visibility = Visibility.Hidden;

        }

        public bool CanExecuteChangeVisibilityCommand(object? param)=>true;
        public void ExecuteSearcedCommon(object? param)
        {
            ResourceDictionary? resourceDictionary = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("CustomPushPinStyle.xaml"));
            Style? customPushpinStyle = resourceDictionary!["CustomPushpinStyle"] as Style;
            Window? thisWindow = (Window)param!;
            Border border = (Border)thisWindow.FindName("border");
            Grid grid = (Grid)border.FindName("grid");
            Map? mymap = (Map)grid.FindName("mymap");
            StackPanel stackPanel = (StackPanel)grid.FindName("stackpanel");
            ComboBox comboBox = (ComboBox)stackPanel.FindName("combobox");
            buses = filldata();
            mymap.Children.Clear();

            foreach (var bus in buses)
            {
                if (comboBox.SelectedValue.ToString() == bus.attributes.DISPLAY_ROUTE_CODE || comboBox.SelectedValue.ToString() == "All Busess")
                {
                    mymap.Children.Add(new Pushpin()
                    {
                        Location = new Location(double.Parse(bus.attributes.LATITUDE, new CultureInfo("tr-TR")), double.Parse(bus.attributes.LONGITUDE, new CultureInfo("tr-TR"))),
                    });
                    UCPushPinContent content = new();
                    content.PrevStop.Text = bus.attributes.PREV_STOP;
                    ToolTipService.SetToolTip(((Pushpin)mymap.Children[mymap.Children.Count - 1]), content);
                    Brush pushPinBackground = new SolidColorBrush(((Color)ColorConverter.ConvertFromString(BusColors.colors[bus.attributes.DISPLAY_ROUTE_CODE])));
                    ((Pushpin)mymap.Children[mymap.Children.Count - 1]).Background = pushPinBackground;
                    ((Pushpin)mymap.Children[mymap.Children.Count - 1]).Style = customPushpinStyle;

                }
            }
        }
        public bool CanSearcedCommon(object? param)
        {
            Window? thisWindow = (Window)param!;
            Border border = (Border)thisWindow.FindName("border");
            Grid grid = (Grid)border.FindName("grid");
            StackPanel stackPanel = (StackPanel)grid.FindName("stackpanel");
            ComboBox comboBox = (ComboBox)stackPanel.FindName("combobox");
            return comboBox.SelectedIndex != -1;
        }
       
    }


    public static class BusColors
    {
        static public Dictionary<string, string> colors = new Dictionary<string, string>
{
    { "7A", "Red" },
    { "14", "Blue" },
    { "2", "Green" },
    { "205", "Yellow" },
    { "3", "Orange" },
    { "17", "Purple" },
    { "88", "Pink" },
    { "6", "Turquoise" },
    { "7B", "Gray" },
    { "1", "Black" },
    { "88A", "White" },
    { "125", "Gold" },
    { "13", "Silver" },
    { "M8", "Maroon" },
    { "211", "Navy" },
    { "127", "Brown" },
    { "Q1", "Fuchsia" },
    { "35", "LightBlue" },
    { "21", "Khaki" },
    { "32", "Olive" },
    { "30", "Teal" },
    { "24", "Beige" },
    { "11", "LightGray" },
    { "175", "LightYellow" },
    { "10", "LightPink" },
    { "5", "Magenta" }
};
    }

}
