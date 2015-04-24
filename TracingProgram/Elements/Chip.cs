using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TracingProgram
{
    class Chip:Element
    {
        Pen pen = Pens.Black;
        public int x { protected set; get; }
        public int y { protected set; get; }
        public virtual int width { protected set; get; }
        public virtual int height { protected set; get; }
        public virtual int outPins { protected set; get; }
        protected int sizeCell { private set; get; }
        public string Name { get; protected set; }
        public Contact[] pins;
        public Chip(int x, int y, int sizeCell)
        {
            this.x = x;
            this.y = y;
            this.sizeCell = sizeCell;
            this.width = 5;
            this.height = 3;
            this.outPins = 4;
            this.Name = "Chip";
            this.pins = new Contact[this.outPins];
        }

        public virtual void draw(Graphics g) 
        {
            g.DrawRectangle(pen,
                           topX,
                           topY,
                           this.sizeCell * this.width,
                           this.sizeCell * this.height
                           );
            if (pins[pins.Length-1] != null)
            {
                foreach (Contact c in pins)
                {
                    c.draw(g);
                }
            }
            else
            {
                for (int i = 0; i < this.outPins / 2; i++)
                {
                    Contact c = new Contact(this.x + i * 2,
                                                            this.y,
                                                            this.sizeCell, this.outPins - i);
                    c.draw(g);
                    pins[this.outPins-i-1] = c;
                    c = new Contact(this.x + i * 2,
                                            this.y + (this.height - 1),
                                            this.sizeCell, i+1);
                    c.draw(g);
                    pins[i] = c;
                }
            }
            g.FillPie(Brushes.Black, topX - sizeCell / 2, topY + sizeCell, sizeCell, sizeCell, 270, 180);
        }

        public int topX { 
            get 
            {
                return this.x * this.sizeCell;
            }
        }

        public int topY {
          get 
          {
              return this.y * this.sizeCell;
          }
        }
        public Point bottomCoord
        {
            get {
                return new Point(this.x*sizeCell + this.width * this.sizeCell,
                                 this.y*sizeCell + this.height * this.sizeCell);
            }
        }

        public Point checkContact(Graphics g, int x, int y)
        {
            foreach (Contact cont in pins)
            {
                if (cont.top.X < x &&
                    cont.top.Y < y &&
                    cont.bottomCoord.X > x &&
                    cont.bottomCoord.Y > y)
                {
                    return cont.active(g);
                }
            }
            return new Point(-1, -1);
        }

        public Point getContactPoint(int number)
        {
            return new Point(this.pins[number - 1].x, this.pins[number-1].y);
        }
    }
}
