using System;

namespace TagGameLib
{
    public enum MoveDirection { UP, Down, Left, Right,
        None
    }
    public class ModelGame
    {
        Random rnd = new Random();
        int[,] map = new int[4, 4]; //Создаём игровое поле
        int step; //счётчик ходов

        public Action<int[,]> RePaint { get; set; }

        public int Step
        {
            get => step;
            protected set => step = value;
        }
        public int this[int row, int col] => map[row, col];

        public ModelGame()
        {
            Init();
            Mix();
            Step = 0;
            RePaint?.Invoke(map);
        }

        /// <summary>
        /// Заполняем
        /// </summary>
        void Init()
        {

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = (i * 4 + j + 1) % 16;
            RePaint?.Invoke(map);
        }

        /// <summary>
        /// Перемешиваем
        /// </summary>
        void Mix()
        {
            for (int i = 0; i < 200; i++)
            {
                switch (rnd.Next(2) + i % 2 * 2)
                {
                    case 0: ToLeft(); break;
                    case 1: ToRight(); break;
                    case 2: ToUp(); break;
                    case 3: ToDown(); break;
                }
            }
            RePaint?.Invoke(map);
        }
        /// <summary>
        /// Выйгрыш
        /// </summary>
        /// <returns></returns>
        public bool Win()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] != (i * 4 + j + 1) % 16)
                        return false;
            return true;
        }

        #region Движение

        /// <summary>
        /// Ищем где 0
        /// </summary>
        /// <returns>возращаем координаты</returns>
        /// <exception cref="ArgumentException"></exception>
        (int, int) FindSpace()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] == 0)
                        return (i, j);
            throw new ArgumentException("Нечто странное!!!");

        }

        /// <summary>
        /// Меняем местами
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        static void Swap(ref int a, ref int b) => (a, b) = (b, a);

        /// <summary>
        /// Сдвиг вниз
        /// </summary>
        void ToDown()
        {
            var (r, c) = FindSpace();
            if (r < 3)
            {
                Swap(ref map[r, c], ref map[r + 1, c]);
                step++;
            }
        }

        /// <summary>
        /// Сдвиг вверх
        /// </summary>
        void ToUp()
        {
            var (r, c) = FindSpace();
            if (r > 0)
            {
                Swap(ref map[r - 1, c], ref map[r, c]);
                step++;
            }
        }

        /// <summary>
        /// Сдвиг вправо
        /// </summary>
        void ToRight()
        {
            var (r, c) = FindSpace();
            if (c < 3)
            {
                Swap(ref map[r, c], ref map[r, c + 1]);
                step++;
            }
        }

        /// <summary>
        /// Сдвиг влево
        /// </summary>
        void ToLeft()
        {
            var (r, c) = FindSpace();
            if (c > 0)
            {
                Swap(ref map[r, c], ref map[r, c - 1]);
                step++;
            }
        }

        public void KeyDown(MoveDirection key)
        {
            switch (key)
            {
                case MoveDirection.Left: ToLeft(); break;
                case MoveDirection.Right: ToRight(); break;
                case MoveDirection.UP: ToUp(); break;
                case MoveDirection.Down: ToDown(); break;
            }
            RePaint?.Invoke(map);
        }
        #endregion
    }
}
