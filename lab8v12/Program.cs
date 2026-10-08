using System;
using System.Collections.Generic;

namespace Lab8Variant12
{
    #region Ієрархія класів

    /// <summary>
    /// Базовий клас для медіа-файлів.
    /// </summary>
    public class MediaFile
    {
        public string FileName { get; set; }

        public MediaFile(string fileName)
        {
            FileName = string.IsNullOrWhiteSpace(fileName) ? "Untitled" : fileName;
        }

        /// <summary>
        /// Віртуальний метод для відкриття файлу.
        /// </summary>
        public virtual void Open()
        {
            Console.WriteLine($"[MediaFile] Відкриття файлу: {FileName}");
        }
    }

    /// <summary>
    /// Похідний клас для аудіофайлів.
    /// </summary>
    public class AudioFile : MediaFile
    {
        public int BitRate { get; set; } // кбіт/с

        public AudioFile(string fileName, int bitRate) : base(fileName)
        {
            BitRate = bitRate;
        }

        public override void Open()
        {
            Console.WriteLine($"[AudioFile] Відтворення аудіо '{FileName}' (Бітрейт: {BitRate} kbps)...");
        }
    }

    /// <summary>
    /// Похідний клас для відеофайлів.
    /// </summary>
    public class VideoFile : MediaFile
    {
        public string Resolution { get; set; } // Наприклад: 1920x1080

        public VideoFile(string fileName, string resolution) : base(fileName)
        {
            Resolution = resolution;
        }

        public override void Open()
        {
            Console.WriteLine($"[VideoFile] Програвання відео '{FileName}' (Роздільна здатність: {Resolution})...");
        }
    }

    /// <summary>
    /// Похідний клас для зображень.
    /// </summary>
    public class ImageFile : MediaFile
    {
        public string Dimensions { get; set; } // Наприклад: 3840x2160

        public ImageFile(string fileName, string dimensions) : base(fileName)
        {
            Dimensions = dimensions;
        }

        public override void Open()
        {
            Console.WriteLine($"[ImageFile] Відображення зображення '{FileName}' (Розмірність: {Dimensions})...");
        }
    }

    #endregion

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=======================================================================");
            Console.WriteLine("  ЛАБОРАТОРНА РОБОТА №8: ПОЛІМОРФІЗМ ТА ДИНАМІЧНЕ ЗВ'ЯЗУВАННЯ (В-12)   ");
            Console.WriteLine("=======================================================================\n");

            // 1. Створення колекції базового типу MediaFile
            List<MediaFile> playlist = new List<MediaFile>
            {
                new AudioFile("song_track_1.mp3", 320),
                new VideoFile("movie_presentation.mp4", "1920x1080"),
                new ImageFile("vacation_photo.jpg", "4000x3000"),
                new AudioFile("podcast_ep12.flac", 1411),
                new VideoFile("tutorial_lab8.mkv", "2560x1440")
            };

            // Список для агрегації назв успішно відкритих файлів
            List<string> openedFilesLog = new List<string>();

            // 2. Демонстрація поліморфізму через динамічне зв'язування
            Console.WriteLine("--- 1. Поліморфний виклик методу Open() для кожного об'єкта ---");
            foreach (var file in playlist)
            {
                // Завдяки поліморфізму викликається override-метод фактичного типу об'єкта
                file.Open();

                // Агрегація: зберігаємо результат роботи (назву файлу)
                openedFilesLog.Add(file.FileName);
            }

            // 3. Агрегація результатів
            Console.WriteLine("\n--- 2. Агрегація результатів поліморфних викликів ---");
            Console.WriteLine($"Всього відкрито медіа-файлів: {openedFilesLog.Count}");
            Console.WriteLine("Список оброблених файлів:");
            for (int i = 0; i < openedFilesLog.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {openedFilesLog[i]}");
            }

            Console.WriteLine("\n=======================================================================");
        }
    }
}