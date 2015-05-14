using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TracingProgram
{
    /// <summary>
    /// Хранит очередь проводников от источника к приемнику(-ам)
    /// </summary>
    class QueueTrace
    {
        public Point source { private set; get; }
        public Queue<Point> distanation;
        /// <summary>
        /// Хранит очередь проводников от источника к приемнику(-ам)
        /// </summary>
        /// <param name="src">координата источника</param>
        public QueueTrace(Point src)
        {
            this.source = src;
            this.distanation = new Queue<Point>();
        }

        public QueueTrace(QueueTrace q)
        {
            this.source = new Point(q.source.X, q.source.Y);
            this.distanation = new Queue<Point>();
            Point[] p = new Point[q.distanation.Count];
            q.distanation.CopyTo(p, 0);
            foreach(Point point in p) {
                this.distanation.Enqueue(point);
            }
        }
        /// <summary>
        /// Добавление в очередь координаты приемника 
        /// </summary>
        /// <param name="dst">координата источника источника</param>
        public void Add(Point dst) {
            distanation.Enqueue(dst);
        }
        /// <summary>
        /// Если есть в очереди координаты приемников, то возвращает ее, иначе координату (-1;-1)
        /// </summary>
        public Point Distantion 
        {
            get
            {
                if (distanation.Count > 0 && distanation != null)
                    return distanation.Dequeue();
                else
                    return new Point(-1,-1);
            }
        }
    }
}
