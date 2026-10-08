using System;
using System.Collections.Generic;

namespace Lab6Variant12
{
    #region Базовий та Похідні Класи

    public class Food
    {
        private string _name;
        private int _calories;

        public string Name
        {
            get => _name;
            set => _name = !string.IsNullOrWhiteSpace(value) ? value : "Невідома їжа";
        }

        public int Calories
        {
            get => _calories;
            set => _calories = value >= 0 ? value : 0;
        }

        public Food(string name, int calories)
        {
            Name = name;
            Calories = calories;
        }

        public virtual void Eat()
        {
            Console.WriteLine($"[Food] Ви споживаєте {Name} ({Calories} ккал).");
        }

        public string GetFoodType()
        {
            return "Загальний продукт харчування (Food)";
        }
    }

    public class Fruit : Food
    {
        public int SweetnessLevel { get; set; }

        public Fruit(string name, int calories, int sweetnessLevel) 
            : base(name, calories)
        {
            SweetnessLevel = sweetnessLevel;
        }

        public override void Eat()
        {
            Console.WriteLine($"[Fruit] Ви їсте соковитий фрукт '{Name}' (Солодощі: {SweetnessLevel}/10, Калорії: {Calories}).");
        }

        public void Peel()
        {
            Console.WriteLine($"[Fruit] Фрукт '{Name}' успішно очищено від шкірки.");
        }

        public new string GetFoodType()
        {
            return "Фрукт (Fruit)";
        }
    }

    public class Vegetable : Food
    {
        public bool IsLeafy { get; set; }

        public Vegetable(string name, int calories, bool isLeafy) 
            : base(name, calories)
        {
            IsLeafy = isLeafy;
        }

        public override void Eat()
        {
            string type = IsLeafy ? "листовий" : "коренеплід/звичайний";
            Console.WriteLine($"[Vegetable] Ви споживаєте {type} овоч '{Name}' ({Calories} ккал).");
        }

        public void Chop()
        {
            Console.WriteLine($"[Vegetable] Овоч '{Name}' нарізано для салату.");
        }
    }

    #endregion

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("===================================================================");
            Console.WriteLine("      ЛАБОРАТОРНА РОБОТА №6: НАСЛІДУВАННЯ ТА ПОЛІМОРФІЗМ (В-12)    ");
            Console.WriteLine("===================================================================\n");

            // 1. Створення об'єктів
            Food genericFood = new Food("Хліб", 265);
            Fruit apple = new Fruit("Яблуко", 52, 8);
            Vegetable carrot = new Vegetable("Морква", 41, false);

            // 2. Виклик унікальних методів
            Console.WriteLine("--- 1. Виклик власних методів похідних класів ---");
            apple.Peel();
            carrot.Chop();
            Console.WriteLine();

            // 3. Демонстрація ПОЛІМОРФІЗМУ (override)
            Console.WriteLine("--- 2. Демонстрація поліморфізму (через масив базового типу Food) ---");
            List<Food> menu = new List<Food> { genericFood, apple, carrot };

            foreach (var item in menu)
            {
                item.Eat();
            }
            Console.WriteLine();

            // 4. Демонстрація різниці між OVERRIDE та NEW (Приховування)
            Console.WriteLine("--- 3. Демонстрація різниці між override та new ---");

            Fruit fruitRef = apple;       // Посилання типу Fruit
            Food foodRef = apple;         // Посилання типу Food (Базовий)

            Console.WriteLine("[A] Віртуальний метод Eat() (використовує override):");
            Console.Write("   - Через посилання Fruit: ");
            fruitRef.Eat();
            Console.Write("   - Через посилання Food:  ");
            foodRef.Eat();
            Console.WriteLine("   -> Результат ОДНАКОВИЙ, бо метод перевизначений у таблиці віртуальних методів (VMT).\n");

            Console.WriteLine("[B] Невіртуальний метод GetFoodType() (використовує new):");
            Console.WriteLine($"   - Через посилання Fruit: {fruitRef.GetFoodType()}");
            Console.WriteLine($"   - Через посилання Food:  {foodRef.GetFoodType()}");
            Console.WriteLine("   -> Результат РІЗНИЙ, бо виклик прихованого через 'new' методу залежить від ТИПУ ПОСИЛАННЯ, а не від типу об'єкта!");

            Console.WriteLine("\n===================================================================");
        }
    }
}