using System;

namespace OOPLab4
{
    public class Weight
    {
        // Приватні поля
        private double _value;
        private string _unit;

        // Статичний член
        public static double KilogramToPoundRatio { get; } = 2.20462;

        // Властивості з валідацією
        public double Value
        {
            get => _value;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Маса повинна бути більшою за 0.");
                }
                _value = value;
            }
        }

        public string Unit
        {
            get => _unit;
            set
            {
                string formattedUnit = value?.Trim().ToLower();
                if (formattedUnit != "kg" && formattedUnit != "lbs")
                {
                    throw new ArgumentException("Допустимі одиниці виміру: 'kg' або 'lbs'.");
                }
                _unit = formattedUnit;
            }
        }

        // Конструктор
        public Weight(double value, string unit)
        {
            Unit = unit;   // Валідація одиниці виміру
            Value = value; // Валідація значення
        }

        // Індексатор для доступу до даних за ключем
        public object this[string key]
        {
            get
            {
                return key?.ToLower() switch
                {
                    "value" or "маса" => Value,
                    "unit" or "одиниця" => Unit,
                    _ => throw new KeyNotFoundException($"Ключ '{key}' не підтримується.")
                };
            }
        }

        // Перевантаження оператора додавання '+'
        public static Weight operator +(Weight w1, Weight w2)
        {
            if (w1 is null || w2 is null)
            {
                throw new ArgumentNullException("Об'єкти маси не можуть бути null.");
            }

            if (!string.Equals(w1.Unit, w2.Unit, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Неможливо додати маси з різними одиницями виміру ({w1.Unit} та {w2.Unit}). Спочатку приведіть їх до однієї системи.");
            }

            return new Weight(w1.Value + w2.Value, w1.Unit);
        }

        // Перевантаження операторів порівняння '==' та '!='
        public static bool operator ==(Weight w1, Weight w2)
        {
            if (ReferenceEquals(w1, w2)) return true;
            if (w1 is null || w2 is null) return false;
            return w1.Equals(w2);
        }

        public static bool operator !=(Weight w1, Weight w2)
        {
            return !(w1 == w2);
        }

        // Перевизначення методу Equals
        public override bool Equals(object obj)
        {
            if (obj is Weight other)
            {
                return Math.Abs(this.Value - other.Value) < 0.0001 &&
                       string.Equals(this.Unit, other.Unit, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        // Перевизначення методу GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(Math.Round(Value, 4), Unit.ToLower());
        }

        // Перевизначення методу ToString
        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота №4 (Варіант 12: Клас Weight) ===\n");

            // 1. Демонстрація створення об'єктів та роботи статичного члена
            Console.WriteLine($"[Статичний член] Коефіцієнт kg -> lbs: {Weight.KilogramToPoundRatio}");

            Weight w1 = new Weight(10.5, "kg");
            Weight w2 = new Weight(5.5, "kg");
            Weight w3 = new Weight(10.5, "kg");
            Weight w4 = new Weight(20.0, "lbs");

            Console.WriteLine($"\nСтворені об'єкти:");
            Console.WriteLine($"w1: {w1}");
            Console.WriteLine($"w2: {w2}");
            Console.WriteLine($"w3: {w3}");
            Console.WriteLine($"w4: {w4}");

            // 2. Демонстрація перевантаження оператора додавання '+'
            Console.WriteLine("\n--- Перевантаження оператора '+' ---");
            Weight sum = w1 + w2;
            Console.WriteLine($"{w1} + {w2} = {sum}");

            // 3. Демонстрація порівняння (==, !=, Equals)
            Console.WriteLine("\n--- Перевантаження операторів '==' та '!=' ---");
            Console.WriteLine($"w1 == w3 (10.5 kg == 10.5 kg): {w1 == w3}");
            Console.WriteLine($"w1 == w2 (10.5 kg == 5.5 kg): {w1 == w2}");
            Console.WriteLine($"w1 != w4 (10.5 kg != 20.0 lbs): {w1 != w4}");

            // 4. Демонстрація роботи індексатора
            Console.WriteLine("\n--- Використання індексатора ---");
            Console.WriteLine($"w1[\"value\"]: {w1["value"]}");
            Console.WriteLine($"w1[\"unit\"]: {w1["unit"]}");

            // 5. Демонстрація валідації
            Console.WriteLine("\n--- Перевірка валідації даних ---");

            try
            {
                Console.WriteLine("Спроба встановити від'ємне значення маси (-5 kg)...");
                Weight invalidWeight = new Weight(-5, "kg");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            try
            {
                Console.WriteLine("Спроба встановити недопустиму одиницю виміру (10 grams)...");
                Weight invalidUnit = new Weight(10, "grams");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            try
            {
                Console.WriteLine("Спроба додати маси у різних одиницях (10.5 kg + 20.0 lbs)...");
                Weight invalidSum = w1 + w4;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            Console.WriteLine("\nДемонстрацію завершено успішно.");
        }
    }
}