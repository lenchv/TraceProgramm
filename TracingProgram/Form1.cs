using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Xml;

using System.Diagnostics;

namespace TracingProgram
{
    public partial class Form1 : Form
    {
        List<Element> chips = new List<Element>();  //список всех выводимых элементов
        int sizeCell = 16;                          //размер ячейки
        int widthGrid = 50;                         //ширина поля в ячейках
        int heightGrid = 50;                        //высота поля в ячейках
        Cell[,] grid;                               //значения каждой ячейки
        /// <summary>
        /// Очередь контактов, для трассировки. Каждый элемент очереди содержит
        /// объект с координатами источника, и очередь с координатами приемников
        /// </summary>
        Queue<QueueTrace> distanation = new Queue<QueueTrace>();  //очередь трассировок
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
            /*QueueTrace q = new QueueTrace(new Point(0, 0));
            q.Add(new Point(50, 40));
            distanation.Enqueue(q);*/

            addElementsFromFile("test.xml");
            pbMainGrid.Width = widthGrid * sizeCell;
            pbMainGrid.Height = heightGrid * sizeCell;
            tsbClearField_Click(new Object(), new EventArgs());
            grid = new Cell[heightGrid, widthGrid];

            fillCells();
            /*for (int i = 0; i < heightGrid; i++)
            {
                for (int j = 0; j < widthGrid; j++)
                {
                    Number n = new Number(j, i, sizeCell, grid[i, j].number);
                    n.draw(pbMainGrid.CreateGraphics());
                    chips.Add(n);
                }
            }*/

        }

