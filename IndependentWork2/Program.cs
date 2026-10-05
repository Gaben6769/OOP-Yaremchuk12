using System;

namespace IndependentWork2
{
    // ==========================================
    // Клас Товар (Product)
    // ==========================================
    public class Product
    {
        // Приватні поля
        private readonly int _id;
        private readonly string _name;
        private readonly decimal _price;
        private readonly string _category;
        private readonly int _stockCount;

        // Публічні властивості (Read-Only)
        public int Id => _id;
        public string Name => _name;
        public decimal Price => _price;
        public string Category => _category;
        public int StockCount => _stockCount;

        // 1. Основний конструктор (ініціалізує всі 5 полів)
        public Product(int id, string name, decimal price, string category, int stockCount)
        {
            _id = id;
            _name = name;
            _price = price;
            _category = category;
            _stockCount = stockCount;
        }

        // 2. Скорочений конструктор (для швидкого додавання)
        // Викликає основний конструктор через : this(...)
        public Product(int id, string name, decimal price)
            : this(id, name, price, "Uncategorized", 0)
        {
        }

        // 3. Конструктор копіювання
        // Викликає основний конструктор, передаючи дані з полів об'єкта-джерела
        public Product(Product other)
            : this(
                other != null ? other.Id : 0,
                other != null ? other.Name : "Unknown",
                other != null ? other.Price : 0m,
                other != null ? other.Category : "Uncategorized",
                other != null ? other.StockCount : 0
            )
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other), "Об'єкт для копіювання не може бути null.");
            }
        }

        // Перевизначення методу ToString()
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price:C}, Category: {Category}, Stock: {StockCount}";
        }
    }

    // ==========================================
    // Точка входу в програму (Program)
    // ==========================================
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Самостійна робота №2: Перевантаження конструкторів ===\n");
            Console.WriteLine("Створення товарів:\n");

            // 1. Створення об'єкта через основний конструктор
            Product product1 = new Product(101, "Laptop", 35000.00m, "Electronics", 15);
            Console.WriteLine($"Товар 1 (основний конструктор): {product1}");

            // 2. Створення об'єкта через скорочений конструктор
            Product product2 = new Product(102, "Mouse", 800.00m);
            Console.WriteLine($"Товар 2 (скорочений конструктор): {product2}");

            // 3. Створення об'єкта через конструктор копіювання (дублікат product1)
            Product product3 = new Product(product1);
            Console.WriteLine($"Товар 3 (конструктор копіювання): {product3}");

            Console.WriteLine("\nДемонстрацію перевантаження конструкторів завершено успішно.");
        }
    }
}