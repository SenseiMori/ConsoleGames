using ConsoleGames.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleGame
{

    class DrawMap
    {
     /*
     * 1. Размер поля равен размеру окна и ограничен стенами
     * 2. При столкновении со стеной игра оканчивается
     */
        private int [] windowWigth = new int [60];
        private int [] windowHeigth = new int [30];
        private char wall = '#';
        private char coins = '$';
        public void test0()
        {

            //Верх
            for (int i = 0; i < windowWigth.Length ; i++)
            {
                Console.Write(wall);
            }
            Console.WriteLine();
            //Левая и правая
            for (int i = 0; i < windowHeigth.Length - 2; i++)
            {
                Console.Write(wall);
                Console.SetCursorPosition(windowWigth.Length -1, Console.CursorTop);
                Console.Write(wall);
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, windowHeigth.Length - 1);
            //нижняя
            for (int i = 0; i < windowWigth.Length; i++)
            {
                Console.Write(wall);
            }
            Console.SetWindowSize(70, 35);
            Console.ReadLine();
        }

        public void DrawPlayground()
        {
            Random rand = new Random();
            int randomWigth = rand.Next(minValue: +2, windowWigth.Length - 2);
            int randomHeigth = rand.Next(minValue: +2, windowHeigth.Length - 2);



            //int[,] matrix = new int[windowWigth.Length, windowHeigth.Length];
            //for (int i = 0;i < windowWigth.Length;i++)
            //{
            //    matrix[i,0] = windowHeigth[i];
            //}
            //for (int i = 0; i< windowHeigth.Length;i++)
            //{
            //    matrix[i,0] = windowWigth[i];
            //}
            //for (int i = 0; i<windowWigth.Length;i++)
            //{
            //    for (int j = 0; j< windowHeigth.Length;j++)
            //    {
            //        Console.Write(matrix[i,j]);
            //    }
            //    Console.WriteLine();
            //}
            //matrix[randomHeigth, randomWigth] = '@';

        }
    }



    class SnakeGame
    {


 

        class Snake
        {

            public static void Game()
            {
                // объявление позиции головы змеи на поле
                int xPlayer = 5;
                int yPlayer = 5;


                // присваивание значения первого элемента хвоста змеи ее голове
                int xSnakeTail = xPlayer;
                int ySnakeTail = yPlayer;
                //то же самое со вторым элементом хвоста
                int xSnakeTail1 = xSnakeTail;
                int ySnakeTail1 = ySnakeTail;
                // и с третьим
                int xSnakeTail2 = xSnakeTail1;
                int ySnakeTail2 = ySnakeTail1;

                //инициализация змеи как массива символов
                char[] snake = { '@', '@', '@', '@' };

                Console.CursorVisible = false;
                //инициализация  монеток на поле
                int coins = 0;
                //и счетчика очков
                int countDollars = 0;



                //инициализация карты как двумерного массива символов
                char[,] playground = {{'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#','#',},
                                 { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '$', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '$', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.','#' },
                                 { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#','#',},
            };



                int rows = playground.GetLength(0);
                int cols = playground.GetLength(1);

                Random rand = new Random();


                //Основной цикл игры
                while (true)
                {
                    //инициализация переменных для появления монеток на игровом поле, чтобы они не появлялись в стенах
                    int randomRow = rand.Next(minValue: +2, rows - 2);
                    int randomCol = rand.Next(minValue: +2, cols - 2);

                    //цикл, отображающий поле
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            Console.Write(playground[i, j]);

                        }
                        Console.WriteLine();
                    }

                    Console.SetCursorPosition(0, 24);
                    Console.WriteLine("Нажмите ESC, чтобы закрыть приложение");


                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine($"Coins: {coins}");
                    //появление головы змеи на поле 
                    Console.SetCursorPosition(yPlayer, xPlayer);
                    Console.WriteLine(snake[0]);

                    //Условия, при которых хвост змеи увеличивается на один элемент
                    if (coins >= 1)
                    {
                        Console.SetCursorPosition(ySnakeTail, xSnakeTail);
                        Console.WriteLine(snake[1]);

                    }
                    if (coins >= 2)
                    {
                        Console.SetCursorPosition(ySnakeTail1, xSnakeTail1);
                        Console.WriteLine(snake[2]);
                    }
                    if (coins >= 3)
                    {
                        Console.SetCursorPosition(ySnakeTail2, xSnakeTail2);
                        Console.WriteLine(snake[3]);
                    }


                    //Условие, при котором если игрок собрал монетку, ее местоположение становится точкой, т. е. элементом игрового поля.
                    // 
                    if (playground[xPlayer, yPlayer] == '$')
                    {
                        playground[xPlayer, yPlayer] = '.';
                        coins++;
                        countDollars++;

                        //Условие, при котором на поле появляются новые монетки
                        if (countDollars >= 1)
                        {
                            playground[randomRow, randomCol] = '$';
                        }

                        countDollars = 0;
                    }

                    //Контроллер передвижения
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.W:
                            if (playground[xPlayer - 1, yPlayer] != '#')
                            {
                                //Передает значения предыдущих элементов следующим, тем самым змея движется.
                                //Код для всех клавиш передвижения идентичен

                                xSnakeTail2 = xSnakeTail1;
                                ySnakeTail2 = ySnakeTail1;

                                xSnakeTail1 = xSnakeTail;
                                ySnakeTail1 = ySnakeTail;

                                xSnakeTail = xPlayer;
                                ySnakeTail = yPlayer;

                                xPlayer--;

                            }
                            break;
                        case ConsoleKey.S:
                            if (playground[xPlayer + 1, yPlayer] != '#')
                            {
                                xSnakeTail2 = xSnakeTail1;
                                ySnakeTail2 = ySnakeTail1;

                                xSnakeTail1 = xSnakeTail;
                                ySnakeTail1 = ySnakeTail;

                                xSnakeTail = xPlayer;
                                ySnakeTail = yPlayer;

                                xPlayer++;
                            }
                            break;
                        case ConsoleKey.D:
                            if (playground[xPlayer, yPlayer + 1] != '#')
                            {
                                xSnakeTail2 = xSnakeTail1;
                                ySnakeTail2 = ySnakeTail1;

                                xSnakeTail1 = xSnakeTail;
                                ySnakeTail1 = ySnakeTail;

                                xSnakeTail = xPlayer;
                                ySnakeTail = yPlayer;

                                yPlayer++;
                            }
                            break;
                        case ConsoleKey.A:
                            if (playground[xPlayer, yPlayer - 1] != '#')
                            {
                                xSnakeTail2 = xSnakeTail1;
                                ySnakeTail2 = ySnakeTail1;

                                xSnakeTail1 = xSnakeTail;
                                ySnakeTail1 = ySnakeTail;

                                xSnakeTail = xPlayer;
                                ySnakeTail = yPlayer;

                                yPlayer--;
                            }
                            break;
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;


                    }

                    //Task.Delay(100).Wait();
                    //Очистка поля, змеи и обновление цикла 
                    Console.Clear();

                }

            }
        }
    }
}
