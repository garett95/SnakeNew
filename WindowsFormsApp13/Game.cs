using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Data.Entity;
namespace WindowsFormsApp13
{
    public partial class Game : Form
    {


        UserContext db;  //подключение к БД

        Graphics graphics; //объект для рисования 
        Snake snake;
        Fruits apple;
        bool userWasFound;
        //  public Random random = new Random();
        public static int score = 0;  // Количество набранных очков
                                      // static - общая переменная для всех объектов
        public static int record = 0;
        int userId = 0;
        int width = 0;   // ширина рамки  в пикселях 
        int height = 0;  // высота рамки  в пикселях 
        
        public static int widthInBlocks = 0;   // ширина холста в блоках
        public static int heightInBlocks = 0;  // высота холста в блоках
        bool gameStart = false;

        public static int snakeSpeed = 250;

        //Конструктор
        //выполняется один раз при запуске приложения
        public Game()
        {
            InitializeComponent();
            this.KeyPreview = true;
            width = this.ClientSize.Width;    // ширина окна 
            height = this.ClientSize.Height;  // высота окна 
            widthInBlocks = width / Block.blocSize;
            heightInBlocks = height / Block.blocSize;

            db = new UserContext();
            graphics = CreateGraphics();
            apple = new Fruits();
            timer1.Interval = snakeSpeed;
           
            
        }



        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    string login = tbLogin.Text;
                    string password = tbPassword.Text;

                    var users = db.Users.ToList();
                    foreach (var u in users)
                    {
                        if (u.Login == login && u.Password == password)
                        {

                            Start();
                            userWasFound = true;
                            record = u.Record;
                            userId = u.Id;
                            break;
                        }

                    }
                    if (!userWasFound)
                    {
                        MessageBox.Show("User is not found", "", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error", "", MessageBoxButtons.OK);
            }
           

           
        }

        public Direction newDirection = Direction.Right;

        private void Game_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (btnStart.Visible) return;   //Если кнопка скрыта,то начинаем игру
            //проверяем нажатия кнопок
            switch (e.KeyCode)
            {
                case Keys.D:
                    newDirection = Direction.Right;
                    break;
                case Keys.A:
                    newDirection = Direction.Left;
                    break;
                case Keys.W:
                    newDirection = Direction.Up;
                    break;
                case Keys.S:
                    newDirection = Direction.Down;
                    break;
                default:
                    break;

            }

            //UpdateForm();
        }
       

        public void UpdateForm()
        {

            if (snake.isGameOver)
            {
                
                timer1.Stop();
              //  snake.isGameOver = false;
                using (UserContext db = new UserContext())
                {
                    var newRecord = db.Users
                                    .Where(i => i.Id == userId).FirstOrDefault();
                    newRecord.Record = score;
                    record = score;
                    db.SaveChanges();
                   // label2.Text = score.ToString();
                }
               
                return;

            }
            else
            {
                timer1.Start();
                graphics.Clear(this.BackColor);
                snake.Move(apple);
                snake.SetDirection(newDirection);
                snake.DrawSnake(graphics);
                apple.DrawCircle(graphics);
                LabelScore.Text = score.ToString();
                label2.Text = $"Record: {record}";
            }
            
            
          
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("You definetly want to exit?", " ", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
       
                Application.Exit();
            }

        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Restart?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
               
                snake.SetDirection(Direction.Right);
                snake.isGameOver = false;
                snake.Restart();
               
                UpdateForm();

            }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            BtnRestart.Visible = false;
            
        }

        




        public void Start()
        {

               // Если в тексбоксе есть текст

            btnStart.Visible = false;  //скрываем кнопку старт 
            BtnSignUp.Visible = false;
            label1.Visible = false;
            label4.Visible = false;
            tbLogin.Visible = false;
            tbPassword.Visible = false;
            CbShowPassword.Hide();
            BtnRestart.Visible = true;
            apple = new Fruits();  //создаем фрукт 
            snake = new Snake();  // создаем змейку

            apple.DrawCircle(graphics);
            snake.DrawSnake(graphics);   //рисуем змейку
            //   label2.Text = tbName1.Text + ": ";
          
            gameStart = true;
            timer1.Enabled = true;

            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameStart)
            {
                    UpdateForm();
                        
            }
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
        }

   

        private void CbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (CbShowPassword.Checked)
            {
                tbPassword.PasswordChar = '*';
            }
            else
            {
                tbPassword.PasswordChar = '\0';
            }
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() ==DialogResult.Cancel)
            {
                return;
            }
            //  this.BackColor = colorDialog1.Color; //цвет фона
            btnStart.BackColor = colorDialog1.Color;
        }

        
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
   

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            //  this.BackColor = colorDialog1.Color; //цвет фона
            BtnRestart.BackColor = colorDialog1.Color;
        }

        private void starToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            btnStart.BackColor = colorDialog1.Color;
        }

        private void signUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            BtnSignUp.BackColor = colorDialog1.Color;
        }

        private void formToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            this.BackColor = colorDialog1.Color;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Exit.ForeColor = colorDialog1.Color;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Rating rating = new Rating();
            rating.Show();
        }
    }
}
