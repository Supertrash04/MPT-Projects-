using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string UserId { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class InformationSystem
    {
        private List<User> users;
        private List<Employee> employees;
        private List<Product> products;

        public InformationSystem()
        {
            users = new List<User>();
            employees = new List<Employee>();
            products = new List<Product>();
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Авторизация");
                Console.WriteLine("2. Выход");
                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        return;
                }
            }
        }

        private void Login()
        {
            Console.Clear();
            Console.Write("Логин: ");
            string login = Console.ReadLine();
            Console.Write("Пароль: ");
            string password = ReadPassword();

            User user = FindUser(login, password);
            if (user != null)
            {
                Console.WriteLine($"Вы вошли как {user.Login}");

                Employee employee = FindEmployee(user.Id);
                if (employee != null)
                {
                    Console.WriteLine($"Имя сотрудника: {employee.Name}");
                }

                switch (user.Role)
                {
                    case "Администратор":
                        ShowAdminMenu();
                        break;
                    case "Менеджер персонала":
                        ShowPersonnelManagerMenu();
                        break;
                    case "Склад-менеджер":
                        ShowWarehouseManagerMenu();
                        break;
                    case "Бухгалтер":
                        ShowAccountantMenu();
                        break;
                    case "Кассир":
                        ShowCashierMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Неверный логин или пароль");
                Console.ReadLine();
            }
        }

        private User FindUser(string login, string password)
        {
            return users.Find(u => u.Login == login && u.Password == password);
        }

        private Employee FindEmployee(string userId)
        {
            return employees.Find(e => e.UserId == userId);
        }

        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        private void ShowAdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Показать всех пользователей");
                Console.WriteLine("2. Добавить пользователя");
                Console.WriteLine("3. Изменить пользователя");
                Console.WriteLine("4. Удалить пользователя");
                Console.WriteLine("5. Поиск пользователя");
                Console.WriteLine("6. Назад");
                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowUsers();
                        break;
                    case "2":
                        AddUser();
                        break;
                    case "3":
                        EditUser();
                        break;
                    case "4":
                        DeleteUser();
                        break;
                    case "5":
                        SearchUser();
                        break;
                    case "6":
                        return;
                }
            }
        }

        private void ShowUsers()
        {
            // Вывод пользователей системы
            Console.WriteLine("Список пользователей:");
            foreach (var user in users)
            {
                Console.WriteLine($"Логин: {user.Login}, Роль: {user.Role}");
            }
            Console.ReadLine();
        }

        private void AddUser()
        {
            // Добавление нового пользователя
            Console.Clear();
            Console.Write("Логин: ");
            string login = Console.ReadLine();
            Console.Write("Пароль: ");
            string password = ReadPassword();
            Console.Write("Роль: ");
            string role = Console.ReadLine();

            User newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Login = login,
                Password = password,
                Role = role
            };

            users.Add(newUser);
            SaveData();

            Console.WriteLine("Пользователь добавлен");
            Console.ReadLine();
        }

        private void EditUser()
        {
            // Редактирование пользователя
            Console.Clear();
            Console.Write("Логин пользователя для изменения: ");
            string login = Console.ReadLine();

            User user = users.Find(u => u.Login == login);
            if (user != null)
            {
                Console.Write("Новый логин: ");
                string newLogin = Console.ReadLine();
                Console.Write("Новый пароль: ");
                string newPassword = ReadPassword();
                Console.Write("Новая роль: ");
                string newRole = Console.ReadLine();

                user.Login = newLogin;
                user.Password = newPassword;
                user.Role = newRole;
                SaveData();

                Console.WriteLine("Пользователь изменен");
            }
            else
            {
                Console.WriteLine("Пользователь не найден");
            }

            Console.ReadLine();
        }

        private void DeleteUser()
        {
            // Удаление пользователя
            Console.Clear();
            Console.Write("Логин пользователя для удаления: ");
            string login = Console.ReadLine();

            User user = users.Find(u => u.Login == login);
            if (user != null)
            {
                users.Remove(user);
                SaveData();
                Console.WriteLine("Пользователь удален");
            }
            else
            {
                Console.WriteLine("Пользователь не найден");
            }

            Console.ReadLine();
        }

        private void SearchUser()
        {
            // Поиск пользователя
            Console.Clear();
            Console.WriteLine("Выберите атрибут для поиска:");
            Console.WriteLine("1. Логин");
            Console.WriteLine("2. Роль");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Логин: ");
                    string login = Console.ReadLine();
                    User user = users.Find(u => u.Login == login);
                    if (user != null)
                    {
                        Console.WriteLine($"Логин: {user.Login}, Роль: {user.Role}");
                    }
                    else
                    {
                        Console.WriteLine("Пользователь не найден");
                    }
                    break;
                case "2":
                    Console.Write("Роль: ");
                    string role = Console.ReadLine();
                    List<User> usersByRole = users.FindAll(u => u.Role == role);
                    if (usersByRole.Count > 0)
                    {
                        Console.WriteLine($"Пользователи с ролью {role}:");
                        foreach (var u in usersByRole)
                        {
                            Console.WriteLine($"Логин: {u.Login}, Роль: {u.Role}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Пользователи с указанной ролью не найдены");
                    }
                    break;
            }

            Console.ReadLine();
        }

        private void ShowPersonnelManagerMenu()
        {
            // Меню менеджера персонала
            // Реализация аналогична меню администратора
        }

        private void ShowWarehouseManagerMenu()
        {
            // Меню склад-менеджера
            // Реализация аналогична меню администратора
        }

        private void ShowAccountantMenu()
        {
            // Меню бухгалтера
            // Реализация аналогична меню администратора
        }

        private void ShowCashierMenu()
        {
            // Меню кассира
            // Реализация аналогична меню администратора
        }

        private void SaveData()
        {
            string usersJson = JsonConvert.SerializeObject(users);
            File.WriteAllText("users.json", usersJson);

            string employeesJson = JsonConvert.SerializeObject(employees);
            File.WriteAllText("employees.json", employeesJson);

            string productsJson = JsonConvert.SerializeObject(products);
            File.WriteAllText("products.json", productsJson);
        }

        private void LoadData()
        {
            if (File.Exists("users.json"))
            {
                string usersJson = File.ReadAllText("users.json");
                users = JsonConvert.DeserializeObject<List<User>>(usersJson);
            }

            if (File.Exists("employees.json"))
            {
                string employeesJson = File.ReadAllText("employees.json");
                employees = JsonConvert.DeserializeObject<List<Employee>>(employeesJson);
            }

            if (File.Exists("products.json"))
            {
                string productsJson = File.ReadAllText("products.json");
                products = JsonConvert.DeserializeObject<List<Product>>(productsJson);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            InformationSystem informationSystem = new InformationSystem();
            informationSystem.Start();
        }
    }
}
