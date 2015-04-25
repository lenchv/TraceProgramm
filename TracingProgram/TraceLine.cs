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
        
        public TraceLine(Cell[,] field, int x, int y, int sizeCell)
        {
            this.x = x;
            this.y = y;
            this.sizeCell = sizeCell;
            this.path = getPath(field);
        }

        Point[] getPath(Cell[,] field)
        {
            Point current = getStart(field);
            Point[] path = new Point[0];
            Direction direction;
            int i = 1;

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
                    i++;
                    path[i] = current;
                    direction = getDirection(path[i], path[i - 1]);
                }
            }
            return path;
        }

        Point getNextHorizontal(Cell[,] field, Point cur)
        {
            if (field[cur.Y, cur.X].number - 1 == field[cur.Y, cur.X - 1].number)
            {
                return new Point(cur.X - 1, cur.Y);
            }
            else if (field[cur.Y, cur.X].number - 1 == field[cur.Y, cur.X + 1].number)
            {
                return new Point(cur.X + 1, cur.Y);
            }
            else if (field[cur.Y, cur.X].number - 1 == field[cur.Y - 1, cur.X].number)
            {
                return new Point(cur.X, cur.Y - 1);
            }
            else
            {
                return new Point(cur.X, cur.Y + 1);
            }
        }

        Point getNextVertical(Cell[,] field, Point cur)
        {
            
            if (field[cur.Y, cur.X].number - 1 == field[cur.Y - 1, cur.X].number)
            {
                return new Point(cur.X, cur.Y - 1);
            }
            else if (field[cur.Y, cur.X].number - 1 == field[cur.Y + 1, cur.X].number)
            {
                return new Point(cur.X, cur.Y + 1);
            } 
            else if (field[cur.Y, cur.X].number - 1 == field[cur.Y, cur.X - 1].number)
            {
                return new Point(cur.X - 1, cur.Y);
            }
            else
            {
                return new Point(cur.X + 1, cur.Y);
            }
        }

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

        Point getStart(Cell[,] field)
        {
            if (field[this.y, this.x - 1].number > 0)
            {
                return new Point(this.x-1, this.y);
            }
            if (field[this.y, this.x + 1].number > 0)
            {
                return new Point(this.x + 1, this.y);
            }
            if (field[this.y - 1, this.x].number > 0)
            {
                return new Point(this.x, this.y - 1);
            }
            if (field[this.y+1, this.x].number > 0)
            {
                return new Point(this.x, this.y + 1);
            }
            return new Point(-1, -1);
        }

        public void draw(Graphics g)
        {
            Element el;
            Direction prev;
            Direction next;
           /* Point start = new Point(this.x, this.y);
            Direction prev = getDirection(this.path[0], start);
            Direction next = getDirection(this.path[1], this.path[0]);
            if (prev == next && prev == Direction.HORIZONTAL)
            {
                el = new Horizontal(path[0].X, path[0].Y, sizeCell);
                el.draw(g);
            }
            else if (prev == next && prev == Direction.VERTICAL)
            {
                el = new Vertical(path[0].X, path[0].Y, sizeCell);
                el.draw(g);
            }
            else
            {
                if ((start.X > path[1].X && start.Y > path[1].Y && prev == Direction.HORIZONTAL) ||
                    (start.X < path[1].X && start.Y < path[1].Y && prev == Direction.VERTICAL)
                    )
                {
                    el = new TopToRight(path[0].X, path[0].Y, sizeCell);
                    el.draw(g);
                }
                else if ((start.X > path[1].X && start.Y < path[1].Y && prev == Direction.HORIZONTAL) ||
                        (start.X < path[1].X && start.Y > path[1].Y && prev == Direction.VERTICAL)) 
                {
                    el = new BottomToRight(path[0].X, path[0].Y, sizeCell);
                    el.draw(g);
                }
                else if ((start.X < path[1].X && start.Y > path[1].Y && prev == Direction.HORIZONTAL) ||
                        (start.X > path[1].X && start.Y < path[1].Y && prev == Direction.VERTICAL))
                {
                    el = new TopToLeft(path[0].X, path[0].Y, sizeCell);
                    el.draw(g);
                }
                else if ((start.X < path[1].X && start.Y < path[1].Y && prev == Direction.HORIZONTAL) ||
                       (start.X > path[1].X && start.Y > path[1].Y && prev == Direction.VERTICAL))
                {
                    el = new BottomToLeft(path[0].X, path[0].Y, sizeCell);
                    el.draw(g);
                }
            }*/

            
            for (int i = 1; i < path.Length-1; i++)
            {
                prev = getDirection(this.path[i], this.path[i-1]);
                next = getDirection(this.path[i+1], this.path[i]);
                if (prev == next && prev == Direction.HORIZONTAL)
                {
                    el = new Horizontal(path[i].X, path[i].Y, sizeCell);
                    el.draw(g);
                }
                else if (prev == next && prev == Direction.VERTICAL)
                {
                    el = new Vertical(path[i].X, path[i].Y, sizeCell);
                    el.draw(g);
                }
                else
                {
                    if ((path[i - 1].X > path[i + 1].X && path[i - 1].Y > path[i + 1].Y && prev == Direction.HORIZONTAL) ||
                        (path[i - 1].X < path[i + 1].X && path[i - 1].Y < path[i + 1].Y && prev == Direction.VERTICAL)
                        )
                    {
                        el = new TopToRight(path[i].X, path[i].Y, sizeCell);
                        el.draw(g);
                    }
                    else if ((path[i - 1].X > path[i + 1].X && path[i - 1].Y < path[i + 1].Y && prev == Direction.HORIZONTAL) ||
                            (path[i - 1].X < path[i + 1].X && path[i - 1].Y > path[i + 1].Y && prev == Direction.VERTICAL))
                    {
                        el = new BottomToRight(path[i].X, path[i].Y, sizeCell);
                        el.draw(g);
                    }
                    else if ((path[i - 1].X < path[i + 1].X && path[i - 1].Y > path[i + 1].Y && prev == Direction.HORIZONTAL) ||
                            (path[i - 1].X > path[i + 1].X && path[i - 1].Y < path[i + 1].Y && prev == Direction.VERTICAL))
                    {
                        el = new TopToLeft(path[i].X, path[i].Y, sizeCell);
                        el.draw(g);
                    }
                    else if ((path[i - 1].X < path[i + 1].X && path[i - 1].Y < path[i + 1].Y && prev == Direction.HORIZONTAL) ||
                           (path[i - 1].X > path[i + 1].X && path[i - 1].Y > path[i + 1].Y && prev == Direction.VERTICAL))
                    {
                        el = new BottomToLeft(path[i].X, path[i].Y, sizeCell);
                        el.draw(g);
                    }
                }
            }
        }
    }
}
