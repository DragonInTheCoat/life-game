using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace life_game
{
    public partial class Form1 : Form
    {
        int colpix = 20;
        int coly = 15;
        int colx = 15;
        bool[,] piece;
        bool[,] mas;
        long tik = 0;
        public Form1()
        {
            InitializeComponent();
            piece = new bool[coly+2, colx+2];
            mas = piece;
            arr(ref piece);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Invalidate();

            if (button1.Text.Contains("Запуск"))
            {
                button1.Text = "Стоп";
                timer1.Enabled = true;
            }
            else
            {
                button1.Text = "Запуск";
                timer1.Enabled = false;
            }
        }

        void arr(ref bool[,] piece)
        {
            for (int i = 0; i < piece.GetLength(0); i++)
            {
                for (int j = 0; j < piece.GetLength(1); j++)
                {
                    piece[i, j] = false;
                }
            }
        }
        private void game(ref PaintEventArgs e, int colx, int coly)//
        {
            Brush br = Brushes.Aqua;

            for (int i = 1; i < piece.GetLength(0)-1; i++)
            {
                for (int j = 1; j < piece.GetLength(1)-1; j++)
                {
                    if (piece[i, j] == true) e.Graphics.FillEllipse(br, j * colpix + 2, i * colpix + 2,  colpix - 4, colpix - 4);
                }
            }

        }
        private void startgame(ref PaintEventArgs e, int colx, int coly)
        {
            e.Graphics.Clear(Color.White);
            Pen sbg = new Pen(Color.LightGray);
            for (int i = 0; i <= Convert.ToInt32(colx); i++)
            {
                e.Graphics.DrawLine(sbg, i * colpix, 0, i * colpix, pictureBox1.Height);
            }
            for (int i = 0; i <= Convert.ToInt32(coly); i++)
            {
                e.Graphics.DrawLine(sbg, 0, i * colpix, pictureBox1.Width, i * colpix);
            }
            game(ref e, colx, coly);
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (piece == null) return;
            startgame(ref e, colx, coly);
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int j = e.X / colpix;
            int i = e.Y / colpix;
            if (piece[i, j] == false) piece[i, j] = true;
            else piece[i, j] = false;
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colx = Convert.ToInt32(textBox1.Text);
            pictureBox1.Width = colx * colpix;
            coly = Convert.ToInt32(textBox2.Text);
            pictureBox1.Height = coly * colpix;
            piece = new bool[coly, colx];
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        private int LiveOrDie(bool[,] arr, int count, int i, int j)
            {
            for (int i1 = -1; i1 <= 1; i1 = i1 + 1)
            {
                for (int j1 = -1; j1 <= 1; j1 = j1 + 1)
                {
                    
                    if ((i1!=0||j1!=0)&&((i + i1 >= 0) && (j + j1 >= 0))&&((i + i1 < arr.GetLength(0)) && (j + j1 < arr.GetLength(1))))
                    { if (arr[i + i1, j + j1] == true) count++; }
                }
            }
            return count;
            }

        private bool[,] life(bool [,] arr)
        {
            bool[,] arr1 = arr.Clone() as bool[,];
            int count = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i,j]==true)
                    {
                        count = LiveOrDie(arr, count, i, j);
                        if (count < 2 || count > 3) arr1[i, j] = false;
                        
                    } else
                    {
                        count = LiveOrDie(arr, count, i, j);
                        if (count == 3) arr1[i, j] = true;
                    }
                    count = 0;
                }
            }
            return arr1;
        }
        private bool[,] gran(bool[,] arr)
        {
            for(int i = 1; i<arr.GetLength(0)-1;i++)
            {
                arr[0, i] = arr[arr.GetLength(1) - 2,i];
                arr[arr.GetLength(0) - 1, i] = arr[1, i];
            }
            for (int i = 1; i < arr.GetLength(1)-1; i++)
            {
                
            }
            return arr;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(TimeSpan.FromSeconds(tik));
            tik++;

           
            piece = life(piece);
            piece= gran(piece);
           
            pictureBox1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            arr(ref piece);
            pictureBox1.Invalidate();
            timer1.Stop();
            label1.Text = "";
            tik = 0;
            button1.Text = "Запуск";
        }
    }
}
