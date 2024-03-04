using System;
using System.IO;
using System.Linq;

public static class FileManager
{
    private static string currentDirectory;

    public static void Start()
    {
        Console.WriteLine("Welcome to the Console File Manager!\n");

        // Получаем список доступных дисков
        DriveInfo[] drives = DriveInfo.GetDrives();

        // Выводим список дисков
        for (int i = 0; i < drives.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {drives[i]}");
        }

        Console.WriteLine();

        // Пользователь выбирает диск
        int selectedDrive = ReadOption("Select a drive: ", drives.Length);

        // Устанавливаем текущую директорию на выбранный диск
        currentDirectory = drives[selectedDrive - 1].RootDirectory.FullName;

        // Отображаем содержимое диска
        ShowDirectoryContents(currentDirectory);
    }

    private static void ShowDirectoryContents(string path)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Current Directory: {path}\n");

            // Показываем список папок и файлов
            string[] directories = Directory.GetDirectories(path);
            for (int i = 0; i < directories.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {Path.GetFileName(directories[i])}");
            }

            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1 + directories.Length} {Path.GetFileName(files[i])}");
            }

            Console.WriteLine();

            // Пользователь выбирает пункт меню
            int selectedOption = ReadOption("Select a folder or file (or press 0 to go back): ",
                directories.Length + files.Length);

            if (selectedOption == 0)
            {
                // Если выбран 0, возвращаемся назад
                if (path == currentDirectory)
                {
                    // Если текущая директория - корневая директория диска, возвращаемся к выбору диска
                    Start();
                    return;
                }
                else
                {
                    // Иначе переходим в папку выше
                    path = Directory.GetParent(path)?.FullName;
                }
            }
            else if (selectedOption <= directories.Length)
            {
                // Если выбрана папка, переходим в нее
                path = directories[selectedOption - 1];
            }
            else
            {
                // Если выбран файл, запускаем его
                string filePath = files[selectedOption - directories.Length - 1];
                LaunchFile(filePath);
            }
        }
    }

    private static int ReadOption(string message, int maxOption)
    {
        int option;
        bool isValidOption;

        do
        {
            Console.Write(message);
            isValidOption = int.TryParse(Console.ReadLine(), out option) && option >= 0 && option <= maxOption;
        } while (!isValidOption);

        return option;
    }

    private static void LaunchFile(string filePath)
    {
        // Получаем расширение файла
        string extension = Path.GetExtension(filePath).ToLower();

        switch (extension)
        {
            case ".txt":
                // Запуск текстового файла через блокнот
                Console.WriteLine($"Opening {filePath} in Notepad...");
                // Здесь нужно вставить код для запуска файла через блокнот
                break;
            case ".docx":
                // Запуск документа в формате docx через Word
                Console.WriteLine($"Opening {filePath} in Word...");
                // Здесь нужно вставить код для запуска файла через Word
                break;
            // Добавьте другие форматы файлов, которые хотите поддерживать
            default:
                Console.WriteLine("Unsupported file format.");
                break;
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

public static class ConsoleArrow
{
    public static ConsoleKeyInfo GetArrowKey()
    {
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(intercept: true);
        } while (keyInfo.Key != ConsoleKey.UpArrow && keyInfo.Key != ConsoleKey.DownArrow);

        return keyInfo;
    }
}

class Program
{
    static void Main()
    {
        FileManager.Start();
    }
}
