using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TracingProgram
{
    class Layer
    {
        List<TraceLine> wires = new List<TraceLine>();
        Queue<QueueTrace> queueWire = new Queue<QueueTrace>();
        Cell[,] field;
        public int number { private set;  get; }
        public Color color { private set; get; }
        public Layer(QueueTrace q, int height, int width, int num)
            : this(height, width, num)
        {
            queueWire.Enqueue(q);
        }
        public Layer(TraceLine el, int height, int width, int num)
            : this(height, width, num)
        {
            wires.Add(el);
        }
        public Layer(int height, int width, int num)
        {
            field = new Cell[height, width];
            Random rand = new Random();
            color = Color.FromArgb(rand.Next(rand.Next(255)), rand.Next(200), rand.Next(180));
            number = num;
        }

        public void draw(Graphics g)
        {
            foreach (TraceLine el in this.wires)
            {
                el.draw(g);
            }
        }

        public TraceLine wire
        {
            set
            {
                wires.Add(value);
            }
        }

        public QueueTrace queue
        {
            get
            {
                try
                {
                    queueWire.Peek();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
                return queueWire.Dequeue();
            }
            set
            {
                queueWire.Enqueue(value);
            }
        }

        public bool isEmpty
        {
            get
            {
                try
                {
                    queueWire.Peek();
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
                return false;
            }
        }

        public void fillField(List<Element> elements)
        {
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = new Cell();
                }
            foreach (Element el in elements)
            {
                if (el is Chip)
                {
                    for (int i = 0; i < (el as Chip).height; i++)
                    {
                        for (int j = 0; j < (el as Chip).width; j++)
                        {
                            field[i + el.y, j + el.x].free = false;
                            field[i + el.y, j + el.x].number = -1;
                        }
                    }
                }
                else if (el is Contact)
                {
                    field[el.y, el.x].free = false;
                    field[el.y, el.x].number = -1;
                }
            }
            foreach (TraceLine tl in wires)
            {
                Point[] p = tl.path;
                for (int i = 1; i < p.Length - 1; i++)
                {
                    field[p[i].Y, p[i].X].free = false;
                    field[p[i].Y, p[i].X].line = true;
                    field[p[i].Y, p[i].X].number = -1;
                }
            }
        }

        public Cell[,] Field
        {
            get
            {
                return field;
            }
        }

        public override string ToString()
        {
            return "Слой " + this.number;
        }
    }
}
