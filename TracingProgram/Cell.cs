using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TracingProgram
{
    class Cell
    {
        public bool line { set; get; }
        public int crossOver {set; get; } //количество пересечений проводников
        public bool free { set; get; }  //указывает свободна ли ячейка
        //если ячейка свободно, то 0, если занята, то значения от -1 до MAX_INT
        //-1 - если занято, но не текущим проводником
        //-2 - если занято проводником
        public int number { set; get; } 

        public Cell()
        {
            this.free = true;
            this.number = 0;
            this.line = false;
            this.crossOver = 0;
        }

        public Cell(bool free, int num)
        {
            this.free = free;
            this.number = num;
            this.line = false;
            this.crossOver = 0;
        }

        public void cross(Cell c)
        {
            this.crossOver = c.crossOver + 1;
        }
    }
}
