using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TracingProgram
{
    class Layer
    {
        List<Element> chips = new List<Element>();
        Queue<QueueTrace> queueWire = new Queue<QueueTrace>();
        Layer(Element el)
        {
            chips.Add(el);
        }

        public void draw(Graphics g)
        {
            foreach (Element el in this.chips)
            {
                el.draw(g);
            }
        }

        public QueueTrace queue
        {
            get
            {
                return queueWire.Dequeue();
            }
            set
            {
                queueWire.Enqueue(value);
            }
        }
    }
}
