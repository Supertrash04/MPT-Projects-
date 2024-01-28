using System;
using System.IO;

namespace TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу:");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            string fileExtension = Path.GetExtension(filePath);

            switch (fileExtension)
            {
                case ".txt":
                    TxtFileProcessor txtFileProcessor = new TxtFileProcessor(filePath);
                    txtFileProcessor.LoadFile();
                    ShowLoadedData(txtFileProcessor.GetData());
                    ListenToCommands(txtFileProcessor);
                    break;
                case ".json":
                    JsonFileProcessor jsonFileProcessor = new JsonFileProcessor(filePath);
                    jsonFileProcessor.LoadFile();
                    ShowLoadedData(jsonFileProcessor.GetData());
                    ListenToCommands(jsonFileProcessor);
                    break;
                case ".xml":
                    XmlFileProcessor xmlFileProcessor = new XmlFileProcessor(filePath);
                    xmlFileProcessor.LoadFile();
                    ShowLoadedData(xmlFileProcessor.GetData());
                    ListenToCommands(xmlFileProcessor);
                    break;
                default:
                    Console.WriteLine("Неподдерживаемый формат файла!");
                    return;
            }
        }

        private static void ShowLoadedData(string data)
        {
            Console.WriteLine("Загруженные данные:");
            Console.WriteLine(data);
        }

        private static void ListenToCommands(IFileProcessor fileProcessor)
        {
            Console.WriteLine("Нажмите F1 для сохранения файла или Escape для выхода.");

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.F1:
                        fileProcessor.SaveFile();
                        Console.WriteLine("Файл сохранен!");
                        break;
                    case ConsoleKey.Escape:
                        Console.WriteLine("Программа завершена.");
                        return;
                }
            }
        }
    }

    public interface IFileProcessor
    {
        void LoadFile();
        void SaveFile();
        string GetData();
    }

    public class TxtFileProcessor : IFileProcessor
    {
        private string filePath;
        private string data;

        public TxtFileProcessor(string filePath)
        {
            this.filePath = filePath;
        }

        public void LoadFile()
        {
            data = File.ReadAllText(filePath);
        }

        public void SaveFile()
        {
            File.WriteAllText(filePath, data);
        }

        public string GetData()
        {
            return data;
        }
    }

    public class JsonFileProcessor : IFileProcessor
    {
        private string filePath;
        private string data;

        public JsonFileProcessor(string filePath)
        {
            this.filePath = filePath;
        }

        public void LoadFile()
        {
            // Загрузка файла JSON и преобразование его в модель
            // В данном примере просто считываем содержимое файла как строку
            data = File.ReadAllText(filePath);
        }

        public void SaveFile()
        {
            // Преобразование модели в JSON и сохранение в файл
            File.WriteAllText(filePath, data);
        }

        public string GetData()
        {
            return data;
        }
    }

    public class XmlFileProcessor : IFileProcessor
    {
        private string filePath;
        private string data;

        public XmlFileProcessor(string filePath)
        {
            this.filePath = filePath;
        }

        public void LoadFile()
        {
            // Загрузка файла XML и преобразование его в модель
            // В данном примере просто считываем содержимое файла как строку
            data = File.ReadAllText(filePath);
        }

        public void SaveFile()
        {
            // Преобразование модели в XML и сохранение в файл
            File.WriteAllText(filePath, data);
        }

        public string GetData()
        {
            return data;
        }
    }
}
