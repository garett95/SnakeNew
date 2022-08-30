using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp13
{
    public class Figure : Block
    {
       

        public Figure() {

        }
        public Figure(int x, int y) : base (col: x, row: y)
        {
          
        }
        //круг
        public void DrawCircle(Graphics graphics) 
        {
            int x = Col * blocSize;
            int y = Row * blocSize;
            graphics.FillEllipse(Brushes.Green, x,y, blocSize, blocSize);
        }
        //квадрат 
        public void DrawSquare(Graphics graphics)
        {
            int x = Col * blocSize;
            int y = Row * blocSize;
            graphics.FillRectangle(Brushes.Blue, x, y, blocSize, blocSize);
        }
    }
}
