using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace TracingProgram
{
    interface Element
    {
        int x { get; }
        int y { get; }
        string Name { get; }
        void draw(Graphics g);
    }
}
