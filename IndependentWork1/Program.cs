using System;

namespace IndependentWork1
{
    // ==========================================
    // 1. Клас Банківський Рахунок (BankAccount)
    // ==========================================
    public class BankAccount
    {
        // Приватні поля
        private string _accountNumber;
        private string _ownerName;
        private decimal _balance;

        // Властивість тільки для читання (Read-only)
        public string AccountNumber => _accountNumber;

        // Звичайна властивість для читання/запису
        public string OwnerName
        {
            get => _ownerName;
            set => _ownerName = value;
        }

        // Властивість для перевірки балансу
        public decimal Balance => _balance;

        // Конструктор
        public BankAccount(string accountNumber, string ownerName, decimal initialBalance)
        {
            _accountNumber = accountNumber;
            _ownerName = ownerName;
            _balance = initialBalance >= 0 ? initialBalance : 0;
        }

        // Метод з обчисленням та зміною стану (поповнення)
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                Console.WriteLine($"[BankAccount] Рахунок {AccountNumber} поповнено на {amount:C}. Поточний баланс: {_balance:C}");
            }
            else
            {
                Console.WriteLine("[BankAccount] Сума поповнення має бути більшою за 0.");
            }
        }

        // Метод перевірки стану (зняття коштів)
        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && _balance >= amount)
            {
                _balance -= amount;
                Console.WriteLine($"[BankAccount] Успішно знято {amount:C}. Залишок: {_balance:C}");
                return true;
            }
            
            Console.WriteLine($"[BankAccount] Недостатньо коштів або невірна сума ({amount:C}) на рахунку {AccountNumber}.");
            return false;
        }
    }

    // ==========================================
    // 2. Клас Студент (Student)
    // ==========================================
    public class Student
    {
        // Приватні поля
        private string _fullName;
        private string _group;
        private double _averageGrade;

        // Властивість тільки для читання
        public string FullName => _fullName;

        // Властивості з get та set
        public string Group
        {
            get => _group;
            set => _group = value;
        }

        public double AverageGrade
        {
            get => _averageGrade;
            set
            {
                if (value >= 0 && value <= 100)
                    _averageGrade = value;
            }
        }

        // Конструктор
        public Student(string fullName, string group, double averageGrade)
        {
            _fullName = fullName;
            _group = group;
            AverageGrade = averageGrade;
        }

        // Метод перевірки стану (чи претендує на підвищену стипендію)
        public bool IsEligibleForScholarship()
        {
            return _averageGrade >= 85.0;
        }

        // Метод виводу інформації
        public void PrintStudentCard()
        {
            string status = IsEligibleForScholarship() ? "Претендує на стипендію" : "Звичайна успішність";
            Console.WriteLine($"[Student] Студент: {FullName} | Група: {Group} | Середній бал: {AverageGrade:F1} | Стан: {status}");
        }
    }

    // ==========================================
    // 3. Клас Автомобіль (Car)
    // ==========================================
    public class Car
    {
        // Приватні поля
        private string _brand;
        private double _fuelTankCapacity; // л
        private double _fuelConsumptionPer100km; // л/100км
        private double _currentFuelLevel; // л

        // Властивість тільки для читання
        public string Brand => _brand;

        // Властивість для читання/запису
        public double CurrentFuelLevel
        {
            get => _currentFuelLevel;
            set
            {
                if (value >= 0 && value <= _fuelTankCapacity)
                    _currentFuelLevel = value;
            }
        }

        // Конструктор
        public Car(string brand, double fuelTankCapacity, double fuelConsumptionPer100km, double initialFuel)
        {
            _brand = brand;
            _fuelTankCapacity = fuelTankCapacity;
            _fuelConsumptionPer100km = fuelConsumptionPer100km;
            _currentFuelLevel = initialFuel <= fuelTankCapacity ? initialFuel : fuelTankCapacity;
        }

        // Метод з обчисленням (розрахунок залишкового запасу ходу у км)
        public double CalculateRange()
        {
            if (_fuelConsumptionPer100km <= 0) return 0;
            return (_currentFuelLevel / _fuelConsumptionPer100km) * 100;
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

            Console.WriteLine("=== Самостійна робота №1: Базовий синтаксис C# і оголошення класів ===\n");

            // 1. Демонстрація класу BankAccount
            Console.WriteLine("--- 1. Робота з класом BankAccount ---");
            BankAccount account = new BankAccount("UA1234567890", "Олександр", 1500.00m);
            Console.WriteLine($"Створено рахунок {account.AccountNumber} на ім'я {account.OwnerName}. Початковий баланс: {account.Balance:C}");
            
            account.Deposit(500.00m);
            account.Withdraw(800.00m);
            account.Withdraw(2000.00m); // Спроба зняти більше, ніж є
            Console.WriteLine();

            // 2. Демонстрація класу Student
            Console.WriteLine("--- 2. Робота з класом Student ---");
            Student student1 = new Student("Іван Петренко", "ПЗ-21", 88.5);
            Student student2 = new Student("Марія Коваль", "ПЗ-21", 74.0);

            student1.PrintStudentCard();
            student2.PrintStudentCard();
            Console.WriteLine();

            // 3. Демонстрація класу Car
            Console.WriteLine("--- 3. Робота з класом Car ---");
            Car myCar = new Car("Skoda Octavia", 50.0, 6.5, 30.0);
            double estimatedRange = myCar.CalculateRange();

            Console.WriteLine($"Автомобіль: {myCar.Brand}");
            Console.WriteLine($"Поточний рівень пального: {myCar.CurrentFuelLevel} л");
            Console.WriteLine($"Залишковий запас ходу: {estimatedRange:F1} км");

            Console.WriteLine("\nДемонстрацію роботи класів завершено успішно.");
        }
    }
}