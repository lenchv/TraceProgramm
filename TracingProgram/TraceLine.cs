using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Diagnostics;

namespace TracingProgram
{
    class TraceLine:Element
    {
        /// <summary>
        /// Определяет тип проводника
        /// </summary>
        enum Direction
        {
            HORIZONTAL,
            VERTICAL
        }

        public string Name { private set; get; }
        public int x { private set; get; }
        public int y { private set; get; }
        public int sizeCell { private set; get; }
        public Point[] path { private set; get; }
        public Color color { private set; get; }
        private int pathPointer;
        /// <summary>
        /// Проводит линию на дискретном поле с числовой волной
        /// </summary>
        /// <param name="field">поле в котором провдена числовая волна</param>
        /// <param name="x">абсцисса стартовой точки</param>
        /// <param name="y">ордината стартовой точки</param>
        /// <param name="sizeCell">размер ячейки</param>
        /// <exception cref="IndexOutOfRangeException"/>
        public TraceLine(Cell[,] field, int x, int y, int sizeCell, Color color)
        {
            this.x = x;
            this.y = y;
            this.sizeCell = sizeCell;
            this.color = color;
            getPath(field);
        }
        /// <summary>
        /// Определяет из числовой волны на поле эффективный путь
        /// </summary>
        /// <param name="field">поле с числовой волной</param>
        void getPath(Cell[,] field)
        {
            Point current = getStart(field);
            this.path = new Point[0];
            Direction direction;
            pathPointer = 1;

            if (current.X > 0)
            {
                path = new Point[field[current.Y, current.X].number+2];
                path[0] = new Point(this.x, this.y);
                path[1] = new Point(current.X, current.Y);
                direction = getDirection(current, new Point(this.x, this.y));
                
                while (field[current.Y, current.X].number > 0) 
                {
                    if (direction == Direction.HORIZONTAL)
                    {
                        current = getNextHorizontal(field, current);
                    }
                    else
                    {
                        current = getNextVertical(field, current);
                    }
                    if (current.X == -1)
                    {
                        throw new IndexOutOfRangeException("Конечная точка трассы не найдена!");
                    }
                    pathPointer++;
                    path[pathPointer] = current;    
                    direction = getDirection(path[pathPointer], path[pathPointer - 1]);
                }
            }
        }
        /// <summary>
        /// Определяет следующую точку, делая приоритет на ось абсцисс
        /// </summary>
        /// <param name="field">дискретное поле с числовой волной</param>
        /// <param name="cur">текущая точка</param>
        /// <returns>Возвращает следующую точку</returns>
        Point getNextHorizontal(Cell[,] field, Point cur)
        {
            if (cur.X > 0 &&
                field[cur.Y, cur.X].number - 1 == field[cur.Y, cur.X - 1].number)
            {
                return new Point(cur.X - 1, cur.Y);
            }
            if (cur.X < field.GetLength(1) - 1 &&
                field[cur.Y, cur.X].number - 1 == field[cur.Y, cur.X + 1].number)
            {
                return new Point(cur.X + 1, cur.Y);
            }
            if (cur.Y > 0 &&
                field[cur.Y, cur.X].number - 1 == field[cur.Y - 1, cur.X].number)
            {
                return new Point(cur.X, cur.Y - 1);
            }
            if ( cur.Y < field.GetLength(0) - 1 &&
                field[cur.Y, cur.X].number - 1 == field[cur.Y + 1, cur.X].number)
            {
                return new Point(cur.X, cur.Y + 1);
            }
            return new Point(-1, -1);
        }
        /// <summary>
        /// Определяет следующую точку, делая приоритет на ось ординат
        /// </summary>
        /// <param name="field">дискретное поле с числовой волной</param>
        /// <param name="cur">текущая точка</param>
        /// <returns>Возвращает следующую точку</returns>
        Point getNextVertical(Cell[,] field, Point cur)
        {
            if (cur.Y > 0 &&
                field[cur.Y, cur.X].number - 1 == field[cur.Y - 1, cur.X].number)
            {
                return new Point(cur.X, cur.Y - 1);
            }
            if (cur.Y < field.GetLength(0) - 1 &&
                field[cur.Y, cur.X].number - 1 == field[cur.Y + 1, cur.X].number)
            {
                return new Point(cur.X, cur.Y + 1);
            }
            if (cur.X > 0 &&
                field[cur.Y, cur.X].number - 1 == field[cur.Y, cur.X - 1].number)
            {
                return new Point(cur.X - 1, cur.Y);
            }
            if (cur.X < field.GetLength(1) - 1 &&
                field[cur.Y, cur.X].number - 1 == field[cur.Y, cur.X + 1].number)
            {
                return new Point(cur.X + 1, cur.Y);
            }
            return new Point(-1, -1);
        }
        /// <summary>
        /// Определяет направление проводника от предыдущей точки к текущей
        /// </summary>
        /// <param name="cur">текущая точка</param>
        /// <param name="prev">предыдущая точка</param>
        /// <returns>Направление от предыдущей точки к текущей</returns>
        Direction getDirection(Point cur, Point prev)
        {
            if (cur.X == prev.X)
            {
                return Direction.VERTICAL;
            }
            else
            {
                return Direction.HORIZONTAL;
            }
        }
        /// <summary>
        /// Определяет начальную точку отсчета
        /// </summary>
        /// <param name="field">дискртеное поле с числовой волной</param>
        /// <returns>Начальная точка отсчета</returns>
        Point getStart(Cell[,] field)
        {
            if (x > 0 && field[this.y, this.x - 1].number >= 0 && !field[this.y, this.x - 1].free)
            {
                return new Point(this.x-1, this.y);
            }
            if (x < field.GetLength(1) - 1 && field[this.y, this.x + 1].number >= 0 && !field[this.y, this.x + 1].free)
            {
                return new Point(this.x + 1, this.y);
            }
            if (y > 0 && field[this.y - 1, this.x].number >= 0 && !field[this.y - 1, this.x].free)
            {
                return new Point(this.x, this.y - 1);
            }
            if (y < field.GetLength(0) - 1 && field[this.y + 1, this.x].number >= 0 && !field[this.y+1, this.x].free)
            {
                return new Point(this.x, this.y + 1);
            }
            return new Point(-1, -1);
        }
        /// <summary>
        /// Рисует на элементе Graphics линию, по ранее определенному пути
        /// </summary>
        /// <param name="g">Поле для рисования</param>
        public virtual void draw(Graphics g)
        {
            Element el;
            Direction prev;
            Direction next;

            for (int i = 1; i < path.Length-1; i++)
            {
                prev = getDirection(this.path[i], this.path[i-1]);
                next = getDirection(this.path[i+1], this.path[i]);
                if (prev == next && prev == Direction.HORIZONTAL)
                {
                    el = new Horizontal(path[i].X, path[i].Y, sizeCell, this.color);
                    el.draw(g);
                }
                else if (prev == next && prev == Direction.VERTICAL)
                {
                    el = new Vertical(path[i].X, path[i].Y, sizeCell, this.color);
                    el.draw(g);
                }
                else
                {
                    if ((path[i - 1].X > path[i + 1].X && path[i - 1].Y > path[i + 1].Y && prev == Direction.HORIZONTAL) ||
                        (path[i - 1].X < path[i + 1].X && path[i - 1].Y < path[i + 1].Y && prev == Direction.VERTICAL))
                    {
                        el = new TopToRight(path[i].X, path[i].Y, sizeCell, this.color);
                        el.draw(g);
                    }
                    else if ((path[i - 1].X > path[i + 1].X && path[i - 1].Y < path[i + 1].Y && prev == Direction.HORIZONTAL) ||
                            (path[i - 1].X < path[i + 1].X && path[i - 1].Y > path[i + 1].Y && prev == Direction.VERTICAL))
                    {
                        el = new BottomToRight(path[i].X, path[i].Y, sizeCell, this.color);
                        el.draw(g);
                    }
                    else if ((path[i - 1].X < path[i + 1].X && path[i - 1].Y > path[i + 1].Y && prev == Direction.HORIZONTAL) ||
                            (path[i - 1].X > path[i + 1].X && path[i - 1].Y < path[i + 1].Y && prev == Direction.VERTICAL))
                    {
                        el = new TopToLeft(path[i].X, path[i].Y, sizeCell, this.color);
                        el.draw(g);
                    }
                    else if ((path[i - 1].X < path[i + 1].X && path[i - 1].Y < path[i + 1].Y && prev == Direction.HORIZONTAL) ||
                           (path[i - 1].X > path[i + 1].X && path[i - 1].Y > path[i + 1].Y && prev == Direction.VERTICAL))
                    {
                        el = new BottomToLeft(path[i].X, path[i].Y, sizeCell, this.color);
                        el.draw(g);
                    }
                }
            }
        }
        /// <summary>
        /// Проверяет наличие точки в проводнике
        /// </summary>
        /// <param name="p">Искомая точка</param>
        /// <returns>True если точка найдена</returns>
        public bool hasPoint(Point p)
        {
            for (int i = 0; i < pathPointer; i++ )
            {
                if (path[i].X == p.X && path[i].Y == p.Y)
                {
                    return true;
                }
            }
            return false;
            
        }

        void extPath()
        {
            Point[] tmp = new Point[path.Length];
            Array.Copy(path, tmp, path.Length);
            path = new Point[path.Length+1];
            Array.Copy(tmp, path, tmp.Length);
        }
    }
}
