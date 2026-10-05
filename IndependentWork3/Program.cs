using System;

namespace IndependentWork3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("===================================================================");
            Console.WriteLine("     САМОСТІЙНА РОБОТА №3: АНАЛІЗ ІНКАПСУЛЯЦІЇ В OPEN-SOURCE     ");
            Console.WriteLine("===================================================================\n");

            Console.WriteLine("Обраний проєкт: Entity Framework Core (EF Core) від Microsoft");
            Console.WriteLine("Репозиторій:    https://github.com/dotnet/efcore\n");

            Console.WriteLine("Проаналізовані класи:");
            Console.WriteLine(" 1. EntityEntry            (src/EFCore/ChangeTracking/EntityEntry.cs)");
            Console.WriteLine(" 2. DbContextOptionsBuilder (src/EFCore/DbContextOptionsBuilder.cs)");
            Console.WriteLine(" 3. PropertyBuilder         (src/EFCore/Metadata/Builders/PropertyBuilder.cs)\n");

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine(" Повний деталізований звіт з аналізом інкапсуляції, валідації,");
            Console.WriteLine(" прикладами коду та відповідями на контрольні запитання міститься");
            Console.WriteLine(" у файлі README.md цього репозиторію.");
            Console.WriteLine("-------------------------------------------------------------------\n");

            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
} 