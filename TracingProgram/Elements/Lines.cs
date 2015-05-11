using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TracingProgram
{
    class Vertical:Line
    {
        public Vertical(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2)-1;
            }
        }
        
        public override void draw(Graphics g)
        {
            g.DrawLine( base.pen,
                        this.topX,
                        base.topY,
                        this.topX,
                        base.bottomY);
        }
    }

    class Horizontal : Line
    {
        public Horizontal(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine( base.pen,
                        base.topX,
                        this.topY,
                        base.bottomX,
                        this.topY);
        }
    }

    class BottomToRight : Line
    {
        public BottomToRight(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        this.topX,
                        base.bottomY);
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        base.bottomX,
                        this.topY);

        }
    }

    class BottomToLeft : Line
    {
        public BottomToLeft(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { } 
        
        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        this.topX,
                        base.bottomY);
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        base.topX,
                        this.topY);

        }
    }

    class TopToRight : Line
    {
        public TopToRight(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        this.topX,
                        base.topY);
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        base.bottomX,
                        this.topY);

        }
    }

    class TopToLeft : Line
    {
        public TopToLeft(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        this.topX,
                        base.topY);
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        base.topX,
                        this.topY);

        }
    }

    class BottomToHorizontal : Line
    {
        public BottomToHorizontal(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }
        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        base.topX,
                        this.topY,
                        base.bottomX,
                        this.topY);
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        this.topX,
                        base.bottomY);
            g.FillEllipse(base.brush,
                          this.topX - base.sizeCell / 8,
                          this.topY - base.sizeCell / 8,
                          base.sizeCell/4,
                          base.sizeCell/4);
        }

    }

    class TopToHorizontal : Line
    {
        public TopToHorizontal(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        base.topX,
                        this.topY,
                        base.bottomX,
                        this.topY);
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        this.topX,
                        base.topY);
            g.FillEllipse(base.brush,
                          this.topX - base.sizeCell / 8,
                          this.topY - base.sizeCell / 8,
                          base.sizeCell / 4,
                          base.sizeCell / 4);
        }
    }

    class RightToVertical : Line
    {
        public RightToVertical(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        this.topX,
                        base.topY,
                        this.topX,
                        base.bottomY);
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        base.bottomX,
                        this.topY);
            g.FillEllipse(base.brush,
                          this.topX - base.sizeCell / 8,
                          this.topY - base.sizeCell / 8,
                          base.sizeCell / 4,
                          base.sizeCell / 4);
        }

    }

    class LeftToVertical : Line
    {
        public LeftToVertical(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        this.topX,
                        base.topY,
                        this.topX,
                        base.bottomY);
            g.DrawLine(base.pen,
                        this.topX,
                        this.topY,
                        base.topX,
                        this.topY);
            g.FillEllipse(base.brush,
                          this.topX - base.sizeCell / 8,
                          this.topY - base.sizeCell / 8,
                          base.sizeCell / 4,
                          base.sizeCell / 4);
        }
    }

    class Cross : Line
    {
        public Cross(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        this.topX,
                        base.topY,
                        this.topX,
                        base.bottomY);
             g.DrawLine(base.pen,
                        base.topX,
                        this.topY,
                        base.bottomX,
                        this.topY);
        }
    }

    class CrossWithConnect : Line
    {
        public CrossWithConnect(int x, int y, int sizeCell, Color col) : base(x, y, sizeCell, col) { }

        public new int topX
        {
            get
            {
                return (base.topX + base.sizeCell / 2) - 1;
            }
        }

        public new int topY
        {
            get
            {
                return (base.topY + base.sizeCell / 2) - 1;
            }
        }

        public override void draw(Graphics g)
        {
            g.DrawLine(base.pen,
                        this.topX,
                        base.topY,
                        this.topX,
                        base.bottomY);
            g.DrawLine(base.pen,
                       base.topX,
                       this.topY,
                       base.bottomX,
                       this.topY);
            g.FillEllipse(base.brush,
                          this.topX - base.sizeCell / 8,
                          this.topY - base.sizeCell / 8,
                          base.sizeCell / 4,
                          base.sizeCell / 4);
        }
    }
}
