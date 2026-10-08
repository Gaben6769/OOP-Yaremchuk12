using System;

namespace Lab7Variant12
{
    #region Ієрархія класів

    /// <summary>
    /// Базовий клас користувача.
    /// </summary>
    public class User
    {
        public string Username { get; set; }

        public User(string username)
        {
            Username = username;
        }

        /// <summary>
        /// Віртуальний метод входу в систему.
        /// </summary>
        public virtual void Login()
        {
            Console.WriteLine($"[User] Користувач '{Username}' увійшов як звичайний користувач.");
        }

        public virtual void DisplayRole()
        {
            Console.WriteLine($"[User] Роль: Звичайний користувач.");
        }
    }

    /// <summary>
    /// Похідний клас Admin (Стратегія: OVERRIDE).
    /// </summary>
    public class Admin : User
    {
        public string AdminLevel { get; set; }

        public Admin(string username, string adminLevel) : base(username)
        {
            AdminLevel = adminLevel;
        }

        /// <summary>
        /// Перевизначення методом override (Динамічний поліморфізм).
        /// </summary>
        public override void Login()
        {
            Console.WriteLine($"[Admin (override)] Адміністратор '{Username}' (Рівень: {AdminLevel}) увійшов із повними правами доступу!");
        }

        public void AccessAdminPanel()
        {
            Console.WriteLine($"[Admin] Відкрито панель адміністрування для {Username}.");
        }
    }

    /// <summary>
    /// Похідний клас Guest (Стратегія: NEW).
    /// </summary>
    public class Guest : User
    {
        public int SessionDurationMinutes { get; set; }

        public Guest(string username, int sessionDurationMinutes) : base(username)
        {
            SessionDurationMinutes = sessionDurationMinutes;
        }

        /// <summary>
        /// Приховування методом new (Статичне зв'язування, без поліморфізму).
        /// </summary>
        public new void Login()
        {
            Console.WriteLine($"[Guest (new)] Гостьовий акаунт '{Username}' увійшов у систему (Сесія обмежена: {SessionDurationMinutes} хв).");
        }

        public void ShowGuestDisclaimer()
        {
            Console.WriteLine($"[Guest] Застереження: Гостьовий доступ має обмежені права.");
        }
    }

    #endregion

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=======================================================================");
            Console.WriteLine("  ЛАБОРАТОРНА РОБОТА №7: ПРИХОВУВАННЯ МЕТОДІВ (NEW) VS OVERRIDE (В-12) ");
            Console.WriteLine("=======================================================================\n");

            // 1. Створення об'єктів
            User baseUser = new User("JohnDoe");
            Admin adminUser = new Admin("Alex_Admin", "SuperAdmin");
            Guest guestUser = new Guest("TemporaryGuest", 30);

            // 2. Демонстрація через посилання БАЗОВОГО ТИПУ (Upcasting)
            Console.WriteLine("--- 1. Виклик методів через посилання БАЗОВОГО типу (User) ---");
            User userRef1 = adminUser; // Upcasting Admin -> User
            User userRef2 = guestUser; // Upcasting Guest -> User

            Console.Write("  a) baseUser.Login():     ");
            baseUser.Login();

            Console.Write("  b) userRef1.Login() [Admin]: ");
            userRef1.Login(); // Викличе Admin.Login(), бо використовується OVERRIDE (динамічне зв'язування)

            Console.Write("  c) userRef2.Login() [Guest]: ");
            userRef2.Login(); // Викличе User.Login(), бо використовується NEW (статичне зв'язування)

            Console.WriteLine("\n--> ПОЯСНЕННЯ:");
            Console.WriteLine("    - Для Admin спрацював ПОЛІМОРФІЗМ (override) -> викликано метод Admin.Login().");
            Console.WriteLine("    - Для Guest спрацювало ПРИХОВУВАННЯ (new) -> викликано метод базового класу User.Login()!\n");

            // 3. Демонстрація через посилання ПОХІДНИХ ТИПІВ (з явним приведенням)
            Console.WriteLine("--- 2. Виклик методів через посилання ПОХІДНИХ типів (Downcasting) ---");

            Console.Write("  a) ((Admin)userRef1).Login(): ");
            ((Admin)userRef1).Login();

            Console.Write("  b) ((Guest)userRef2).Login(): ");
            ((Guest)userRef2).Login();

            Console.WriteLine("\n--> ПОЯСНЕННЯ:");
            Console.WriteLine("    - При явному приведенні до типу Guest викликається прихований метод Guest.Login().\n");

            // 4. Прямий виклик через об'єкти
            Console.WriteLine("--- 3. Прямий виклик через змінні відповідних типів ---");
            Console.Write("  - adminUser.Login(): ");
            adminUser.Login();
            Console.Write("  - guestUser.Login(): ");
            guestUser.Login();

            Console.WriteLine("\n=======================================================================");
        }
    }
}