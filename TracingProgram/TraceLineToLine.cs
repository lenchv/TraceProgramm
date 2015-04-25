using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Diagnostics;

namespace TracingProgram
{
    class TraceLineToLine:TraceLine
    {
        [Flags]
        enum Direction : byte
        {
            EMPTY = 0x00,
            LEFT = 0x01,
            RIGHT = 0x02,
            UP = 0x04,
            DOWN = 0x08,
            CURLEFT = 0x10,
            CURRIGHT = 0x20,
            CURUP = 0x40,
            CURDOWN = 0x80
        }
        Cell[,] field;
        Direction direct;
        public TraceLineToLine(Cell[,] field, int x, int y, int sizeCell, Point[] dist)
            : base(field, x, y, sizeCell) 
        {
            this.direct = getDirection1(dist);
        }

        Direction getDirection1(Point[] points)
        {
            int centerX = base.path[path.Length - 1].X;
            int centerY = base.path[path.Length - 1].Y;
            int prevX = base.path[path.Length - 2].X;
            int prevY = base.path[path.Length - 2].Y;
            Direction d = Direction.EMPTY;
            
            for (int i = 0; i < points.Length; i++) 
            {
                if (points[i].X == 0 && points[i].Y == 0)
                {
                    break;
                }
                if (points[i].X == centerX-1 && points[i].Y == centerY)
                {
                   d |= Direction.LEFT;
                }
                if (points[i].X == centerX + 1 && points[i].Y == centerY)
                {
                    d |= Direction.RIGHT;
                }
                if (points[i].X == centerX && points[i].Y == centerY + 1)
                {
                    d |= Direction.DOWN;
                }
                if (points[i].X == centerX && points[i].Y == centerY - 1)
                {
                    d |= Direction.UP;
                }
            }
            if (centerX - 1 == prevX)
            {
                d |= Direction.CURLEFT;
            }
            else if (centerX + 1 == prevX)
            {
                d |= Direction.CURRIGHT;
            }
            else if (centerY + 1 == prevY)
            {
                d |= Direction.CURDOWN;
            }
            else
            {
                d |= Direction.CURUP;
            }
            return d;
        }

        Direction getDirection(Cell[,] field)
        {

            int centerX = base.path[path.Length - 1].X;
            int centerY = base.path[path.Length - 1].Y;
            int prevX = base.path[path.Length - 2].X;
            int prevY = base.path[path.Length - 2].Y;
            Direction direct = Direction.EMPTY;
            if (!field[centerX - 1, centerY].free && field[centerX - 1, centerY].number != -1)
            {
                if (prevX == centerX - 1 && centerY == prevY)
                {
                    direct |= Direction.CURLEFT;
                }
                else
                {
                    direct |= Direction.LEFT;
                }
            }
            if (!field[centerX + 1, centerY].free && field[centerX + 1, centerY].number != -1)
            {
                if (prevX == centerX + 1 && centerY == prevY)
                {
                    direct |= Direction.CURRIGHT;
                }
                else
                {
                    direct |= Direction.RIGHT;
                }
            }
            if (!field[centerX, centerY + 1].free && field[centerX, centerY + 1].number != -1)
            {
                if (prevX == centerX && centerY == prevY + 1)
                {
                    direct |= Direction.CURDOWN;
                }
                else
                {
                    direct |= Direction.DOWN;
                }
            }
            if (!field[centerX, centerY - 1].free && field[centerX, centerY - 1].number != -1)
            {
                if (prevX == centerX && centerY == prevY - 1)
                {
                    direct |= Direction.CURUP;
                }
                else
                {
                    direct |= Direction.UP;
                }
            }
            return direct;
        }

        public override void draw(Graphics g)
        {
            base.draw(g);
            int centerX = base.path[path.Length - 1].X;
            int centerY = base.path[path.Length - 1].Y;

            Element el;
            Debug.WriteLine(direct);
            switch (direct)
            {
 
                /*
                case (Direction.CURLEFT | Direction.DOWN | Direction.RIGHT):
                case (Direction.CURDOWN | Direction.LEFT | Direction.RIGHT):
                case (Direction.CURRIGHT | Direction.DOWN | Direction.LEFT):*/
                case (Direction)0x1A:   //CURLEFT & DOWN & RIGHT
                case (Direction)0x83:   //CURDOWN & LEFT & RIGHT
                case (Direction)0x29:   //CURRIGHT & DOWN & LEFT
                    el = new BottomToHorizontal(centerX, centerY, base.sizeCell);
                    el.draw(g);
                    break;
               /* case (Direction.CURLEFT|Direction.UP|Direction.RIGHT):
                case (Direction.CURUP | Direction.LEFT | Direction.RIGHT):
                case (Direction.CURRIGHT | Direction.UP | Direction.LEFT):*/
                case (Direction)0x16:
                case (Direction)0x43:
                case (Direction)0x25:
                    el = new TopToHorizontal(centerX, centerY, base.sizeCell);
                    el.draw(g);
                    break;
               /* case (Direction.CURLEFT | Direction.UP | Direction.DOWN):
                case (Direction.CURUP | Direction.LEFT | Direction.DOWN):
                case (Direction.CURDOWN | Direction.UP | Direction.LEFT):*/
                case (Direction)0x1C:
                case (Direction)0x49:
                case (Direction)0x85:
                    el = new LeftToVertical(centerX, centerY, base.sizeCell);
                    el.draw(g);
                    break;
                /*case (Direction.CURRIGHT | Direction.UP | Direction.DOWN):
                case (Direction.CURUP | Direction.RIGHT | Direction.DOWN):
                case (Direction.CURDOWN | Direction.UP | Direction.RIGHT):*/
                case (Direction)0x2C:
                case (Direction)0x4A:
                case (Direction)0x86:
                    el = new RightToVertical(centerX, centerY, base.sizeCell);
                    el.draw(g);
                    break;

                default:
                    el = new CrossWithConnect(centerX, centerY, base.sizeCell);
                    el.draw(g);
                    break;
            }
        }
    }
}