        /**Клик по дискретному полю*/
        void pbMainGrid_MouseClick(object sender, MouseEventArgs e)
        {
            /*Определение, нажата ли кнопка на элементе контакта*/
            Point tmp;
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
                       tmp = cont.active((sender as PictureBox).CreateGraphics());
                       //Проверяем это контакт источника или приемника
                      /* if (source.X == -1)
                       {
                           source = new Point(tmp.X, tmp.Y);
                           MessageBox.Show(source.ToString());
                       }
                       else
                       {
                           distanation.Enqueue(new Point(tmp.X, tmp.Y));
                           MessageBox.Show(tmp.ToString());
                       }*/
                       break;
                    }
                }
                else if (el is Chip)
                {
                    Chip ch = (el as Chip);
                    if (ch.topX < e.X &&
                        ch.topY < e.Y &&
                        ch.bottomCoord.X > e.X &&
                        ch.bottomCoord.Y > e.Y)
                    {
                        tmp = ch.checkContact((sender as PictureBox).CreateGraphics(), e.X, e.Y);
                        //Проверяем это контакт источника или приемника
                       /* if (source.X == -1)
                        {
                            source = new Point(tmp.X, tmp.Y);
                        }
                        else
                        {
                            distanation.Enqueue(new Point(tmp.X, tmp.Y));
                        }*/
                        break;
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

        /*Parse XML*/
        public void addElementsFromFile(string filename)
        {
            try
            {
                FileStream file = new FileStream(filename,FileMode.Open);
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlElement root = doc.DocumentElement;
                this.widthGrid = Int32.Parse(root.Attributes.GetNamedItem("width").Value);
                this.heightGrid = Int32.Parse(root.Attributes.GetNamedItem("height").Value);
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name == "Elements")
                    {
                        foreach (XmlNode el in node.ChildNodes)
                        {
                            int x = Int32.Parse(el.Attributes.GetNamedItem("x").Value);
                            int y = Int32.Parse(el.Attributes.GetNamedItem("y").Value);
                            string name = el.Attributes.GetNamedItem("Name").Value;
                            switch(el.Name.ToLower()) {
                                case "dip14": 
                                    chips.Add(new Dip14(x,y,this.sizeCell,name));
                                    break;
                                case "dip16":
                                    chips.Add(new Dip16(x, y, this.sizeCell, name));
                                    break;
                                case "dip24":
                                    chips.Add(new Dip24(x, y, this.sizeCell, name));
                                    break;
                                case "contact":
                                    chips.Add(new Contact(x, y, this.sizeCell,1, name));
                                    break;
                            }
                            chips.Last().draw(this.pbMainGrid.CreateGraphics());
                        }
                    }
                    else if (node.Name == "Connections")
                    {
                        foreach (XmlNode con in node.ChildNodes)
                        {
                            int i = Int32.Parse(con.Attributes.GetNamedItem("contact").Value);
                            QueueTrace tmp = new QueueTrace(this.getContactOfChip(con.Name, i));
                            foreach (XmlNode dst in con.ChildNodes)
                            {
                                i = Int32.Parse(dst.Attributes.GetNamedItem("contact").Value);
                                tmp.Add(this.getContactOfChip(dst.Name, i));
                            }
                            distanation.Enqueue(tmp);
                        }
                    }
                }
            } 
            catch(ArgumentException ex) {
                MessageBox.Show("Ошибка: Аргумент\nПодробнее: "+ex.Message);
            }
            catch (SecurityException ex)
            {
                MessageBox.Show("Ошибка: Безопасность\nПодробнее: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Ошибка: Не найден файл\nПодробнее: " + ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Ошибка: Ввод\\Вывод\nПодробнее: " + ex.Message);
            }
            catch (XmlException ex)
            {
                MessageBox.Show("Ошибка: XML\nПодробнее: " + ex.Message);
            }
        }

        private Point getContactOfChip(string name, int number)
        {
            foreach (Element chip in chips)
            {
                if (name == chip.Name)
                {
                    if (chip is Chip)
                    {
                        return (chip as Chip).getContactPoint(number);
                    }
                    else if (chip is Contact)
                    {
                        return new Point((chip as Contact).x, (chip as Contact).y);
                    }
                }
            }
            throw new ArgumentException("Элемента с именем " + name + " нет на плате.\nПроверьте конфигурационный файл");
        }

        private void fillCells()
        {

            for (int i = 0; i < heightGrid; i++)
            {
                for (int j = 0; j < widthGrid; j++)
                {
                    grid[i, j] = new Cell();
                }
            }
            foreach (Element chip in chips)
            {
                if (chip is Chip)
                {
                    for (int i = 0; i < (chip as Chip).height; i++)
                    {
                        for (int j = 0; j < (chip as Chip).width; j++)
                        {
                            grid[i+chip.y, j+chip.x].free = false;
                            grid[i + chip.y, j + chip.x].number = -1;
                        }
                    }
                }
                else if (chip is TraceLine)
                {
                    foreach (Point p in (chip as TraceLine).path)
                    {
                        grid[p.Y, p.X].free = false;
                        grid[p.Y, p.X].number = -1;
                    }
                }
                else if (!(chip is Number))
                {
                    grid[chip.y, chip.x].free = false;
                    grid[chip.y, chip.x].number = -1;
                }
            }
        }

        private void methodConnectionComplexes(Cell[,] field, QueueTrace queueTrace) 
        {
            Point dst = queueTrace.Distantion;  //точка приемника
            int x = queueTrace.source.X;        //абсцисса источника
            int y = queueTrace.source.Y;        //ордината источника
            field[y, x].number = 0;             //номер источника равен 0, чтобы начинать отсчет с него
            int maxPoint = field.GetLength(0) * field.GetLength(1); //максимальное количество точек на поле
            Point[] coord = new Point[maxPoint];    //массив точек, от котрых ведется отсчет в текущий момент
            Point[] newCoord = new Point[maxPoint]; //массив формируемых точек, от которых будет вестись отсчет в следующий момент
            Point[] lineCoord = new Point[maxPoint]; //массив стартовых точек от линий
            int countLine = 0; //количество точек линий
            coord[0] = new Point(x, y);             //инициализация источника
            int count = 1;                          //количество точек от которых ведется отсчет в текущий момент
            int newCount = 0;                       //кол-во точек, от которых будет вестись отсчет в следующий момент

            TraceLine tl = null;

            Point[] allLinePoints = new Point[maxPoint];
            int countAllLine = 0;
            while (dst.X != -1)
            {
                int j = 0;  //индекс итераций
                bool flag = true;   //флаг нахождения точки приемника
                //цикл ведется до тех пор пока не найдена точка приемника, или заполнено все поле, а приемник не найден
                while (j<(maxPoint/2) && flag)
                {
                    //цикл по точкам, от которых ведется отсчет в текущий момент
                    for (int i = 0; i < count; i++)
                    {
                        //текущие координаты
                        x = coord[i].X;
                        y = coord[i].Y;
                        //если следующая ячейка приемник, выход из цикла
                        if (x + 1 == dst.X && y == dst.Y ||
                            x - 1 == dst.X && y == dst.Y ||
                            x == dst.X && y + 1 == dst.Y ||
                            x == dst.X && y - 1 == dst.Y)
                        {
                            flag = false;
                            break;
                        }
                        //если справа свободно
                        if (x < field.GetLength(1)-1 && field[y, x + 1].free)
                        {
                            newCoord[newCount] = new Point(x+1,y);  //следующая точка отсчета
                            field[y, x + 1].free = false;           //ячейка занята
                            field[y, x + 1].number = field[y, x].number+1;  //запись номера ячейки на 1 больше чем предыдущая
                            newCount++;                             //увеличения счетчика кол-ва следующих точек
                            //вывод на экран номера ячейки
                            Number n = new Number(x + 1, y, sizeCell, field[y, x + 1].number);
                            n.draw(pbMainGrid.CreateGraphics());
                            chips.Add(n);
                        }
                        //если слева свободно
                        if (x > 0 && field[y, x - 1].free)
                        {
                            newCoord[newCount] = new Point(x - 1,y);
                            field[y, x - 1].free = false;
                            field[y, x - 1].number = field[y, x].number+1;
                            newCount++;
                            Number n = new Number(x - 1, y, sizeCell, field[y, x - 1].number);
                            n.draw(pbMainGrid.CreateGraphics());
                            chips.Add(n);
                        }
                        //если снизу свободно
                        if (y < field.GetLength(0)-1 && field[y+1, x].free)
                        {
                            newCoord[newCount] = new Point(x, y + 1);
                            field[y + 1, x].free = false;
                            field[y + 1, x].number = field[y, x].number+1;
                            newCount++;
                            Number n = new Number(x, y + 1, sizeCell, field[y + 1, x].number);
                            n.draw(pbMainGrid.CreateGraphics());
                            chips.Add(n);
                        }
                        //если сверху свободно
                        if (y >0 && field[y - 1, x].free)
                        {
                            newCoord[newCount] = new Point(x, y - 1);
                            field[ y - 1, x].free = false;
                            field[y - 1,x].number = field[y, x].number+1;
                            newCount++;
                            Number n = new Number(x, y - 1, sizeCell, field[y - 1, x].number);
                            n.draw(pbMainGrid.CreateGraphics());
                            chips.Add(n);
                        }
                    }
                    count = newCount;                       //перезапись кол-ва текущих точек
                    Array.Copy(newCoord, coord, newCount);  //перезапись текущих точек
                    Array.Clear(newCoord, 0, newCount);     //обнуление массива следующих точек
                    newCount = 0;                           //обнуление кол-ва след. точек
                    j++;                                    //увеличение индекса итерации добавления точек
                }
                //если найден приемник, то нарисовать трассу
                if (!flag)
                {
                    if (tl == null)
                    {
                        tl = new TraceLine(field, dst.X, dst.Y, sizeCell);
                    }
                    else
                    {
                        Array.Copy(tl.path, 0, allLinePoints, countAllLine, tl.path.Length);
                        countAllLine += tl.path.Length;
                        tl = new TraceLineToLine(field, dst.X, dst.Y, sizeCell, allLinePoints);
                    }
                    tl.draw(pbMainGrid.CreateGraphics());
                    chips.Add(tl);
                    for(int i = 0; i < field.GetLength(0); i++) {
                        for (j = 0; j < field.GetLength(1); j++ )
                        {
                            if (field[i, j].number != -1)
                            {
                                field[i, j].number = 0;
                                field[i, j].free = true;
                            }
                        }
                    }

                    Array.Copy(tl.path, 1, lineCoord, countLine, tl.path.Length-1);
                    countLine += tl.path.Length - 2;
                    Array.Copy(lineCoord, coord, countLine);
                    count = countLine;

                    foreach (Point p in lineCoord)
                    {
                        field[p.Y, p.X].free = false;
                    }
                }
                //drawTrace(field, dst);
                dst = queueTrace.Distantion;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                methodConnectionComplexes(grid, distanation.Dequeue());
                chips.RemoveAll(removeNumber);
                fillCells();
            }
            catch (InvalidOperationException) { }
        }

        private static bool removeNumber(Element el)
        {
            return el is Number;
        }

        private void drawTrace(Cell[,] field, Point start)
        {
            int x = start.X;
            int y = start.Y;
            Graphics g = pbMainGrid.CreateGraphics();
            Element el;
            if (field[y, x - 1].number > 0)
            {
                if (field[y, x - 1].number - 1 == field[y - 1, x - 1].number)
                {
                    el = new TopToRight(x - 1, y, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                else if (field[y, x - 1].number - 1 == field[y + 1, x - 1].number)
                {
                    el = new BottomToRight(x - 1, y, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                else
                {
                    el = new Horizontal(x - 1, y, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                x = x - 1;
            }
            else if (field[y, x + 1].number > 0)
            {
                if (field[y, x + 1].number - 1 == field[y - 1, x + 1].number)
                {
                    el = new TopToLeft(x + 1, y, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                else if (field[y, x + 1].number - 1 == field[y + 1, x + 1].number)
                {
                    el = new BottomToLeft(x + 1, y, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                else
                {
                    el = new Horizontal(x + 1, y, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                x = x + 1;
            }
            else if (field[y + 1, x].number > 0)
            {
                if (field[y + 1, x].number - 1 == field[y + 1, x - 1].number)
                {
                    el = new TopToLeft(x, y + 1, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                else if (field[y + 1, x].number - 1 == field[y + 1, x + 1].number)
                {
                    el = new TopToRight(x, y + 1, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                else
                {
                    el = new Vertical(x, y + 1, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                y = y + 1;
            }
            else if (field[y - 1, x].number > 0)
            {
                if (field[y - 1, x].number - 1 == field[y - 1, x - 1].number)
                {
                    el = new BottomToLeft(x, y - 1, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                else if (field[y - 1, x].number - 1 == field[y - 1, x + 1].number)
                {
                    el = new BottomToRight(x, y - 1, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                else
                {
                    el = new Vertical(x, y - 1, sizeCell);
                    el.draw(g);
                    chips.Add(el);
                }
                y = y - 1;
            }



            do
            {
                if (field[y, x].number - 1 == field[y, x - 1].number)
                {
                    if (field[y, x - 1].number - 1 == field[y, x - 2].number)
                    {
                        el = new Horizontal(x - 1, y, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    else if (field[y, x - 1].number - 1 == field[y - 1, x - 1].number)
                    {
                        el = new TopToRight(x - 1, y, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    else if (field[y, x - 1].number - 1 == field[y + 1, x - 1].number)
                    {
                        el = new BottomToRight(x - 1, y, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    x = x - 1;
                }
                else if (field[y, x].number - 1 == field[y, x + 1].number)
                {
                    if (field[y, x + 1].number - 1 == field[y, x + 2].number)
                    {
                        el = new Horizontal(x + 1, y, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    if (field[y, x + 1].number - 1 == field[y - 1, x + 1].number)
                    {
                        el = new TopToLeft(x + 1, y, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    else if (field[y, x + 1].number - 1 == field[y + 1, x + 1].number)
                    {
                        el = new BottomToLeft(x + 1, y, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    x = x + 1;
                }
                else if (field[y, x].number - 1 == field[y - 1, x].number)
                {
                    if (field[y - 1, x].number - 1 == field[y - 2, x].number)
                    {
                        el = new Vertical(x, y - 1, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    if (field[y - 1, x].number - 1 == field[y - 1, x - 1].number)
                    {
                        el = new BottomToLeft(x, y - 1, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    else if (field[y - 1, x].number - 1 == field[y - 1, x + 1].number)
                    {
                        el = new BottomToRight(x, y - 1, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    y = y - 1;
                }
                else if (field[y, x].number - 1 == field[y + 1, x].number)
                {
                    if (field[y + 1, x].number - 1 == field[y + 2, x].number)
                    {
                        el = new Vertical(x, y + 1, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    if (field[y + 1, x].number - 1 == field[y + 1, x - 1].number)
                    {
                        el = new TopToLeft(x, y + 1, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    else if (field[y + 1, x].number - 1 == field[y + 1, x + 1].number)
                    {
                        el = new TopToRight(x, y + 1, sizeCell);
                        el.draw(g);
                        chips.Add(el);
                    }
                    y = y + 1;
                }
            } while (field[y, x].number > 1);
        }
    }
}
