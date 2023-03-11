using System;
using System.Drawing;
using System.Windows.Forms;
using TagGameLib;

namespace TagGameWinForms
{
    public partial class Form1 : Form
    {
        ModelGame model;
        MoveDirection dir = MoveDirection.None;
        Button[,] map = new Button[4, 4];
        public Form1()
        {
            InitializeComponent();
            model = new ModelGame();
            model.RePaint += ModelRePaint;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var btn = new Button
                    {
                        Text = "",
                        Size = new Size(80, 80),
                        Location = new Point(j * 80, i * 80)

                    };
                    this.Controls.Add(btn);
                    map[i, j] = btn;
                }
            }
            this.Paint += Form1_Paint;
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var x = model[i, j];
                    map[i, j].Text = model[i, j].ToString();
                    //if (model[i, j].ToString() == "0")
                    //    map[i, j].Text = "*";
                    //map[i, j].Visible = model[i, j] == 0;
                    map[i, j].Visible = model[i, j] != 0;
                    if(model.Win())
                    {
                        MessageBox.Show("Ты победил!");
                        Environment.Exit(0);
                    }    

                }
            }
        }

        private void ModelRePaint(int[,] map)
        {
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: dir = MoveDirection.Left; break;
                case Keys.Right: dir = MoveDirection.Right; break;
                case Keys.Up: dir = MoveDirection.UP; break;
                case Keys.Down: dir = MoveDirection.Down; break;
            }
            if (dir != MoveDirection.None)
                model.KeyDown(dir);
        }

        void BtnUp_Click(object sender, EventArgs e)
        {
            model.KeyDown(MoveDirection.UP);
        }

        void BtnDown_Click(object sender, EventArgs e)
        {
            model.KeyDown(MoveDirection.Down);
        }

        void BtnLeft_Click(object sender, EventArgs e)
        {
            model.KeyDown(MoveDirection.Left);
        }

        void BtnRight_Click(object sender, EventArgs e)
        {
            model.KeyDown(MoveDirection.Right);
        }
    }
}
