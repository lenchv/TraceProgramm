using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TracingProgram
{
    class Contact:Element
    {
        public int x {get; private set;}
        public int y {get; private set;}
        public bool isChecked {get; set;}
        public int number { get; protected set; }
        public string Name { get; protected set; }
        int sizeCell;
        public Contact() { }
        public Contact(int x, int y, int sizeCell, int number)
        {
            this.x = x;
            this.y = y;
            this.sizeCell = sizeCell;
            this.number = number;
        }

        public Contact(int x, int y, int sizeCell, int number, string name):
            this(x, y, sizeCell, number)
        {
            this.Name = name;
        }

        public void draw(Graphics g) {
            int topX = this.x * this.sizeCell;
            int topY = this.y * this.sizeCell;
            g.DrawLine( Pens.Black,
                        topX,
                        topY,
                        topX + this.sizeCell,
                        topY + this.sizeCell
                        );
            g.DrawLine(Pens.Black,
                        topX,
                        topY + this.sizeCell,
                        topX + this.sizeCell,
                        topY
                        );

            if (this.isChecked)
            {
                this.on(g);
            }
        }

        public Point bottomCoord
        {
            get
            {
                return new Point(this.x*this.sizeCell + this.sizeCell,
                                 this.y*this.sizeCell + this.sizeCell);
            }
        }

        public Point top
        {
            get
            {
                return new Point( this.x*this.sizeCell,
                                  this.y*this.sizeCell);
            }
        }

        public Point active(Graphics g)
        {
            if (this.isChecked)
            {
                this.isChecked = false;
                this.off(g);
            }
            else
            {
                this.isChecked = true;
                this.on(g);
            }
            return new Point(this.x, this.y);
        }

        private void off(Graphics g)
        {
            g.FillEllipse(Brushes.White, this.top.X + 2, this.top.Y + 2, this.sizeCell - 4, this.sizeCell - 4);
            draw(g);
        }

        private void on(Graphics g)
        {
            g.FillEllipse(Brushes.Red, this.top.X + 2, this.top.Y + 2, this.sizeCell - 4, this.sizeCell - 4);            
        }
    }
}
