using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TracingProgram
{
    class Line:Element
    {
        protected Pen pen = Pens.Black;
        protected Brush brush = Brushes.Black;
        public int x { protected set; get; }
        public int y { protected set; get; }
        public int sizeCell { protected set; get; }

        public Line(int x, int y, int sizeCell)
        {
            this.x = x;
            this.y = y;
            this.sizeCell = sizeCell;
        }

        public int topX
        {
            get 
            {
                return this.x * this.sizeCell;
            }
        }

        public int topY
        {
            get
            {
                return this.y * this.sizeCell;
            }
        }

        public int bottomX
        {
            get
            {
                return topX+this.sizeCell;
            }
        }

        public int bottomY
        {
            get
            {
                return topY + this.sizeCell;
            }
        }

        public virtual void draw(Graphics g) {}

        public string Name
        {
            get
            {
                return "line";
            }
        }

    }
}
