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
        FileXML file;
        List<Element> chips = new List<Element>();  //список всех выводимых элементов
        int sizeCell = 16;                          //размер ячейки
        int widthGrid = 50;                         //ширина поля в ячейках
        int heightGrid = 50;                        //высота поля в ячейках

        Cell[,] demonstrationField;                               
        Point[] demoPoints;
        QueueTrace demoWire;
        QueueTrace demoBackUpWire;
        Point demoEnd;
        bool demoStart = false;
        List<TraceLine> demoComplexLines = new List<TraceLine>();
        List<Layer> layers = new List<Layer>();
        Layer demoLayer;
        int demoLayerPointer;
        bool demonstrationMode;
        bool workMode;
        bool workModeAuto;
        bool workModeStep;
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
            /**клик по дискретному полю*/
            pbMainGrid.MouseClick += pbMainGrid_MouseClick;

            tsbDrawWire.Click += tsbDrawWire_Click;

            demonstrationItem.Click += modeItem_CheckedChanged;
            workItem.Click += modeItem_CheckedChanged;
            workStepItem.Click += modeItem_CheckedChanged;
            workAutoItem.Click += modeItem_CheckedChanged;
        }

        void modeItem_CheckedChanged(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).CheckState = CheckState.Checked;
            if ((String)(sender as ToolStripMenuItem).Tag == "0")
            {
                demonstrationMode = true;
                workMode = false;
                workItem.CheckState = CheckState.Unchecked;
            }
            else if ((String)(sender as ToolStripMenuItem).Tag == "1")
            {
                workMode = true;
                demonstrationMode = false;
                demonstrationItem.CheckState = CheckState.Unchecked;
            }
            else if ((String)(sender as ToolStripMenuItem).Tag == "2")
            {
                workModeStep = true;
                workModeAuto = false;
                workAutoItem.CheckState = CheckState.Unchecked;
            }
            else
            {
                workModeAuto = true;
                workModeStep = false;
                workStepItem.CheckState = CheckState.Unchecked;
            }
            initFromFile(file.name);
        }

        void tsbDrawWire_Click(object sender, EventArgs e)
        {
            List<TraceLine> wires;
            QueueTrace wire;
            Layer layer;
            if (workMode)
            {
                if (workModeAuto)
                {
                    for (int i = 0; i < layers.Count; i++)
                    {
                        layer = new Layer(heightGrid, widthGrid, layers[i].number + 1);
                        wire = layers[i].queue;
                        while (wire != null)
                        {
                            wires = methodConnectionComplexes(layers[i].Field, new QueueTrace(wire), layers[i].color);
                            if (wires == null)
                            {
                                layer.queue = new QueueTrace(wire);
                            }
                            else
                            {
                                foreach (TraceLine tl in wires)
                                {
                                    layers[i].wire = tl;
                                }
                                layers[i].fillField(chips);
                                pbMainGrid.Invalidate();
                            }
                            wire = layers[i].queue;
                        }
                        if (!layer.isEmpty)
                        {
                            layer.fillField(chips);
                            addCheckBoxLayer(layer);
                            layers.Add(layer);
                        }
                    }
                }
            }
            if (demonstrationMode)
            {
                if (!demoStart)
                {
                    layers[demoLayerPointer].fillField(chips);
                    demoWire = layers[demoLayerPointer].queue;
                    if (demoWire != null)
                    {
                        demoBackUpWire = new QueueTrace(demoWire);
                        demoPoints = new Point[1];
                        demoPoints[0] = demoWire.source;
                        layers[demoLayerPointer].Field[demoPoints[0].Y, demoPoints[0].X].number = 0;
                        demoEnd = demoWire.Distantion;
                        demoStart = true;
                    }
                }
                if (demoWire != null)
                {
                    demonstrationField = layers[demoLayerPointer].Field;
                    demoPoints = demoMethodConnectionComplexes(ref demonstrationField, demoPoints, demoEnd);
                
                    if (demoPoints.Length > 0 && demoPoints[0].Equals(demoEnd))
                    {
                        TraceLine tl;
                        if (demoComplexLines.Count == 0)
                        {
                            tl = new TraceLine(layers[demoLayerPointer].Field, demoEnd.X, demoEnd.Y, sizeCell, layers[demoLayerPointer].color);
                        }
                        else
                        {
                            demoPoints = new Point[layers[demoLayerPointer].Field.Length];
                            int i = 0;
                            foreach (TraceLine el in demoComplexLines) 
                            {
                                Array.Copy(el.path, 0, demoPoints, i, el.path.Length);
                                i = el.path.Length;
                            }
                            Array.Resize(ref demoPoints, i);
                            tl = new TraceLineToLine(layers[demoLayerPointer].Field, demoEnd.X, demoEnd.Y, sizeCell, demoPoints, layers[demoLayerPointer].color);
                        }
                        tl.draw(pbMainGrid.CreateGraphics());
                        layers[demoLayerPointer].wire = tl;
                        demoComplexLines.Add(tl);
                        demoEnd = demoWire.Distantion;
                        if (demoEnd.X == -1)
                        {
                            demoStart = false;
                            demoComplexLines.Clear();
                            chips.RemoveAll(removeNumber);
                            pbMainGrid.Invalidate();
                        }
                        else
                        {
                            updateDemoField();
                        }
                    }
                    else if (demoPoints.Length == 0)
                    {
                        demoLayer.queue = new QueueTrace(demoBackUpWire);
                        demoStart = false;
                        demoComplexLines.Clear();
                        chips.RemoveAll(removeNumber);
                        pbMainGrid.Invalidate();
                    }
                }
                else if (!demoLayer.isEmpty)
                {
                    demoLayerPointer++;
                    demoLayer.fillField(chips);
                    addCheckBoxLayer(demoLayer);
                    layers.Add(demoLayer);
                    demoLayer = new Layer(heightGrid, widthGrid, layers[demoLayerPointer].number + 1);
                }
            }
        }

        void updateDemoField()
        {
            //очищение всех номеров в поле, кроме занятых
            for (int i = 0; i < layers[demoLayerPointer].Field.GetLength(0); i++)
            {
                for (int j = 0; j < layers[demoLayerPointer].Field.GetLength(1); j++)
                {
                    if (layers[demoLayerPointer].Field[i, j].number != -1)
                    {
                        layers[demoLayerPointer].Field[i, j].number = 0;
                        layers[demoLayerPointer].Field[i, j].free = true;
                    }
                }
            }
            demoPoints = new Point[layers[demoLayerPointer].Field.Length];
            int index = 0;            
            if (demoStart)
            {
                if (demoComplexLines.Count > 0)
                {
                    foreach (TraceLine el in demoComplexLines)
                    {
                        Array.Copy(el.path, 1, demoPoints, index, el.path.Length - 1);
                        index = el.path.Length-1;
                    }
                }
                else
                {
                    demoPoints[index++] = demoWire.source;
                }
            }
            else
            {
                if (demoWire != null)
                {
                    demoPoints[index++] = demoWire.source;
                }
                else
                {
                    demoPoints[index++] = new Point(0, 0);
                }
            }
            
            Array.Resize(ref demoPoints, index);
            //пометить точки на поле всех проведенных проводников в комплексе занятыми
            foreach (Point p in demoPoints)
            {
                layers[demoLayerPointer].Field[p.Y, p.X].free = false;
            }
            chips.RemoveAll(removeNumber);
            pbMainGrid.Invalidate();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                demonstrationMode = demonstrationItem.Checked;
                workMode = workItem.Checked;
                workModeAuto = workAutoItem.Checked;
                workModeStep = workStepItem.Checked;
                initFromFile("demonstration.xml");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            foreach (CheckBox cb in groupBoxLayers.Controls)
            {
                if (cb.Checked)
                {
                    layers[(int)cb.Tag].draw(e.Graphics);
                }
            }
        }
        /*Очистка сетки*/
        void tsbClearField_Click(object sender, EventArgs e)
        {
            Graphics g = pbMainGrid.CreateGraphics();
            g.Clear(Color.Gray);
            pbMainGrid.BackgroundImage = imageList1.Images[0];
            this.sizeCell = imageList1.Images[0].Height;
            chips.RemoveAll(removeNumber);
            foreach(Layer l in layers) 
            {
                l.fillField(chips);
            }

            updateDemoField();
        }

        private void addCheckBoxLayer(Layer l)
        {
            CheckBox cb = new CheckBox();
            cb.Text = l.ToString();
            cb.ForeColor = l.color;
            cb.Location = new Point(5, l.number*20);
            cb.Tag = l.number-1;
            cb.Checked = true;
            cb.CheckedChanged += cb_CheckedChanged;
            groupBoxLayers.Controls.Add(cb);
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            pbMainGrid.Invalidate();
        }

        private List<TraceLine> methodConnectionComplexes(Cell[,] field, QueueTrace queueTrace, Color color) 
        {
            Point dst = queueTrace.Distantion;  //точка приемника
            int x = queueTrace.source.X;        //абсцисса источника
            int y = queueTrace.source.Y;        //ордината источника
            field[y, x].number = 0;             //номер источника равен 0, чтобы начинать отсчет с него
            int maxPoint = field.GetLength(0) * field.GetLength(1); //максимальное количество точек на поле
            Point[] coord = new Point[maxPoint];    //массив точек, от котрых ведется отсчет в текущий момент
            Point[] newCoord = new Point[maxPoint]; //массив формируемых точек, от которых будет вестись отсчет в следующий момент
            Point[] lineCoord = new Point[maxPoint]; //массив стартовых точек от линий
            int countLine = 1; //количество точек линий
            lineCoord[0] = queueTrace.source; //заносится стартовая точка, на случай если придется пересекать другие проводники
            coord[0] = new Point(x, y);             //инициализация источника
            int count = 1;                          //количество точек от которых ведется отсчет в текущий момент
            int newCount = 0;                       //кол-во точек, от которых будет вестись отсчет в следующий момент

            TraceLine tl = null; //объект проводника
            List<TraceLine> wires = new List<TraceLine>();
            Point[] allLinePoints = new Point[maxPoint];    //все точки проводников включая 1ю и последнюю
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
                           /* Number n = new Number(x + 1, y, sizeCell, field[y, x + 1].number);
                            n.draw(pbMainGrid.CreateGraphics());
                            chips.Add(n);*/
                        }
                        //если слева свободно
                        if (x > 0 && field[y, x - 1].free)
                        {
                            newCoord[newCount] = new Point(x - 1,y);
                            field[y, x - 1].free = false;
                            field[y, x - 1].number = field[y, x].number+1;
                            newCount++;
                            /*Number n = new Number(x - 1, y, sizeCell, field[y, x - 1].number);
                            n.draw(pbMainGrid.CreateGraphics());
                            chips.Add(n);*/
                        }
                        //если снизу свободно
                        if (y < field.GetLength(0)-1 && field[y+1, x].free)
                        {
                            newCoord[newCount] = new Point(x, y + 1);
                            field[y + 1, x].free = false;
                            field[y + 1, x].number = field[y, x].number+1;
                            newCount++;
                            /*Number n = new Number(x, y + 1, sizeCell, field[y + 1, x].number);
                            n.draw(pbMainGrid.CreateGraphics());
                            chips.Add(n);*/
                        }
                        //если сверху свободно
                        if (y >0 && field[y - 1, x].free)
                        {
                            newCoord[newCount] = new Point(x, y - 1);
                            field[ y - 1, x].free = false;
                            field[y - 1,x].number = field[y, x].number+1;
                            newCount++;
                            /*Number n = new Number(x, y - 1, sizeCell, field[y - 1, x].number);
                            n.draw(pbMainGrid.CreateGraphics());
                            chips.Add(n);*/
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
                    //если это первый проводник в комплексе, то провести линию от контакта к контакту
                    if (tl == null && countAllLine == 0)
                    {
                        tl = new TraceLine(field, dst.X, dst.Y, sizeCell, color);
                    }
                    //а иначе провести линию от контакта к проводнику
                    else
                    {
                        //массив всех точек дополнить точками предыдущего проводника
                        Array.Copy(tl.path, 0, allLinePoints, countAllLine, tl.path.Length);
                        //и изменить его длину
                        countAllLine += tl.path.Length;
                        tl = new TraceLineToLine(field, dst.X, dst.Y, sizeCell, allLinePoints, color);
                    }
                    //нарисовать проводник
                    //tl.draw(pbMainGrid.CreateGraphics());
                    //и добавить его в список элементов
                    wires.Add(tl);
                    //очищение всех номеров в поле, кроме занятых
                    for (int i = 0; i < field.GetLength(0); i++)
                    {
                        for (j = 0; j < field.GetLength(1); j++)
                        {
                            if (field[i, j].number != -1)
                            {
                                field[i, j].number = 0;
                                field[i, j].free = true;
                            }
                        }
                    }
                    //Дополнить массив всех точек точками проведенных проводников в комплексе, кроме 1й  
                    //и последней точек
                    Array.Copy(tl.path, 1, lineCoord, countLine, tl.path.Length - 1);
                    countLine += tl.path.Length - 2;

                    //Скопировать в массив текущих точек точки всех проведенных проводников в комплексе
                    Array.Copy(lineCoord, coord, countLine);
                    count = countLine;
                    //пометить точки на поле всех проведенных проводников в комплексе занятыми
                    foreach (Point p in lineCoord)
                    {
                        field[p.Y, p.X].free = false;
                    }
                }
                else
                {
                    return null;
                }
                
                //взять следующий комплекс из очереди
                dst = queueTrace.Distantion;
            }
            return wires;
        }

        Point[] demoMethodConnectionComplexes(ref Cell[,] field, Point[] start, Point end)
        {
            int maxPoint = field.GetLength(0) * field.GetLength(1); //максимальное количество точек на поле            
            Point[] newCoord = new Point[maxPoint]; //массив формируемых точек, от которых будет вестись отсчет в следующий момент
            int newCount = 0;                       //кол-во точек, от которых будет вестись отсчет в следующий момент
            foreach (Point p in start)
            {
                //текущие координаты
                int x = p.X;
                int y = p.Y;
                //если следующая ячейка приемник, выход
                if (x + 1 == end.X && y == end.Y ||
                    x - 1 == end.X && y == end.Y ||
                    x == end.X && y + 1 == end.Y ||
                    x == end.X && y - 1 == end.Y)
                {
                    newCount = 1;
                    newCoord[0] = new Point(end.X, end.Y);
                    break;
                }
                //если справа свободно
                if (x < field.GetLength(1) - 1 && field[y, x + 1].free)
                {
                    newCoord[newCount] = new Point(x + 1, y);  //следующая точка отсчета
                    field[y, x + 1].free = false;           //ячейка занята
                    field[y, x + 1].number = field[y, x].number + 1;  //запись номера ячейки на 1 больше чем предыдущая
                    newCount++;                             //увеличения счетчика кол-ва следующих точек
                    //вывод на экран номера ячейки
                     Number n = new Number(x + 1, y, sizeCell, field[y, x + 1].number);
                     n.draw(pbMainGrid.CreateGraphics());
                     chips.Add(n);
                }
                //если слева свободно
                if (x > 0 && field[y, x - 1].free)
                {
                    newCoord[newCount] = new Point(x - 1, y);
                    field[y, x - 1].free = false;
                    field[y, x - 1].number = field[y, x].number + 1;
                    newCount++;
                    Number n = new Number(x - 1, y, sizeCell, field[y, x - 1].number);
                    n.draw(pbMainGrid.CreateGraphics());
                    chips.Add(n);
                }
                //если снизу свободно
                if (y < field.GetLength(0) - 1 && field[y + 1, x].free)
                {
                    newCoord[newCount] = new Point(x, y + 1);
                    field[y + 1, x].free = false;
                    field[y + 1, x].number = field[y, x].number + 1;
                    newCount++;
                    Number n = new Number(x, y + 1, sizeCell, field[y + 1, x].number);
                    n.draw(pbMainGrid.CreateGraphics());
                    chips.Add(n);
                }
                //если сверху свободно
                if (y > 0 && field[y - 1, x].free)
                {
                    newCoord[newCount] = new Point(x, y - 1);
                    field[y - 1, x].free = false;
                    field[y - 1, x].number = field[y, x].number + 1;
                    newCount++;
                    Number n = new Number(x, y - 1, sizeCell, field[y - 1, x].number);
                    n.draw(pbMainGrid.CreateGraphics());
                    chips.Add(n);
                }
            }
            Array.Resize(ref newCoord, newCount);
            return newCoord;
        }

        void fillField(List<Element> elements)
        {
            for (int i = 0; i < demonstrationField.GetLength(0); i++)
                for (int j = 0; j < demonstrationField.GetLength(1); j++)
                {
                    demonstrationField[i, j] = new Cell();
                }
            foreach (Element el in elements)
            {
                if (el is Chip)
                {
                    for (int i = 0; i < (el as Chip).height; i++)
                    {
                        for (int j = 0; j < (el as Chip).width; j++)
                        {
                            demonstrationField[i + el.y, j + el.x].free = false;
                            demonstrationField[i + el.y, j + el.x].number = -1;
                        }
                    }
                }
                else if (el is Contact)
                {
                    demonstrationField[el.y, el.x].free = false;
                    demonstrationField[el.y, el.x].number = -1;
                }
                else if (el is TraceLine)
                {
                    foreach (Point p in (el as TraceLine).path)
                    {
                        demonstrationField[p.Y, p.X].free = false;
                        demonstrationField[p.Y, p.X].number = -1;
                    }
                }
            }
        }

        //Предикат для удаления номеров со списка элементов
        private static bool removeNumber(Element el)
        {
            return el is Number;
        }

        private void FileMenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FileMenuOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                initFromFile(openFileDialog1.FileName);
            }
        }

        void initFromFile(string name)
        {
            try
            {
                file = new FileXML(name, sizeCell, pbMainGrid.CreateGraphics());
                this.widthGrid = file.widthGrid;
                this.heightGrid = file.heightGrid;
                layers.Clear();
                layers.Add(file.layer);
                file.CopyChips(out chips);
                groupBoxLayers.Controls.Clear();
                addCheckBoxLayer(layers[0]);
                pbMainGrid.Width = widthGrid * sizeCell;
                pbMainGrid.Height = heightGrid * sizeCell;
                demonstrationField = new Cell[heightGrid, widthGrid];
                fillField(chips);
                tsbClearField_Click(new Object(), new EventArgs());
                demoStart = false;
                demoLayer = new Layer(heightGrid, widthGrid, layers[demoLayerPointer].number + 1);
                demoLayerPointer = 0;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
