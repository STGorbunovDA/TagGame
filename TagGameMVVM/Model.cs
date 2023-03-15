using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TagGameMVVM
{
    public enum MoveDirection
    {
        UP, Down, Left, Right,
        None
    }

    public sealed class Model : ViewModel
    {
        Random rnd = new Random();
        int step = 0;
        string timer;

        (int r, int c) space;

        ObservableCollection<Fishka> fishki = new ObservableCollection<Fishka>();
        public ObservableCollection<Fishka> Fishki => fishki;

        public Model()
        {
            Init();

        }

        public int Step
        {
            get => step;
            private set => Set(ref step, value);
        }

        public event Action WinCompite;

        /// <summary>
        /// Заполняем
        /// </summary>
        internal void Init()
        {
            //fishki = new Fishka[15];
            for (int k = 0; k < 15; k++)
            {
                var i = k / 4;
                var j = k % 4;
                fishki.Add(new Fishka(i, j, k + 1));
            }
            space = (3, 3);
            //Mix();
        }

        Fishka MoveFrom(int r, int c)
        {
            var f = FindFishka(r, c);
            if (f != null)
            {
                space = (r, c);
            }
            return f;
        }
        void ToUp() => MoveFrom(space.r + 1, space.c)?.ToUp();
        void ToDown() => MoveFrom(space.r - 1, space.c)?.ToDown();
        void ToLeft() => MoveFrom(space.r, space.c + 1)?.ToLeft();
        void ToRight() => MoveFrom(space.r, space.c - 1)?.ToRight();
        
        

        Fishka FindFishka(int r, int c) => fishki.Where(item => item.IsHere(r, c)).FirstOrDefault();

        public void KeyDown(MoveDirection key)
        {
            switch (key)
            {
                case MoveDirection.Left: ToLeft(); break;
                case MoveDirection.Right: ToRight(); break;
                case MoveDirection.UP: ToUp(); break;
                case MoveDirection.Down: ToDown(); break;
            }
        }
        void Mix()
        {
            for (int i = 0; i < 5; i++)
            {
                switch (rnd.Next(2) + i % 2 * 2)
                {
                    case 0: ToLeft(); break;
                    case 1: ToRight(); break;
                    case 2: ToUp(); break;
                    case 3: ToDown(); break;
                }
            }
        }
        public bool Win()
        {
            var map = new int[4, 4];
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] != (i * 4 + j + 1) % 16)
                        return false;
            return true;
        }

        internal void PressBy(Fishka fishka)
        {
            if (fishka.IsHere(space.r + 1, space.c)) ToUp();
            if (fishka.IsHere(space.r - 1, space.c)) ToDown();
            if (fishka.IsHere(space.r, space.c + 1)) ToLeft();
            if (fishka.IsHere(space.r, space.c - 1)) ToRight();
        }
    }
    public class Fishka : ViewModel
    {
        int r, c;
        public int X => c * 110;
        public int Y => r * 110;
        public int Num { get; private set; }
        public Fishka(int r, int c, int num)
        {
            this.r = r;
            this.c = c;
            this.Num = num;
        }
        public void ToDown()
        {
            if (r < 3)
            {
                r++;
                Fire(nameof(Y));
            }
        }
        public void ToUp()
        {
            if (r > 0)
            {
                r--;
                Fire(nameof(Y));
            }
        }
        public void ToRight()
        {
            if (c < 3)
            {
                c++;
                Fire(nameof(X));
            }
        }
        public void ToLeft()
        {
            if (c > 0)
            {
                c--;
                Fire(nameof(X));
            }
        }

        internal bool IsHere(int r, int c) => r == this.r && c == this.c;
    }
}
