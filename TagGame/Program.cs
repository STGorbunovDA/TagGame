using System;
using System.Timers;
using TagGameLib;

namespace TagGame
{
    internal class Program
    {
        static ModelGame model;
        static Timer timer;
        static bool flag;
        static DateTime start = DateTime.Now;
        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (flag)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"{(DateTime.Now - start).TotalSeconds:F1} sec...");
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += TimerElapsed;
            timer.Start();
            model = new ModelGame();
            model.RePaint += Print;
            model.KeyDown(ConsoleKey.LeftArrow);

            do model.KeyDown(Console.ReadKey(true).Key);
            while (!model.Win());

            timer.Stop();
            Console.WriteLine("Ты победил!");
            Console.ReadLine();
        }
        /// <summary>
        /// Печатаем
        /// </summary>
        static void Print(int[,] map)
        {
            flag = false;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Ходов: {model.Step}");
            Console.WriteLine();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0) Console.Write("  *");
                    else Console.Write($"{map[i, j],3}");
                }
                Console.WriteLine();
            }
            flag = true;
        }
    }
}
