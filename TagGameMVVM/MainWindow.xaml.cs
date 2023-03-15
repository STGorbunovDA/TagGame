using System;
using System.Windows;
using System.Windows.Controls;

namespace TagGameMVVM
{
    public partial class MainWindow : Window
    {
        Model model = new Model();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            model.Init();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var brd = (sender as Border);
            var fishka = brd.DataContext as Fishka;
            model.PressBy(fishka);
        }
    }

}
