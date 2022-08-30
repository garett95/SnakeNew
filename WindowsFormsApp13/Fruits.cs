using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp13
{
    public class Fruits : Figure
    {
        //public Point coordinates = new Point();
        public Random random = new Random(); //случайные координаты фрукта 
     
        
        public Fruits()
        {

            this.Col = random.Next(0, Game.widthInBlocks);
            this.Row = random.Next(3, Game.heightInBlocks);
            
        }
        public void Move()
        {
            this.Col = random.Next(0, Game.widthInBlocks);
            this.Row = random.Next(3, Game.heightInBlocks);
        }
       
    }
}
