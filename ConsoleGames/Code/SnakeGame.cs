using ConsoleGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
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
    class DrawMap
    {
        private readonly int windowSizeX = int.Parse(ConfigurationManager.AppSettings["WindowSizeX"]!);
        private readonly int windowSizeY = int.Parse(ConfigurationManager.AppSettings["WindowSizeY"]!);
        private readonly int WindowPadding = int.Parse(ConfigurationManager.AppSettings["WindowPadding"]!);
        private Point _element = new Point();
        private Random _rand = new();
        public List<Point>? allWalls;
        public Point dollar;

        public void DrawWalls()
        {

           allWalls = new List<Point>();
           Console.SetWindowSize(windowSizeX + WindowPadding, windowSizeY + WindowPadding);
           DrawHorizontal(windowSizeX, 0);
           DrawHorizontal(windowSizeX, windowSizeY);
           DrawVertical(0, windowSizeY);
           DrawVertical(windowSizeX, windowSizeY);
           
        }


        public void DrawHorizontal(int x, int y)
        {
            if (allWalls is not null)
                {
                    for (int i = 0; i < x; i++)
                        {
                            _element.Draw(i, y, '#');
                            allWalls.Add(_element);
                        }
                }
        }
        public void DrawVertical(int x, int y)
        {
            if (allWalls is not null)
                {
                    for (int i = 0; i < y; i++)
                        {
                            _element.Draw(x, i, '#');
                            allWalls.Add(_element);  
                        }
            }
        }
        public void DrawCoins()
        {
            dollar.Draw(_rand.Next(2, windowSizeX),
                        _rand.Next(2, windowSizeY),
                        '$'
                        );
        }
    }
 
    class Game
    {
        private Logic _logic;
        private DrawMap _map;
        private DrawSnake _snake;
        public Game()
        {
            _map = new DrawMap();
            _snake = new DrawSnake();
            _logic = new Logic(_map, _snake);
        }
        public void Start()
        {
            _map.DrawWalls();
            _map.DrawCoins();
            while (true)
            {
                _snake.DrawHead();
                _logic.HandleInput();
                _logic.Eat();
                _logic.HitInWall();
                Thread.Sleep(_logic.currentSpeed);
            }
        }
    }
    class Logic
    {
        private DrawMap _map;
        private DrawSnake _snake;
        private int _minimalSpeed = 50;
        private int _stepDecrease = 25;
        public int currentSpeed = 150;

        public Logic(DrawMap walls, DrawSnake snake)
        {
            _map = walls;
            _snake = snake;
        }
        public bool HitInWall()
        {
            if(_map.allWalls is not null)
               { 
                    if (_map.allWalls.Any(wall => wall.Equals(_snake.newPositionHead)))
                    {
                        Console.Clear();
                        Console.WriteLine("END");
                        Console.ReadKey();
                        Environment.Exit(0);
                        return true;
                    }
                }
            return false;
        }

        public void SpeedControl()
        {
                if (currentSpeed >= _minimalSpeed)
                    currentSpeed -= _stepDecrease;
        }

        public bool CheckEat()
        {
            if (_snake.newPositionHead.Equals(_map.dollar))
                return true;
            return false;   
        }

        public void Eat()
        {
            if (CheckEat())
            {
                SpeedControl();
                _map.DrawCoins();
                _snake.tail.Add(new Point());
            }
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
                            _snake.changeDirection(0, -1);
                            break;
                        case ConsoleKey.S:
                            _snake.changeDirection(0, 1);
                            break;
                        case ConsoleKey.A:
                            _snake.changeDirection(-1, 0);
                            break;
                        case ConsoleKey.D:
                            _snake.changeDirection(1, 0);
                            break;
                    }
                }
            });
            inputThread.Start();
        }

    }

    class DrawSnake
    {
        private Point _head;
        private Point _direction;
        public Point newPositionHead;
        public List<Point> tail = new List<Point>();

        public DrawSnake()
        {

            tail.Add(new Point(5, 5, '@'));
            tail.Add(new Point(5, 4, '@'));
            tail.Add(new Point(5, 3, '@'));
         
        }
        public void DrawHead()
        {
            ClearSnake();
            newPositionHead = new Point(tail[0].X + _direction.X, tail[0].Y + _direction.Y);
            tail.Insert(0, newPositionHead);
            tail.RemoveAt(tail.Count - 1);
            DrawTail();
        }
        public void DrawTail()
        {
            for (int i = 0; i < tail.Count; i++)
            {
                _head.Draw(tail[i].X, tail[i].Y);
            }
        }
        public void ClearSnake()
        {
            foreach (Point p in tail)
                _head.Clear(p.X, p.Y);
        }
        public void changeDirection(int deltaX, int deltaY)
        {
            _direction = new Point(deltaX, deltaY);
        }

    }
}



