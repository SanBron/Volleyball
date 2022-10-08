using System;
using System.Linq;
using System.Threading;
using KeyboardMenuDemo;

using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace KeyboardMenuDemo
{

    public class Field
    {
        Settings FieldSettings = new Settings();
        int[] Gdata = new int[3];
        public static int fieldLength, fieldWidth, GameSpeed;
        public const int MaxScore = 10;
        const char TopLineTile = '=';
        const char BottomLineTile = '.';
        public string TopLine;
        public string BottomLine;

        public String topLine {
            get { return TopLine; }
            set { TopLine = value; }
        }

        public String bottomLine
        {
            get { return BottomLine; }
            set { BottomLine = value; }
        }

        public Field()
        {
            Gdata = FieldSettings.SetOptions();
        }
        public void Initialization()
        {
            fieldLength = Gdata[0];
            fieldWidth = Gdata[1];
            GameSpeed = Gdata[2];
        }
        public void CreateField()
        {
            TopLine = string.Concat(Enumerable.Repeat(TopLineTile, fieldLength));
            BottomLine = string.Concat(Enumerable.Repeat(BottomLineTile, fieldLength));


            Console.SetCursorPosition(1, 0);
            Console.WriteLine(TopLine);

            Console.SetCursorPosition(1, fieldWidth);
            Console.WriteLine(BottomLine);
            for (int i = 1; i < fieldWidth / 2; i++)
            {
                Console.SetCursorPosition(fieldLength / 2 - 1, fieldWidth - i);
                Console.WriteLine('|');
                Console.SetCursorPosition(fieldLength / 2, fieldWidth - i);
                Console.WriteLine('|');
            }
            for (int i = 1; i < fieldWidth; i++)
            {
                Console.SetCursorPosition(fieldLength, fieldWidth - i);
                Console.WriteLine('|');
                Console.SetCursorPosition(1, fieldWidth - i);
                Console.WriteLine('|');
            }
        }
    }

    public class Rackets
    {
        public int racketLength = 6;
        const char racketTile = '-';
        public int leftRacketHeight = 0;
        public int rightRacketHeight = Field.fieldLength / 2;

        public void PrintTheRacket()
        {
            for (int i = 1; i < racketLength; i++)
            {
                Console.SetCursorPosition(i + 1 + leftRacketHeight, Field.fieldWidth);
                Console.WriteLine(racketTile);
                Console.SetCursorPosition(i + rightRacketHeight, Field.fieldWidth);
                Console.WriteLine(racketTile);
            }
        }
    }

    public class Ball
    {
        public int ballX = Field.fieldLength / 2;
        public int ballY = Field.fieldWidth / 2;
        public char ballTile = 'o';

        public bool isBallGoingDown = true;
        public bool isBallGoingRight = true;
        public void BallPosition(int X, int Y)
        {
            ballX = X;
            ballY = Y;
        }
    }

    public class Points
    {
        public int leftPlayerPoints = 0;
        public int rightPlayerPoints = 0;

    }

    public class Scoreboard
    {
        public int scoreboardX = Field.fieldLength / 2 - 2;
        public int scoreboardY = Field.fieldWidth + 1;
    }

    public class Starter
    {
        public static void GameProcess()
        {
            Field field = new Field();
            field.Initialization();
            Rackets rackets = new Rackets();
            Points points = new Points();
            Scoreboard scoreboard = new Scoreboard();
            Ball ball = new Ball();
            while (true)
            {
                field.CreateField();
                rackets.PrintTheRacket();
                while (!Console.KeyAvailable)
                {

                    Console.SetCursorPosition(ball.ballX, ball.ballY);
                    Console.WriteLine(ball.ballTile);
                    Thread.Sleep(Field.GameSpeed);

                    Console.SetCursorPosition(ball.ballX, ball.ballY);
                    Console.WriteLine(" ");
                    if (ball.isBallGoingDown)
                    {
                        ball.ballY++;
                    }
                    else
                    {
                        ball.ballY--;
                    }

                    if (ball.isBallGoingRight)
                    {
                        ball.ballX++;
                    }
                    else
                    {
                        ball.ballX--;
                    }

                    if (ball.ballY == 1)
                    {
                        ball.isBallGoingDown = !ball.isBallGoingDown;
                    }

                    if (ball.ballX == 2 || ball.ballX == Field.fieldLength - 2)
                    {
                        ball.isBallGoingRight = !ball.isBallGoingRight;
                    }
                    else if (ball.ballX == Field.fieldLength / 2 && ball.ballY >= Field.fieldWidth / 2)
                    {
                        points.leftPlayerPoints++;
                        ball.BallPosition(Field.fieldLength / 2, Field.fieldWidth / 2 - 2);
                        Console.SetCursorPosition(scoreboard.scoreboardX, scoreboard.scoreboardY);
                        Console.WriteLine($"{points.leftPlayerPoints} | {points.rightPlayerPoints}");
                        ball.isBallGoingRight = true;
                        if (points.leftPlayerPoints == Field.MaxScore)
                        {
                            goto outer;
                        }
                    }
                    else if (ball.ballX == Field.fieldLength / 2 - 1 && ball.ballY >= Field.fieldWidth / 2)
                    {
                        points.rightPlayerPoints++;
                        ball.BallPosition(Field.fieldLength / 2, Field.fieldWidth / 2 - 2);
                        Console.SetCursorPosition(scoreboard.scoreboardX, scoreboard.scoreboardY);
                        Console.WriteLine($"{points.leftPlayerPoints} | {points.rightPlayerPoints}");
                        ball.isBallGoingRight = false;
                        if (points.rightPlayerPoints == Field.MaxScore)
                        {
                            goto outer;
                        }
                    }
                    if (ball.ballY == Field.fieldWidth - 1)
                    {
                        if (ball.ballX >= Field.fieldLength / 2)
                        {
                            if (ball.ballX >= rackets.rightRacketHeight + 1 && ball.ballX <= rackets.rightRacketHeight + rackets.racketLength - 1)
                            {
                                ball.isBallGoingDown = !ball.isBallGoingDown;
                            }
                            else
                            {
                                points.leftPlayerPoints++;
                                ball.ballY = Field.fieldWidth / 2 - 2;
                                ball.ballX = Field.fieldLength / 2;
                                Console.SetCursorPosition(scoreboard.scoreboardX, scoreboard.scoreboardY);
                                Console.WriteLine($"{points.leftPlayerPoints} | {points.rightPlayerPoints}");
                                ball.isBallGoingRight = false;
                                if (points.leftPlayerPoints == Field.MaxScore)
                                {
                                    goto outer;
                                }
                            }
                        }
                        else if (ball.ballX <= Field.fieldLength / 2)
                        {
                            if (ball.ballX >= rackets.leftRacketHeight + 2 && ball.ballX <= rackets.leftRacketHeight + rackets.racketLength)
                            {
                                ball.isBallGoingDown = !ball.isBallGoingDown;
                            }
                            else
                            {
                                points.rightPlayerPoints++;
                                ball.ballY = Field.fieldWidth / 2 - 2;
                                ball.ballX = Field.fieldLength / 2;
                                Console.SetCursorPosition(scoreboard.scoreboardX, scoreboard.scoreboardY);
                                Console.WriteLine($"{points.leftPlayerPoints} | {points.rightPlayerPoints}");
                                ball.isBallGoingRight = true;
                                if (points.rightPlayerPoints == Field.MaxScore)
                                {
                                    goto outer;
                                }
                            }
                        }
                    }
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (rackets.rightRacketHeight > Field.fieldLength / 2)
                        {
                            rackets.rightRacketHeight--;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (rackets.rightRacketHeight < Field.fieldLength - rackets.racketLength - 1)
                        {
                            rackets.rightRacketHeight++;

                        }
                        break;

                    case ConsoleKey.A:
                        if (rackets.leftRacketHeight > 0)
                        {
                            rackets.leftRacketHeight--;
                        }
                        break;

                    case ConsoleKey.D:
                        if (rackets.leftRacketHeight < Field.fieldLength / 2 - rackets.racketLength - 2)
                        {
                            rackets.leftRacketHeight++;
                        }
                        break;
                }



                for (int i = 1; i < Field.fieldWidth; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(Field.fieldLength - 1, i);
                    Console.WriteLine(" ");
                }
            }
        outer:;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            if (points.rightPlayerPoints == 10)
            {
                Console.WriteLine($"Игрок справа первым набрал {Field.MaxScore} очков");
            }
            else
            {
                Console.WriteLine($"Игрок справа первым набрал {Field.MaxScore} очков");
            }
            Console.WriteLine("Настройки очищенны");
            Console.WriteLine("Спасибо за игру!");
            StreamWriter ender = new StreamWriter("C:\\Users\\sanmo\\source\\repos\\Volleyball\\Menu\\options.txt");
            ender.WriteLine(" ");
            ender.Close();
        }
    }
}