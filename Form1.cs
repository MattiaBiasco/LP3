using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class frmSnake : Form
    {
        Random rand;
        enum GameBoardFields
        {
            Free,
            Snake,
            Bonus
        };

        enum Directions
        {
            Up,
            Down,
            Left,
            Right
        };

        struct SnakeCoordinates
        {
            public int x;
            public int y;
        }

        GameBoardFields[,] gameBoardField;
        SnakeCoordinates[] snakeXY;
        int snakeLength;
        Directions direction;
        Graphics g;

        
        public frmSnake()
        {
            InitializeComponent();
            gameBoardField = new GameBoardFields[11, 11];
            snakeXY = new SnakeCoordinates[100];
            rand = new Random();
        }

        private void frmSnake_Load(object sender, EventArgs e)
        {
            picGameBoard.Image = new Bitmap(420, 420);
            g = Graphics.FromImage(picGameBoard.Image);
            g.Clear(Color.White);

            for (int i = 1; i <= 10; i++)
            {
                //obere und untere Wand
                g.DrawImage(imgList.Images[3], i * 35, 0);
                g.DrawImage(imgList.Images[3], i * 35, 245);
            }

            for (int i = 0; i <= 11; i++)
            {
                //linke und rechte Wand
                g.DrawImage(imgList.Images[3], 0, i * 35);
                g.DrawImage(imgList.Images[3], 245, i * 35);
            }

            //initial snake Körper und Kopf
            snakeXY[0].x = 5; //Kopf
            snakeXY[0].y = 4;
            snakeXY[1].x = 5; //1.Körper
            snakeXY[1].y = 5;
            snakeXY[2].x = 5; //2.Körper
            snakeXY[2].y = 6;

            g.DrawImage(imgList.Images[2], 5 * 35, 4 * 35); //Kopf
            g.DrawImage(imgList.Images[1], 5 * 35, 5 * 35); //1.Körper
            g.DrawImage(imgList.Images[1], 5 * 35, 6 * 35); //2.Körper

            gameBoardField[5, 4] = GameBoardFields.Snake;
            gameBoardField[5, 5] = GameBoardFields.Snake;
            gameBoardField[5, 6] = GameBoardFields.Snake;

            direction = Directions.Up;
            snakeLength = 3;

            for (int i = 0; i < 4; i++)
            {
                Bonus();
            }
        }

        private void Bonus()
        {
            int x, y;
            var imgIndex = rand.Next(0);

            do
            {
                x = rand.Next(1, 6);
                y = rand.Next(1, 6);
            }
            while (gameBoardField[x, y] != GameBoardFields.Free);

            gameBoardField[x, y] = GameBoardFields.Bonus;
            g.DrawImage(imgList.Images[imgIndex], x * 35, y * 35);
        }

        private void frmSnake_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction = Directions.Up;
                    break;
                case Keys.Down:
                    direction = Directions.Down;
                    break;
                case Keys.Left:
                    direction = Directions.Left;
                    break;
                case Keys.Right:
                    direction = Directions.Right;
                    break;
            }
        }

        private void GameOver()
        {
            timer.Enabled = false;
            MessageBox.Show("GAME OVER");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //Ende der Snake löschen
            g.FillRectangle(Brushes.White, snakeXY[snakeLength - 1].x * 35, snakeXY[snakeLength - 1].y * 35, 35, 35);
            gameBoardField[snakeXY[snakeLength - 1].x, snakeXY[snakeLength - 1].y] = GameBoardFields.Free;
            
            
            for (int i = snakeLength; i>= 1; i--)
            {
                snakeXY[i].x = snakeXY[i - 1].x;
                snakeXY[i].y = snakeXY[i - 1].y;
            }

            g.DrawImage(imgList.Images[1], snakeXY[0].x * 35, snakeXY[0].y * 35);
            
            //Richtung ändern vom Kopf
            switch (direction)
            {
                case Directions.Up:
                    snakeXY[0].y = snakeXY[0].y - 1;
                    break;
                case Directions.Down:
                    snakeXY[0].y = snakeXY[0].y + 1;
                    break;
                case Directions.Left:
                    snakeXY[0].x = snakeXY[0].x - 1;
                    break;
                case Directions.Right:
                    snakeXY[0].x = snakeXY[0].x + 1;
                    break;



            }
        }
        
    }
}

