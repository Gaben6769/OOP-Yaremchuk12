using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab5Variant12
{
    /// <summary>
    /// Клас для представлення спортивного або ігрового табло.
    /// </summary>
    public class Scoreboard : IEquatable<Scoreboard>
    {
        private readonly Dictionary<string, int> _scores;

        public Scoreboard()
        {
            _scores = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

        public Scoreboard(IDictionary<string, int> initialScores) : this()
        {
            if (initialScores != null)
            {
                foreach (var kvp in initialScores)
                {
                    _scores[kvp.Key] = kvp.Value;
                }
            }
        }

        /// <summary>
        /// Кількість гравців у табло.
        /// </summary>
        public int Count => _scores.Count;

        /// <summary>
        /// Індексатор для доступу до балів гравця за його ім'ям.
        /// </summary>
        public int this[string playerName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(playerName))
                    throw new ArgumentException("Ім'я гравця не може бути порожнім.", nameof(playerName));

                return _scores.TryGetValue(playerName, out int score) ? score : 0;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(playerName))
                    throw new ArgumentException("Ім'я гравця не може бути порожнім.", nameof(playerName));

                _scores[playerName] = value;
            }
        }

        /// <summary>
        /// Повертає загальну суму балів усіх гравців.
        /// </summary>
        public int TotalScore()
        {
            return _scores.Values.Sum();
        }

        #region Перевантаження операторів

        /// <summary>
        /// Оператор + : Об'єднання двох табло. Повертає нове табло.
        /// Якщо гравець є в обох табло, його бали додаються.
        /// </summary>
        public static Scoreboard operator +(Scoreboard a, Scoreboard b)
        {
            var result = new Scoreboard();

            if (a != null)
            {
                foreach (var kvp in a._scores)
                {
                    result[kvp.Key] += kvp.Value;
                }
            }

            if (b != null)
            {
                foreach (var kvp in b._scores)
                {
                    result[kvp.Key] += kvp.Value;
                }
            }

            return result;
        }

        /// <summary>
        /// Оператор > : Порівняння за загальною сумою очок.
        /// </summary>
        public static bool operator >(Scoreboard a, Scoreboard b)
        {
            int scoreA = a?.TotalScore() ?? 0;
            int scoreB = b?.TotalScore() ?? 0;
            return scoreA > scoreB;
        }

        /// <summary>
        /// Оператор < : Порівняння за загальною сумою очок.
        /// </summary>
        public static bool operator <(Scoreboard a, Scoreboard b)
        {
            int scoreA = a?.TotalScore() ?? 0;
            int scoreB = b?.TotalScore() ?? 0;
            return scoreA < scoreB;
        }

        public static bool operator ==(Scoreboard a, Scoreboard b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Scoreboard a, Scoreboard b)
        {
            return !(a == b);
        }

        #endregion

        #region Перевизначення базових методів

        public override bool Equals(object obj)
        {
            return Equals(obj as Scoreboard);
        }

        public bool Equals(Scoreboard other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (_scores.Count != other._scores.Count) return false;

            foreach (var pair in _scores)
            {
                if (!other._scores.TryGetValue(pair.Key, out int otherValue) || pair.Value != otherValue)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var pair in _scores.OrderBy(p => p.Key, StringComparer.OrdinalIgnoreCase))
            {
                unchecked
                {
                    hash = hash * 31 + StringComparer.OrdinalIgnoreCase.GetHashCode(pair.Key);
                    hash = hash * 31 + pair.Value.GetHashCode();
                }
            }
            return hash;
        }

        public override string ToString()
        {
            if (_scores.Count == 0)
                return "Scoreboard [Порожньо]";

            var entries = _scores.Select(kvp => $"{kvp.Key}: {kvp.Value}");
            return $"Scoreboard [Загалом очок: {TotalScore()}] ({string.Join(", ", entries)})";
        }

        #endregion
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ДЕМОНСТРАЦІЯ РОБОТИ КЛАСУ SCOREBOARD (ВАРАНТ 12) ===\n");

            // 1. Створення об'єктів
            var teamA = new Scoreboard();
            var teamB = new Scoreboard();

            // 2. Використання індексатора (Запис)
            Console.WriteLine("--- 1. Додавання даних через індексатор ---");
            teamA["Олексій"] = 15;
            teamA["Марія"] = 20;
            teamA["Іван"] = 10;

            teamB["Марія"] = 5;  // Перекривається з teamA при додаванні
            teamB["Дмитро"] = 30;

            // 3. Читання через індексатор та ToString()
            Console.WriteLine($"Команда А: {teamA}");
            Console.WriteLine($"Команда Б: {teamB}");
            Console.WriteLine($"Очки Марії в Команді А (через індексатор): {teamA["Марія"]}");
            Console.WriteLine($"Очки неіснуючого гравця 'Олена': {teamA["Олена"]}\n");

            // 4. Демонстрація оператора + (Об'єднання)
            Console.WriteLine("--- 2. Об'єднання табло (Оператор +) ---");
            Scoreboard totalBoard = teamA + teamB;
            Console.WriteLine($"Об'єднане табло (teamA + teamB): {totalBoard}");
            Console.WriteLine($"Загальна сума балів: {totalBoard.TotalScore()}\n");

            // 5. Демонстрація операторів порівняння (> та <)
            Console.WriteLine("--- 3. Порівняння за сумою очок (Оператори > та <) ---");
            Console.WriteLine($"Суммарні очки TeamA ({teamA.TotalScore()}) > TeamB ({teamB.TotalScore()}): {teamA > teamB}");
            Console.WriteLine($"Суммарні очки TeamA ({teamA.TotalScore()}) < TeamB ({teamB.TotalScore()}): {teamA < teamB}\n");

            // 6. Перевірка ==, != та Equals
            Console.WriteLine("--- 4. Перевірка рівності (==, !=, Equals) ---");
            var teamACopy = new Scoreboard();
            teamACopy["Олексій"] = 15;
            teamACopy["Марія"] = 20;
            teamACopy["Іван"] = 10;

            Console.WriteLine($"teamA == teamACopy: {teamA == teamACopy}");
            Console.WriteLine($"teamA == teamB: {teamA == teamB}");
            Console.WriteLine($"teamA.Equals(teamACopy): {teamA.Equals(teamACopy)}");
        }
    }
}