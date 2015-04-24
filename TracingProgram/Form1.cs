using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TracingProgram
{
    public partial class Form1 : Form
    {
        List<Element> chips = new List<Element>();  //список всех выводимых элементов
        int sizeCell = 16;                          //размер ячейки
        int widthGrid = 50;                         //ширина поля в ячейках
        int heightGrid = 50;                        //высота поля в ячейках
        Cell[,] grid;                               //значения каждой ячейки
        public Form1()
        {
            this.Load += Form1_Load;
            InitializeComponent();
            /**Очистка сетки*/
            tsbClearGrid.Click += tsbClearField_Click;
            /**Перерисовка компонентов*/
            pbMainGrid.Paint += pbMainGrid_Paint;
            pbMainGrid.MouseClick += pbMainGrid_MouseClick;

        }

        void Form1_Load(object sender, EventArgs e)
        {
            pbMainGrid.Width = widthGrid * sizeCell;
            pbMainGrid.Height = heightGrid * sizeCell;
            tsbClearField_Click(new Object(), new EventArgs());
            grid = new Cell[heightGrid, widthGrid];
            for (int i = 0; i < heightGrid; i++ )
            {
                for (int j = 0; j < widthGrid; j++)
                {
                    grid[i,j] = new Cell();
                }
            }
        }
        /**Клик по дискретному полю*/
        void pbMainGrid_MouseClick(object sender, MouseEventArgs e)
        {
            /*Определение, нажата ли кнопка на элементе контакта*/
            foreach (Element el in chips)
            {
                if (el is Contact)
                {
                    Contact cont = (el as Contact);
                    if (cont.top.X < e.X &&
                        cont.top.Y < e.Y &&
                        cont.bottomCoord.X > e.X &&
                        cont.bottomCoord.Y > e.Y)
                    {
                        cont.active((sender as PictureBox).CreateGraphics());
                    }
                }
                else
                {
                    Chip ch = (el as Chip);
                    if (ch.topX < e.X &&
                        ch.topY < e.Y &&
                        ch.bottomCoord.X > e.X &&
                        ch.bottomCoord.Y > e.Y)
                    {
                        ch.checkContact((sender as PictureBox).CreateGraphics(), e.X, e.Y);
                    }
                    
                }
            }
        }

        void pbMainGrid_Paint(object sender, PaintEventArgs e)
        {
            foreach (Element c in chips)
            {
                c.draw(e.Graphics);
            }
        }
        /*Очистка сетки*/
        void tsbClearField_Click(object sender, EventArgs e)
        {
            Graphics g = pbMainGrid.CreateGraphics();
            g.Clear(Color.Gray);
            pbMainGrid.BackgroundImage = imageList1.Images[0];
            this.sizeCell = imageList1.Images[0].Height;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Random coord = new Random();
            Number c = new Number(coord.Next(0, 25), coord.Next(0, 25), sizeCell, coord.Next(0, 500));
            c.draw(pbMainGrid.CreateGraphics());
            chips.Add(c);
        }
    }
}
