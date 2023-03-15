using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TagGameLib;

namespace TagGameWPF
{
    public partial class MainWindow : Window
    {
        ModelGame model;
        DateTime start;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            model = new ModelGame();
            model.RePaint += ModelRePaint;
            model.WinCompite += ModelWinComplite;
            model.KeyDown(MoveDirection.Left);
            start = DateTime.Now;
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tblTimer.Text = (DateTime.Now - start).ToString(@"mm\:ss");
        }

        private void ModelWinComplite()
        {
            brdWin.Visibility = Visibility.Visible;
            timer.Stop();

            List<Record> rec;
            if (File.Exists("records.txt"))
                rec = File.ReadAllLines("records.txt")
                    .Select(s => new Record(s)).ToList();
            else rec = new List<Record>();

            var r = new Record
            {
                Date = DateTime.Now.ToString(),
                Time = tblTimer.Text,
                Steps = model.Step.ToString()
            };
            rec.Add(r);
            var ordList = rec.OrderBy(s => s.Time)
                .Select((x, i) => { x.Pos = i + 1; return x; })
                .Take(10)
                .ToArray();
            File.WriteAllLines("records.txt", ordList.Select(x => x.ToString()));
            records.ItemsSource = ordList;
        }

        private void ModelRePaint(int[,] e)
        {
            int[][] map = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                map[i] = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    map[i][j] = model[i, j];
                }
            }
            ic.ItemsSource = map;
            tblStep.Text = model.Step.ToString();
        }

        private void BorderMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var brd = sender as Border;
            var num = (int)brd.DataContext;
            model.Press(num);
        }

        private void BtnContinueGame_Click(object sender, RoutedEventArgs e)
        {
            tblStep.Text = "0";
            brdWin.Visibility = Visibility.Collapsed;
            model = new ModelGame();
            start = DateTime.Now;
            timer.Start();
            model.RePaint += ModelRePaint;
            model.WinCompite += ModelWinComplite;
        }

        private void BtnCloseGame_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        class Record
        {
            public Record()
            {

            }
            public Record(string s)
            {
                var field = s.Split('\t');
                Pos = int.Parse(field[0]);
                Date = field[1];
                Time = field[2];
                Steps = field[3];
            }
            public override string ToString()
            {
                return $"{Pos}\t{Date}\t{Time}\t{Steps}";
            }
            public int Pos { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Steps { get; set; }
        }

    }
}
