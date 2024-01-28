using System;
using System.IO;
using System.Collections.Generic;

class CakeOrder
{
    private string form;
    private string size;
    private string flavor;
    private int quantity;
    private string icing;
    private string decor;

    public CakeOrder()
    {
        form = "";
        size = "";
        flavor = "";
        quantity = 0;
        icing = "";
        decor = "";
    }

    public void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("Выберите пункт меню:");
        Console.WriteLine("1. Форма");
        Console.WriteLine("2. Размер");
        Console.WriteLine("3. Вкус");
        Console.WriteLine("4. Количество");
        Console.WriteLine("5. Глазурь");
        Console.WriteLine("6. Декор");
        Console.WriteLine("7. Завершить заказ");

        int selectedItem = 0;
        int menuItemsCount = 7;

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedItem = selectedItem > 0 ? selectedItem - 1 : menuItemsCount - 1;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedItem = selectedItem < menuItemsCount - 1 ? selectedItem + 1 : 0;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                switch (selectedItem)
                {
                    case 0:
                        form = SubMenu("Форма", new string[] { "Квадратная", "Круглая", "Прямоугольная" });
                        break;
                    case 1:
                        size = SubMenu("Размер", new string[] { "Маленький", "Средний", "Большой" });
                        break;
                    case 2:
                        flavor = SubMenu("Вкус", new string[] { "Шоколадный", "Ванильный", "Фруктовый" });
                        break;
                    case 3:
                        Console.Write("Введите количество: ");
                        int.TryParse(Console.ReadLine(), out quantity);
                        break;
                    case 4:
                        icing = SubMenu("Глазурь", new string[] { "Шоколадная", "Сливочная", "Фруктовая" });
                        break;
                    case 5:
                        decor = SubMenu("Декор", new string[] { "Цветы", "Фигурки", "Надписи" });
                        break;
                    case 6:
                        SaveOrder();
                        return;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine("Выберите пункт меню:");
            for (int i = 0; i < menuItemsCount; i++)
            {
                if (i == selectedItem)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }

                switch (i)
                {
                    case 0:
                        Console.Write("Форма");
                        break;
                    case 1:
                        Console.Write("Размер");
                        break;
                    case 2:
                        Console.Write("Вкус");
                        break;
                    case 3:
                        Console.Write("Количество");
                        break;
                    case 4:
                        Console.Write("Глазурь");
                        break;
                    case 5:
                        Console.Write("Декор");
                        break;
                    case 6:
                        Console.Write("Завершить заказ");
                        break;
                }

                Console.WriteLine();
            }
        }
    }

    private string SubMenu(string category, string[] options)
    {
        Console.Clear();
        Console.WriteLine($"Выберите {category}:");
        Console.WriteLine();

        int selectedItem = 0;
        int optionsCount = options.Length;

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedItem = selectedItem > 0 ? selectedItem - 1 : optionsCount - 1;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedItem = selectedItem < optionsCount - 1 ? selectedItem + 1 : 0;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                return options[selectedItem];
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return "";
            }

            Console.Clear();
            Console.WriteLine($"Выберите {category}:");
            for (int i = 0; i < optionsCount; i++)
            {
                if (i == selectedItem)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }

                Console.WriteLine(options[i]);
            }
        }
    }

    private void SaveOrder()
    {
        string orderDetails = $"Форма: {form}\n" +
                              $"Размер: {size}\n" +
                              $"Вкус: {flavor}\n" +
                              $"Количество: {quantity}\n" +
                              $"Глазурь: {icing}\n" +
                              $"Декор: {decor}\n" +
                              $"Стоимость: {CalculatePrice()} руб.";

        using (StreamWriter writer = new StreamWriter("история заказов.txt", true))
        {
            writer.WriteLine(orderDetails);
        }
    }

    private int CalculatePrice()
    {
        // рассчитать цену заказа в зависимости от выбранных параметров торта
        int basePrice = 100; // базовая цена торта
        int totalPrice = basePrice * quantity; // общая цена заказа
        return totalPrice;
    }

    public static void Main(string[] args)
    {
        do
        {
            CakeOrder order = new CakeOrder();
            order.DisplayMenu();

            Console.WriteLine("Заказ оформлен!");
            Console.WriteLine("Нажмите любую клавишу для оформления нового заказа или Escape для выхода.");
        } while (Console.ReadKey().Key != ConsoleKey.Escape);
    }
}

class ArrowMenu
{
    public string Description { get; set; }
    public int Price { get; set; }

    public ArrowMenu(string description, int price)
    {
        Description = description;
        Price = price;
    }
}
