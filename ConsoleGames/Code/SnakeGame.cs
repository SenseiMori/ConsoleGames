using ConsoleGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ConsoleGames.Code.Snake;

namespace ConsoleGames.Code
{

    public struct Point
    {
        public int X;
        public int Y;
        public char Ch;
        public Point(int x, int y, char ch = '@')
        {
            X = x;
            Y = y;
            Ch = ch;
        }
        public void Draw(int x, int y, char ch = '@')
        {
            X = x;
            Y = y;
            Ch = ch;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(ch);
        }
        public void Clear(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(' ');
        }
        public override bool Equals(object? obj)
        {
            if (obj is Point)
            {
                Point point = (Point)obj;
                return this.X == point.X && this.Y == point.Y;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Ch);
        }
    }
    class DrawningMap
    {
        public readonly int windowSizeX = 60;
        public readonly int windowSizeY = 30;
        public List<Point> allWalls;
        public void DrawWalls()
        {
            allWalls = new List<Point>();
            Console.SetWindowSize(windowSizeX + 2, windowSizeY + 2);
            DrawHorizontalWall();
            DrawVerticalWall();
        }

        public void DrawHorizontalWall()
        {
            for (int i = 0; i < windowSizeX; i++)
            {
                Point leftWall = new Point(i, 0);
                Point rightWall = new Point(i, windowSizeY);
                leftWall.Draw(i, 0, '#');
                rightWall.Draw(i, windowSizeY, '#');
                allWalls.Add(leftWall);
                allWalls.Add(rightWall);
            }
        }
        public void DrawVerticalWall()
        {
            for (int i = 1; i < windowSizeY; i++)
            {
                Point topWall = new Point(0, i);
                Point bottomWall = new Point(windowSizeY, i);
                topWall.Draw(0, i, '#');
                bottomWall.Draw(windowSizeX, i, '#');
                allWalls.Add(topWall);
                allWalls.Add(bottomWall);
            }
        }
    }
    class Coins
    {
        public Point dollar;
        public void DrawCoins()
        {
            DrawningMap drawMap = new DrawningMap();
            Random rand = new();
            dollar.Draw(rand.Next(2, drawMap.windowSizeX),
                        rand.Next(2, drawMap.windowSizeY),
                        '$'
                        );
        }
    }
    class Game
    {
        private Logic logic;
        private DrawningMap drawMap;
        private Coins coins;
        private Snake snake;
        public Game()
        {
            drawMap = new DrawningMap();
            coins = new Coins();
            snake = new Snake(drawMap, coins);
            logic = new Logic(drawMap, snake, coins);
        }
        public void Start()
        {
            drawMap.DrawWalls();
            coins.DrawCoins();
            while (true)
            {
                snake.HandleInput();
                snake.DrawSnake();
                logic.Eat(snake.newPositionHead);
                logic.HitInWall(snake.newPositionHead);
                logic.SpeedControl();
                Thread.Sleep(logic.speed);
            }
        }
    }
    class Logic
    {
        private DrawningMap walls;
        private Snake snake;
        private Coins coins;
        public bool hasEated;
        public int countCoins;
        public int speed = 150;

        public Logic(DrawningMap walls, Snake snake, Coins coins)
        {
            this.walls = walls;
            this.snake = snake;
            this.coins = coins;
        }
        public bool HitInWall(Point newHead)
        {
            if (walls.allWalls.Any(wall => wall.Equals(newHead)))
            {
                Console.Clear();
                Console.WriteLine("END");
                Console.ReadKey();
                Environment.Exit(0);
                return true;
            }
            return false;
        }
        public void SpeedControl()
        {
            for (int i = 0; i < countCoins; i++)
            {
                if (hasEated && speed >= 30)
                {
                    speed -= 25;
                    hasEated = false;
                }
            }
        }
        public bool Eat(Point newHead)
        {
            if (newHead.Equals(coins.dollar))
            {
                coins.DrawCoins();
                snake.tail.Add(new Point());
                countCoins++;
                hasEated = true;
                return hasEated;
            }
            return false;
        }
    }
    class Snake
    {
        private Coins coins;
        private DrawningMap walls;
        public List<Point> tail = new List<Point>();
        public Point head;
        public Point newPositionHead;
        public Point direction;
        public Snake(DrawningMap walls, Coins coins)
        {
            this.walls = walls;
            this.coins = coins;

            tail.Add(new Point(5, 5, '@'));
            tail.Add(new Point(5, 4, '@'));
            tail.Add(new Point(5, 3, '@'));
        }
        public void ClearSnake()
        {
            foreach (Point p in tail)
                head.Clear(p.X, p.Y);
        }
        public void DrawTail()
        {
            for (int i = 0; i < tail.Count; i++)
            {
                head.Draw(tail[i].X, tail[i].Y);
            }
        }
        public void DrawSnake()
        {
            ClearSnake();
            newPositionHead = new Point(tail[0].X + direction.X, tail[0].Y + direction.Y);
            tail.Insert(0, newPositionHead);
            tail.RemoveAt(tail.Count - 1);
            DrawTail();
        }
        public void HandleInput()
        {
            Thread inputThread = new Thread(() =>
            {
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.W:
                            changeDirection(0, -1);
                            break;
                        case ConsoleKey.S:
                            changeDirection(0, 1);
                            break;
                        case ConsoleKey.A:
                            changeDirection(-1, 0);
                            break;
                        case ConsoleKey.D:
                            changeDirection(1, 0);
                            break;
                    }
                }
            });
            inputThread.Start();
        }
        public void changeDirection(int deltaX, int deltaY)
        {
            direction = new Point(deltaX, deltaY);
        }

    }
}



