using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp13
{
    public class Block
    {
        public int Col { get; set; }  //столбцы
        public int Row { get; set; }  //строки 

        static public int blocSize = 20;  // размер блока  в пикселях 

        public Block()
        {

        }

        public Block(int col, int row)
        {
            Col = col;
            Row = row;
        }
        public  bool Equal(Figure otherFigure)
        {
            return Col == otherFigure.Col && Row == otherFigure.Row;
        }
    }
}
