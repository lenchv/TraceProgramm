using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TracingProgram
{
    class Dip24:Chip
    {
        public override int width { protected set;  get; }
        public override int height { protected set; get; } 
        public override int outPins { protected set; get; }
        public string name { private set; get; }
        private string label;
        private Pen pen = Pens.Black;
        public Dip24(int x, int y, int sizeCell, string label):base(x,y, sizeCell)
        {
            this.name = label;
            base.Name = label;
            this.label = "DIP24_" + label;
            this.width = 23;
            this.height = 3;
            this.outPins = 24;
            base.pins = new Contact[this.outPins];
        }

        public override void draw(Graphics g)
        {
            int topX = base.x * base.sizeCell;
            int topY = base.y * base.sizeCell;

            base.draw(g);

            g.DrawString(this.label,
                        new Font(FontFamily.GenericSerif, 12),
                        Brushes.Black,
                        (float)(topX + this.width / 2),
                        (float)(topY + sizeCell)
                        );
        }
    }
}
