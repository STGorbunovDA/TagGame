using System;
using System.Windows;
using System.Windows.Documents;
using TagGameLib;

namespace TagGameWPF
{
    public partial class MainWindow : Window
    {
        ModelGame model;
        public MainWindow()
        {
            InitializeComponent();
            model = new ModelGame();
            model.RePaint += ModelRePaint;
            model.Init();
        }

        private void ModelRePaint(int[,] e)
        {
            int[][] map = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                map[i] = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    int x = model[i, j];
                    map[i][j] = model[i, j];
                }
            }
            ic.ItemsSource = map;
        }
    }
}
