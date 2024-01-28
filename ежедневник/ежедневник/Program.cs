using System;
using System.Collections.Generic;

namespace DailyPlanner
{
    class Program
    {
        static List<Note> notes;
        static int currentIndex;

        static void Main(string[] args)
        {
            InitializeNotes();

            Console.WriteLine("Добро пожаловать в ежедневник!");

            while (true)
            {
                DisplayMenu();
                HandleInput();
            }
        }

        static void InitializeNotes()
        {
            notes = new List<Note>()
            {
                new Note("Заметка 1", "Описание заметки 1", new DateTime(2021, 6, 6)),
                new Note("Заметка 2", "Описание заметки 2", new DateTime(2021, 6, 8)),
                new Note("Заметка 3", "Описание заметки 3", new DateTime(2021, 6, 13)),
                new Note("Заметка 4", "Описание заметки 4", new DateTime(2021, 6, 15)),
                new Note("Заметка 5", "Описание заметки 5", new DateTime(2021, 6, 18))
            };

            currentIndex = 0;
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Ежедневник");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Дата: " + notes[currentIndex].Date.ToString("dd.MM.yyyy"));
            Console.WriteLine();
            Console.WriteLine("Заметка: " + notes[currentIndex].Title);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Стрелка влево - предыдущая заметка");
            Console.WriteLine("Стрелка вправо - следующая заметка");
            Console.WriteLine("Enter - подробная информация");
        }

        static void HandleInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    MoveToPreviousNote();
                    break;
                case ConsoleKey.RightArrow:
                    MoveToNextNote();
                    break;
                case ConsoleKey.Enter:
                    DisplayNoteDetails();
                    break;
            }
        }

        static void MoveToPreviousNote()
        {
            currentIndex--;

            if (currentIndex < 0)
                currentIndex = notes.Count - 1;
        }

        static void MoveToNextNote()
        {
            currentIndex++;

            if (currentIndex == notes.Count)
                currentIndex = 0;
        }

        static void DisplayNoteDetails()
        {
            Console.Clear();
            Console.WriteLine("Заметка: " + notes[currentIndex].Title);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Описание: " + notes[currentIndex].Description);
            Console.WriteLine("Дата создания: " + notes[currentIndex].Date.ToString("dd.MM.yyyy"));
            Console.WriteLine("Дата выполнения: " + notes[currentIndex].Deadline.ToString("dd.MM.yyyy"));
            Console.WriteLine("---------------------------");
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }
    }

    class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }

        public Note(string title, string description, DateTime date)
        {
            Title = title;
            Description = description;
            Date = date;
            Deadline = DateTime.Now.AddDays(7);
        }
    }
}