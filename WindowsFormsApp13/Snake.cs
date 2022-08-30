using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    public class Snake 
    {

        Direction direction; //текущее направление движения 
        Direction nextDirection = Direction.Right; // изменение направления движения

        //Создаем лист и записыаем сегменты змейки
        List<Figure> snake = new List<Figure>()
        {
            new Figure(7,5),  // голова 
            new Figure(6,5),  // тело
            new Figure(5,5)   // хвост
        };
        public   bool isGameOver = false;
    

        //Рисуем квадрат для каждого сегмента тела змейки
        public void DrawSnake(Graphics graphics)
        {
            foreach (Figure s in snake)
            {
                s.DrawSquare(graphics);
            }
        }
        Figure head = new Figure();
        Figure newHead = new Figure();
     
        public void Move(Fruits fruit)
        {
            head = snake[0];
            direction = nextDirection;
            if(direction == Direction.Right)
            {
                newHead = new Figure(head.Col+1, head.Row);
            }
            else if(direction == Direction.Left)
            {
                newHead = new Figure(head.Col-1, head.Row);
            }
            else if (direction == Direction.Up)
            {
                newHead = new Figure(head.Col, head.Row-1);
            }
            else if (direction == Direction.Down)
            {
                newHead = new Figure(head.Col, head.Row+1);
            }
            if (CheckCollision())  //Если 
            {
                isGameOver = true;
                GameOver();
              
            }
            snake.Insert(0, newHead);
            if (newHead.Equal(fruit))
            {
                Game.snakeSpeed -= 200;
                Game.score++;
                //if(Game.snakeSpeed >=20)
                   
                fruit.Move();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }
        public void SetDirection(Direction newDirection)
        {
           // проверка
            if (direction == Direction.Up && newDirection == Direction.Down)
            {
                return;

            }
            else if (direction == Direction.Right && newDirection == Direction.Left)
            {
                return;
            }
            else if (direction == Direction.Down && newDirection == Direction.Up)
            {
                return;
            }
            else if (direction == Direction.Left && newDirection == Direction.Right)
            {
                return;
            }

            nextDirection = newDirection;

        }
        //Проверяем, не столкнулась ли змейка со стенкой или с собой 
        public bool CheckCollision()
        {

            var leftCollision = (newHead.Col < 0);
            var topColliosion = (newHead.Row < 3);
            var rightCollision = (newHead.Col == Game.widthInBlocks);
            var bottomCollision = (newHead.Row > Game.heightInBlocks);

            //var wallCollision = leftCollision || topColliosion || rightCollision || bottomCollision;
            var selfCollision = false;
            //съели ли фрукт
            // проверка столкновений
            if (rightCollision)
            {
                newHead.Col = 0;
            }
            if (leftCollision)
            {
                newHead.Col = Game.widthInBlocks;
            }
            if (topColliosion)
            {
                newHead.Row = Game.heightInBlocks;
            }
            if (bottomCollision)
            {
                newHead.Row = 3;
            }
            foreach (var s in snake )
            {
                if (newHead.Equal(s))
                {
                    selfCollision = true;
                }
            }
            return selfCollision;
        }
        public void Restart()
        {
            Game.score = 0;
            nextDirection = Direction.Right;
            snake.Clear();
            snake.Add(new Figure(7,5));
            snake.Add(new Figure(6, 5));
            snake.Add(new Figure(5, 5));
        }
        public bool IsGameOver()
        {
            return isGameOver;
        }
        void GameOver()
        {
            MessageBox.Show("Вы проиграли:(");  
            return;
        }
         void Win()
        {
            if (snake.Count == Game.widthInBlocks * (Game.heightInBlocks -2))
            {
                MessageBox.Show("You win");
            }
        }
    }
}





