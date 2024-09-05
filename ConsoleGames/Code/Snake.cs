using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleGame
{
    class Snake
    {
        //test2

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
