using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TracingProgram
{
    class Cell
    {
        public bool free { private set; get; }  //указывает свободна ли ячейка
        //если ячейка свободно, то 0, если занята, то значения от -1 до MAX_INT
        public int number { private set; get; } 

        public Cell()
        {
            this.free = true;
            this.number = 0;
        }

        public Cell(bool free, int num)
        {
            this.free = free;
            this.number = num;
        }
    }
}
