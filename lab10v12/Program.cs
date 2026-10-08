using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab10Variant12
{
    #region Частина 1: Інтерфейс IQueue<T> та його реалізації

    /// <summary>
    /// Інтерфейс для визначення контракту черги (FIFO).
    /// </summary>
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        bool IsEmpty { get; }
    }

    /// <summary>
    /// Реалізація черги на основі списку/масиву.
    /// </summary>
    public class ArrayQueue<T> : IQueue<T>
    {
        private readonly List<T> _items = new List<T>();

        public bool IsEmpty => _items.Count == 0;

        public void Enqueue(T item)
        {
            _items.Add(item);
            Console.WriteLine($"[ArrayQueue] Додано в чергу: {item}");
        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Черга ArrayQueue порожня!");
            }

            T item = _items[0];
            _items.RemoveAt(0);
            Console.WriteLine($"[ArrayQueue] Вилучено з черги: {item}");
            return item;
        }
    }

    /// <summary>
    /// Реалізація черги на основі двозв'язного списку LinkedList.
    /// </summary>
    public class LinkedListQueue<T> : IQueue<T>
    {
        private readonly LinkedList<T> _items = new LinkedList<T>();

        public bool IsEmpty => _items.Count == 0;

        public void Enqueue(T item)
        {
            _items.AddLast(item);
            Console.WriteLine($"[LinkedListQueue] Додано в чергу: {item}");
        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Черга LinkedListQueue порожня!");
            }

            T item = _items.First.Value;
            _items.RemoveFirst();
            Console.WriteLine($"[LinkedListQueue] Вилучено з черги: {item}");
            return item;
        }
    }

    #endregion

    #region Частина 2: Абстрактний клас TaskScheduler та його похідні

    /// <summary>
    /// Абстрактний клас планувальника завдань (шаблонний алгоритм та спільна логіка).
    /// </summary>
    public abstract class TaskScheduler
    {
        public string SchedulerName { get; set; }

        protected TaskScheduler(string schedulerName)
        {
            SchedulerName = schedulerName;
        }

        /// <summary>
        /// Абстрактний метод додавання завдання.
        /// </summary>
        public abstract void ScheduleTask(string taskName);

        /// <summary>
        /// Абстрактний метод виконання наступного завдання.
        /// </summary>
        public abstract void ExecuteNextTask();

        /// <summary>
        /// Конкретний метод спільної логіки (логування завершення завдання).
        /// </summary>
        public void LogTaskCompletion(string taskName)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine($"[{timestamp}] [{SchedulerName}] Завдання '{taskName}' УСПІШНО ВИКОНАНО.");
        }
    }

    /// <summary>
    /// Планувальник FIFO (First-In-First-Out).
    /// </summary>
    public class FifoScheduler : TaskScheduler
    {
        private readonly Queue<string> _tasks = new Queue<string>();

        public FifoScheduler() : base("FIFO Scheduler") { }

        public override void ScheduleTask(string taskName)
        {
            _tasks.Enqueue(taskName);
            Console.WriteLine($"[{SchedulerName}] Заплановано: '{taskName}'");
        }

        public override void ExecuteNextTask()
        {
            if (_tasks.Count == 0)
            {
                Console.WriteLine($"[{SchedulerName}] Немає завдань для виконання.");
                return;
            }

            string currentTask = _tasks.Dequeue();
            Console.WriteLine($"[{SchedulerName}] Виконується: '{currentTask}'...");
            
            // Викликаємо спільний конкретний метод базового класу
            LogTaskCompletion(currentTask);
        }
    }

    /// <summary>
    /// Планувальник за пріоритетом (Priority).
    /// </summary>
    public class PriorityScheduler : TaskScheduler
    {
        private readonly List<string> _priorityTasks = new List<string>();

        public PriorityScheduler() : base("Priority Scheduler") { }

        public override void ScheduleTask(string taskName)
        {
            // Умовна логіка пріоритету: якщо містить "CRITICAL" або "HIGH", ставимо на початок
            if (taskName.Contains("CRITICAL") || taskName.Contains("HIGH"))
            {
                _priorityTasks.Insert(0, taskName);
                Console.WriteLine($"[{SchedulerName}] [ВИСОКИЙ ПРІОРИТЕТ] Заплановано на початок: '{taskName}'");
            }
            else
            {
                _priorityTasks.Add(taskName);
                Console.WriteLine($"[{SchedulerName}] [ЗВИЧАЙНИЙ ПРІОРИТЕТ] Заплановано: '{taskName}'");
            }
        }

        public override void ExecuteNextTask()
        {
            if (_priorityTasks.Count == 0)
            {
                Console.WriteLine($"[{SchedulerName}] Немає завдань для виконання.");
                return;
            }

            string currentTask = _priorityTasks[0];
            _priorityTasks.RemoveAt(0);
            Console.WriteLine($"[{SchedulerName}] Виконується найпріоритетніше: '{currentTask}'...");

            // Викликаємо спільний конкретний метод базового класу
            LogTaskCompletion(currentTask);
        }
    }

    #endregion

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=======================================================================");
            Console.WriteLine("  ЛАБОРАТОРНА РОБОТА №10: АБСТРАКТНІ КЛАСИ ТА ІНТЕРФЕЙСИ (В-12) ");
            Console.WriteLine("=======================================================================\n");

            // -------------------------------------------------------------------
            // ДЕМОНСТРАЦІЯ 1: Робота з інтерфейсом IQueue<T>
            // -------------------------------------------------------------------
            Console.WriteLine("--- 1. Поліморфна робота з колекцією об'єктів типу IQueue<string> ---");

            List<IQueue<string>> queueCollection = new List<IQueue<string>>
            {
                new ArrayQueue<string>(),
                new LinkedListQueue<string>()
            };

            // Заповнення черг через контракт інтерфейсу
            int queueCounter = 1;
            foreach (var q in queueCollection)
            {
                Console.WriteLine($"\n> Обробка черги #{queueCounter++} ({q.GetType().Name}):");
                q.Enqueue("Завдання А");
                q.Enqueue("Завдання Б");
                q.Enqueue("Завдання В");
            }

            // Вилучення з черг через контракт інтерфейсу
            Console.WriteLine("\n> Вилучення елементів з усіх черг:");
            foreach (var q in queueCollection)
            {
                while (!q.IsEmpty)
                {
                    q.Dequeue();
                }
            }

            Console.WriteLine("\n" + new string('-', 71) + "\n");

            // -------------------------------------------------------------------
            // ДЕМОНСТРАЦІЯ 2: Робота з абстрактним класом TaskScheduler
            // -------------------------------------------------------------------
            Console.WriteLine("--- 2. Поліморфна робота з колекцією об'єктів типу TaskScheduler ---");

            List<TaskScheduler> schedulers = new List<TaskScheduler>
            {
                new FifoScheduler(),
                new PriorityScheduler()
            };

            // Додавання завдань до планувальників
            foreach (var scheduler in schedulers)
            {
                Console.WriteLine($"\n> Планування завдань у {scheduler.SchedulerName}:");
                scheduler.ScheduleTask("Оновити базі даних");
                scheduler.ScheduleTask("CRITICAL: Виправити падіння сервера");
                scheduler.ScheduleTask("Згенерувати звіт");
            }

            // Виконання завдань з використанням поліморфізму та спільної логіки
            Console.WriteLine("\n> Виконання запланованих завдань:");
            foreach (var scheduler in schedulers)
            {
                Console.WriteLine($"\n>> Запуск виконання у {scheduler.SchedulerName}:");
                scheduler.ExecuteNextTask();
                scheduler.ExecuteNextTask();
            }

            Console.WriteLine("\n=======================================================================");
        }
    }
}