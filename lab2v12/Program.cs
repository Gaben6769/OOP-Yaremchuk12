using System;

namespace OOP_Lab2
{
    // Клас Song
    public class Song
    {
        // 1. Приватні поля
        private string _title;
        private string _artist;
        private int _durationSeconds;

        // 2. Публічні властивості
        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public string Artist
        {
            get => _artist;
            set => _artist = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        // Властивість з валідацією (тривалість > 0)
        public int DurationSeconds
        {
            get => _durationSeconds;
            set
            {
                if (value > 0)
                {
                    _durationSeconds = value;
                }
                else
                {
                    Console.WriteLine($"[Помилка]: Тривалість пісні '{Title}' має бути більшою за 0! Встановлено значення за замовчуванням (180 сек).");
                    _durationSeconds = 180;
                }
            }
        }

        // 3. Перевантажені конструктори

        // Конструктор за замовчуванням: використовує : this(...) для ланцюгового виклику
        public Song() : this("Unknown", "Unknown", 180)
        {
            Console.WriteLine("[Конструктор]: Створено об'єкт за замовчуванням (через : this()).");
        }

        // Параметризований конструктор
        public Song(string title, string artist, int durationSeconds)
        {
            Title = title;
            Artist = artist;
            DurationSeconds = durationSeconds; // Задіюємо валідацію через set

            Console.WriteLine($"[Конструктор]: Створено пісню \"{Title}\" - {Artist}.");
        }

        // 4. Метод класу
        public void Play()
        {
            int minutes = _durationSeconds / 60;
            int seconds = _durationSeconds % 60;
            Console.WriteLine($"▶ Зараз грає: \"{Title}\" — {Artist} [{minutes:D2}:{seconds:D2}]");
        }

        // 5. Деструктор (Фіналізатор)
        ~Song()
        {
            Console.WriteLine($"[Деструктор]: Об'єкт пісні \"{_title}\" знищено з пам'яті.");
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("=== Creating objects ===");

            // Об'єкт 1: За замовчуванням (викликає ланцюговий конструктор : this())
            Song song1 = new Song();

            // Об'єкт 2: З повними коректними параметрами
            Song song2 = new Song("Bohemian Rhapsody", "Queen", 354);

            // Об'єкт 3: Перевірка валідації (передаємо від'ємну тривалість)
            Song song3 = new Song("Bad Duration Song", "Test Artist", -50);

            Console.WriteLine("\n=== Objects created successfully ===\n");

            // Виклик методів класу
            song1.Play();
            song2.Play();
            song3.Play();

            Console.WriteLine("\n=== End of Main, preparing for GC ===");

            // Обнуляємо посилання, щоб об'єкти стали доступними для Garbage Collector
            song1 = null;
            song2 = null;
            song3 = null;

            // Примусовий виклик збирача сміття для демонстрації роботи деструктора
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("=== Program finished ===");
        }
    }
}