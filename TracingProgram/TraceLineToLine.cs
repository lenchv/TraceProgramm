﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Diagnostics;

namespace TracingProgram
{
    class TraceLineToLine:TraceLine
    {   
        /// <summary>
        /// Задает направление занятой точки
        /// </summary>
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
        Direction direct;
        /// <summary>
        /// Рисует линию от контакта к проводнику на дискретном поле с числовой волной
        /// </summary>
        /// <param name="field">поле с числовой волной</param>
        /// <param name="x">стартовая абсцисса</param>
        /// <param name="y">стартовая ордината</param>
        /// <param name="sizeCell">размер ячейки на поле</param>
        /// <param name="dist">точки ранее проведенных проводников, к которым можно присоедениться</param>
        public TraceLineToLine(Cell[,] field, int x, int y, int sizeCell, Point[] dist, Color col)
            : base(field, x, y, sizeCell, col) 
        {
            this.direct = getDirection(dist);
        }
        /// <summary>
        /// Определяет тип присоединения к контакту
        /// </summary>
        /// <param name="points">точки ранее проведенных проводников, к которым можно присоедениться</param>
        /// <returns>Тип присоединения</returns>
        Direction getDirection(Point[] points)
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

        public override void draw(Graphics g)
        {
            base.draw(g);
            int centerX = base.path[path.Length - 1].X;
            int centerY = base.path[path.Length - 1].Y;

            Element el;
            switch (direct)
            {
 
                /*
                case (Direction.CURLEFT | Direction.DOWN | Direction.RIGHT):
                case (Direction.CURDOWN | Direction.LEFT | Direction.RIGHT):
                case (Direction.CURRIGHT | Direction.DOWN | Direction.LEFT):*/
                case (Direction)0x1A:   //CURLEFT & DOWN & RIGHT
                case (Direction)0x83:   //CURDOWN & LEFT & RIGHT
                case (Direction)0x29:   //CURRIGHT & DOWN & LEFT
                    el = new BottomToHorizontal(centerX, centerY, base.sizeCell, this.color);
                    el.draw(g);
                    break;
               /* case (Direction.CURLEFT|Direction.UP|Direction.RIGHT):
                case (Direction.CURUP | Direction.LEFT | Direction.RIGHT):
                case (Direction.CURRIGHT | Direction.UP | Direction.LEFT):*/
                case (Direction)0x16:
                case (Direction)0x43:
                case (Direction)0x25:
                    el = new TopToHorizontal(centerX, centerY, base.sizeCell, this.color);
                    el.draw(g);
                    break;
               /* case (Direction.CURLEFT | Direction.UP | Direction.DOWN):
                case (Direction.CURUP | Direction.LEFT | Direction.DOWN):
                case (Direction.CURDOWN | Direction.UP | Direction.LEFT):*/
                case (Direction)0x1C:
                case (Direction)0x49:
                case (Direction)0x85:
                    el = new LeftToVertical(centerX, centerY, base.sizeCell, this.color);
                    el.draw(g);
                    break;
                /*case (Direction.CURRIGHT | Direction.UP | Direction.DOWN):
                case (Direction.CURUP | Direction.RIGHT | Direction.DOWN):
                case (Direction.CURDOWN | Direction.UP | Direction.RIGHT):*/
                case (Direction)0x2C:
                case (Direction)0x4A:
                case (Direction)0x86:
                    el = new RightToVertical(centerX, centerY, base.sizeCell, this.color);
                    el.draw(g);
                    break;
                default:
                    el = new CrossWithConnect(centerX, centerY, base.sizeCell, this.color);
                    el.draw(g);
                    break;
            }
        }
    }
}
