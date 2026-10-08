using System;
using System.Collections.Generic;

namespace Lab9Variant12
{
    #region Винятки

    /// <summary>
    /// Власний виняток для помилок валідації даних звіту.
    /// </summary>
    public class InvalidReportDataException : Exception
    {
        public InvalidReportDataException(string message) : base(message) { }
    }

    #endregion

    #region Ієрархія генераторів звітів

    /// <summary>
    /// Абстрактний базовий клас для генерації звітів.
    /// </summary>
    public abstract class ReportGenerator
    {
        public string ReportTitle { get; set; }

        protected ReportGenerator(string reportTitle)
        {
            if (string.IsNullOrWhiteSpace(reportTitle))
            {
                throw new ArgumentException("Назва звіту не може бути порожньою.", nameof(reportTitle));
            }
            ReportTitle = reportTitle;
        }

        /// <summary>
        /// Абстрактний метод для генерації звіту.
        /// </summary>
        /// <param name="data">Дані для звіту</param>
        public abstract void Generate(string data);

        /// <summary>
        /// Базова валідація даних для всіх типів звітів.
        /// </summary>
        protected void ValidateData(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new InvalidReportDataException($"[{ReportTitle}] Помилка: Вхідні дані для генерації звіту відсутні або порожні!");
            }
        }
    }

    /// <summary>
    /// Генератор PDF-звітів.
    /// </summary>
    public class PDFReport : ReportGenerator
    {
        public string PageSize { get; set; }

        public PDFReport(string reportTitle, string pageSize = "A4") : base(reportTitle)
        {
            PageSize = pageSize;
        }

        public override void Generate(string data)
        {
            ValidateData(data);
            Console.WriteLine($"[PDF] Успішно згенеровано PDF-звіт '{ReportTitle}' (Формат: {PageSize}). Дані: \"{data}\"");
        }
    }

    /// <summary>
    /// Генератор Excel-звітів.
    /// </summary>
    public class ExcelReport : ReportGenerator
    {
        public string SheetName { get; set; }

        public ExcelReport(string reportTitle, string sheetName = "Sheet1") : base(reportTitle)
        {
            SheetName = sheetName;
        }

        public override void Generate(string data)
        {
            ValidateData(data);

            // Специфічна перевірка для Excel
            if (data.Length < 5)
            {
                throw new InvalidReportDataException($"[{ReportTitle}] Помилка Excel: Обсяг даних занадто малий (мінімум 5 символів)!");
            }

            Console.WriteLine($"[Excel] Успішно згенеровано Excel-таблицю '{ReportTitle}' (Аркуш: {SheetName}). Дані: \"{data}\"");
        }
    }

    /// <summary>
    /// Генератор HTML-звітів.
    /// </summary>
    public class HTMLReport : ReportGenerator
    {
        public bool IsResponsive { get; set; }

        public HTMLReport(string reportTitle, bool isResponsive = true) : base(reportTitle)
        {
            IsResponsive = isResponsive;
        }

        public override void Generate(string data)
        {
            ValidateData(data);
            Console.WriteLine($"[HTML] Успішно згенеровано HTML-сторінку '{ReportTitle}' (Адаптивна: {IsResponsive}). Дані: \"{data}\"");
        }
    }

    #endregion

    #region Сервісний клас

    /// <summary>
    /// Сервіс для поліморфної обробки та генерації групи звітів.
    /// </summary>
    public class ReportService
    {
        /// <summary>
        /// Поліморфний метод для генерації всіх звітів зі списку із обробкою винятків.
        /// </summary>
        public void GenerateAll(List<ReportGenerator> reports, string data)
        {
            Console.WriteLine($"--- Старт генерації звітів (Всього у черзі: {reports.Count}) ---\n");

            int successCount = 0;
            int errorCount = 0;

            foreach (var report in reports)
            {
                try
                {
                    // Поліморфний виклик абстрактного методу
                    report.Generate(data);
                    successCount++;
                }
                catch (InvalidReportDataException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ПОМИЛКА ВАЛІДАЦІЇ] {ex.Message}");
                    Console.ResetColor();
                    errorCount++;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"[СИСТЕМНА ПОМИЛКА] Несподіваний збій під час генерації звіту '{report.ReportTitle}': {ex.Message}");
                    Console.ResetColor();
                    errorCount++;
                }
            }

            Console.WriteLine($"\n--- Підсумок виконання ---");
            Console.WriteLine($"Успішно згенеровано: {successCount}");
            Console.WriteLine($"Помилок генерації:   {errorCount}");
        }
    }

    #endregion

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=======================================================================");
            Console.WriteLine("    ЛАБОРАТОРНА РОБОТА №9: ПОЛІМОРФНІ СЕРВІСИ ТА ОБРОБКА ПОМИЛОК (В-12) ");
            Console.WriteLine("=======================================================================\n");

            ReportService reportService = new ReportService();

            // 1. Демонстрація УСПІШНОЇ генерації всіх типів звітів
            Console.WriteLine("=======================================================================");
            Console.WriteLine("  СЦЕНАРІЙ 1: Генерація звітів із коректними вхідними даними");
            Console.WriteLine("=======================================================================");

            List<ReportGenerator> validReports = new List<ReportGenerator>
            {
                new PDFReport("Річний фінансовий звіт", "A4"),
                new ExcelReport("Продажі за Q3", "ДаніПродажів"),
                new HTMLReport("Веб-аналітика відвідуваності", true)
            };

            reportService.GenerateAll(validReports, "Продажі: +25%, Прибуток: $150,000");

            Console.WriteLine("\n");

            // 2. Демонстрація ОБРОБКИ ПОМИЛОК (навмисний виклик винятків)
            Console.WriteLine("=======================================================================");
            Console.WriteLine("  СЦЕНАРІЙ 2: Демонстрація валідації та обробки помилок");
            Console.WriteLine("=======================================================================");

            List<ReportGenerator> mixedReports = new List<ReportGenerator>
            {
                new PDFReport("Звіт з кадрового обліку", "A3"),
                new ExcelReport("Короткий звіт", "Sheet1"), // Викличе помилку Excel (дані < 5 символів)
                new HTMLReport("Системний лог", false)
            };

            // Передаємо занадто короткі/невалідні дані
            reportService.GenerateAll(mixedReports, "123");

            Console.WriteLine("\n");

            // 3. Демонстрація з порожніми даними (порожній рядок)
            Console.WriteLine("=======================================================================");
            Console.WriteLine("  СЦЕНАРІЙ 3: Генерація з порожніми даними (null / порожній рядок)");
            Console.WriteLine("=======================================================================");

            List<ReportGenerator> emptyDataReports = new List<ReportGenerator>
            {
                new PDFReport("Аудит безпеки"),
                new HTMLReport("Статус серверів")
            };

            reportService.GenerateAll(emptyDataReports, "");

            Console.WriteLine("\n=======================================================================");
        }
    }
}