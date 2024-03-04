using System;
using System.Collections.Generic;
using System.Threading;

public enum Border
{
    MaxRight = 50,
    MaxBottom = 20
}

public class SnakeGame
{
    private bool gameover;
    private Thread drawThread;
    private List<int[]> snake;
    private int[] food;

    public SnakeGame()
    {
        Console.CursorVisible = false;
        Console.SetWindowSize((int)Border.MaxRight, (int)Border.MaxBottom);
        Console.SetBufferSize((int)Border.MaxRight, (int)Border.MaxBottom);
        Console.ForegroundColor = ConsoleColor.Green;

        snake = new List<int[]>
        {
            new int[] { (int)Border.MaxRight / 2, (int)Border.MaxBottom / 2 }
        };
        food = GenerateFood();

        drawThread = new Thread(Draw);
    }

    public void StartGame()
    {
        Console.Clear();
        gameover = false;

        drawThread.Start();

        while (!gameover)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        ChangeDirection(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        ChangeDirection(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        ChangeDirection(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        ChangeDirection(1, 0);
                        break;
                }
            }

            Move();

            if (snake[0][0] == food[0] && snake[0][1] == food[1])
            {
                EatFood();
                food = GenerateFood();
            }

            if (CheckCollision())
            {
                gameover = true;
            }

            Thread.Sleep(100);
        }

        drawThread.Join();

        Console.Clear();
        Console.WriteLine("ПОТРАЧЕНО");
    }

    private void Draw()
    {
        while (!gameover)
        {
            Console.Clear();

            foreach (int[] position in snake)
            {
                Console.SetCursorPosition(position[0], position[1]);
                Console.Write("■");
            }

            Console.SetCursorPosition(food[0], food[1]);
            Console.Write("⭐");

            Thread.Sleep(50);
        }
    }

    private void Move()
    {
        List<int[]> newSnake = new List<int[]>();


        for (int i = 1; i < snake.Count; i++)
        {
            newSnake.Add(new int[] { snake[i - 1][0], snake[i - 1][1] });
        }

        snake = newSnake;
        CheckBoundary();
    }

    private void ChangeDirection(int x, int y)
    {
        if (snake[0][2] + x != 0 || snake[0][3] + y != 0)
        {
            snake[0][2] = x;
            snake[0][3] = y;
        }
    }

    private void CheckBoundary()
    {
        if (snake[0][0] < 0)
        {
            snake[0][0] = (int)Border.MaxRight - 1;
        }
        else if (snake[0][0] >= (int)Border.MaxRight)
        {
            snake[0][0] = 0;
        }

        if (snake[0][1] < 0)
        {
            snake[0][1] = (int)Border.MaxBottom - 1;
        }
        else if (snake[0][1] >= (int)Border.MaxBottom)
        {
            snake[0][1] = 0;
        }
    }

    private bool CheckCollision()
    {
        for (int i = 1; i < snake.Count; i++)
        {
            if (snake[0][0] == snake[i][0] && snake[0][1] == snake[i][1])
            {
                return true;
            }
        }

        return false;
    }

    private int[] GenerateFood()
    {
        Random random = new Random();
        int x = random.Next((int)Border.MaxRight);
        int y = random.Next((int)Border.MaxBottom);

        while (CheckFoodLocation(x, y))
        {
            x = random.Next((int)Border.MaxRight);
            y = random.Next((int)Border.MaxBottom);
        }

        return new int[] { x, y };
    }

    private bool CheckFoodLocation(int x, int y)
    {
        foreach (int[] position in snake)
        {
            if (position[0] == x && position[1] == y)
            {
                return true;
            }
        }

        return false;
    }

    private void EatFood()
    {
        snake.Add(new int[] { snake[snake.Count - 1][0], snake[snake.Count - 1][1] });
    }
}

class Program
{
    static void Main(string[] args)
    {
        SnakeGame game = new SnakeGame();
        game.StartGame();
    }
}