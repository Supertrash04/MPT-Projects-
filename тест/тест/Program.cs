using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace TypingTest
{
    [DataContract]
    class User
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int CharactersPerMinute { get; set; }
        [DataMember]
        public int CharactersPerSecond { get; set; }
    }

    static class RecordTable
    {
        private const string FilePath = "records.json";

        public static List<User> Users { get; private set; }

        static RecordTable()
        {
            LoadRecords();
        }

        public static void AddUser(User user)
        {
            Users.Add(user);
            SaveRecords();
        }

        public static void ShowRecords()
        {
            Console.WriteLine("\nRecord Table:");
            Console.WriteLine("{0,-10} {1,-10} {2,-10}", "Name", "CPM", "CPS");

            foreach (var user in Users)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10}", user.Name, user.CharactersPerMinute, user.CharactersPerSecond);
            }
        }

        private static void LoadRecords()
        {
            if (File.Exists(FilePath))
            {
                using (var stream = new FileStream(FilePath, FileMode.Open))
                {
                    var serializer = new DataContractJsonSerializer(typeof(List<User>));
                    Users = (List<User>)serializer.ReadObject(stream);
                }
            }
            else
            {
                Users = new List<User>();
            }
        }

        private static void SaveRecords()
        {
            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<User>));
                serializer.WriteObject(stream, Users);
            }
        }
    }

    class TypingTest
    {
        private static User user;
        private static bool typingFinished;
        private static Stopwatch stopwatch;

        public static void Start()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            user = new User { Name = name };

            stopwatch = Stopwatch.StartNew();

            Console.WriteLine("Type the following text:");
            Console.WriteLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
            Console.WriteLine("Press Enter to start typing.");

            Console.ReadLine();

            ConsoleKeyInfo keyInfo;

            Console.Write(">");
            do
            {
                keyInfo = Console.ReadKey(true);

                if (typingFinished)
                {
                    break;
                }

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    typingFinished = true;
                }
                else
                {
                    Console.Write(keyInfo.KeyChar);
                }
            } while (true);

            stopwatch.Stop();

            int totalCharacters = Console.CursorLeft;
            int elapsedTimeInSeconds = (int)stopwatch.Elapsed.TotalSeconds;

            user.CharactersPerMinute = (int)(totalCharacters / stopwatch.Elapsed.TotalMinutes);
            user.CharactersPerSecond = totalCharacters / elapsedTimeInSeconds;

            RecordTable.AddUser(user);

            RecordTable.ShowRecords();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                TypingTest.Start();

                Console.WriteLine("\nPress Enter to retry or any other key to exit...");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }
    }
}