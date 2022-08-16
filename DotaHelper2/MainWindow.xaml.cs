using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
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
using System.IO;
using Search;

namespace DotaHelper2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<string> _dataCBList = new BindingList<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var heroes = File.ReadAllLines("heroes.txt");
            if(settings.Default.isDarkTheme)
            {
                settings.Default.isDarkTheme = false;
                Button_Click_1(button2, e);
            }
            foreach (var hero in heroes)
            {
                _dataCBList.Add(hero);
            }
            comboB1.ItemsSource = _dataCBList;
        }

        private bool isThreadWorks = false;
        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!isThreadWorks && comboB1.SelectedItem?.ToString() != null && comboB1.SelectedItem.ToString() != string.Empty)
            {
                var text = comboB1.SelectedItem?.ToString()?.ToLower().Replace(' ', '-') ?? "null";
                string t1 = string.Empty;
                string t2 = string.Empty;
                string t3 = string.Empty;
                button1.Visibility = Visibility.Hidden;
                string[] result = await Task.Run(() =>
                {
                    isThreadWorks = true;
                    t1 = SDotabuff.getData(text, 0);
                    t2 = SDotabuff.getData(text, 1);
                    t3 = SDotabuff.getData(text, 2);
                    isThreadWorks = false;
                    return Task.FromResult(new string[] { t1, t2, t3 });
                });
                build1.Content = result[0];
                build2.Content = result[1];
                build3.Content = result[2];
                button1.Visibility = Visibility.Visible;
                build1.Visibility = Visibility.Visible;
                build2.Visibility = Visibility.Visible;
                build3.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            if (settings.Default.isDarkTheme)
            {
                grid.Background = Brushes.White;
                title.Foreground = Brushes.Black;
                build1.Foreground = Brushes.Black;
                build2.Foreground = Brushes.Black;
                build3.Foreground = Brushes.Black;
                button1.Background = bc.ConvertFrom("#FFDDDDDD") as Brush;
                button2.Background = bc.ConvertFrom("#FFDDDDDD") as Brush;
            }
            else
            {
                grid.Background = Brushes.Black;
                title.Foreground = Brushes.White;
                build1.Foreground = Brushes.White;
                build2.Foreground = Brushes.White;
                build3.Foreground = Brushes.White;
                button1.Background = bc.ConvertFrom("#FF929292") as Brush;
                button2.Background = bc.ConvertFrom("#FF929292") as Brush;
            }
            settings.Default.isDarkTheme = !settings.Default.isDarkTheme;
            settings.Default.Save();
        }
    }
}
