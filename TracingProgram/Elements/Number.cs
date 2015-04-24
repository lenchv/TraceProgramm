using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace TracingProgram
{
    class Number:Line
    {
        int number;
        public Number(int x, int y, int sizeCell, int n):base(x,y,sizeCell) 
        {
            this.number = n;
        }

        public override void draw(Graphics g)
        {
            int fontSize = 10;
            if (this.number / 100 != 0)
            {
                fontSize = 6;
            }
            
            g.DrawString(
                this.number.ToString(),
                new Font(FontFamily.GenericSerif, fontSize),
                Brushes.Black,
                (float)base.topX,
                (float)base.topY
                );
        }
    }
}
