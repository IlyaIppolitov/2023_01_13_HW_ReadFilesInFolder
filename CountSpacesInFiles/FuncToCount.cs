using System.Diagnostics;

namespace CountSpacesInFiles
{
    public class FuncToCount
    {
        public static void CountSpacesInAllFiles(string dirFullName)
        {
            // Проверка на существование папки
            if (!Directory.Exists(dirFullName))
            {
                Console.WriteLine($"Папки с названием \"{dirFullName}\" не существует! До свидания!");
                return;
            }

            // Проверка на наличие файлов в папке
            if (Directory.GetFiles(dirFullName).Length == 0)
            {
                Console.WriteLine($"В папке с названием \"{dirFullName}\" нет файлов! До свидания!");
                return;
            }

            // Пуск таймера для проверки времени работы программы
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // Опреление потоков в соответствии с количеством файлов в папке
            var setOfFiles = Directory.EnumerateFiles(dirFullName, "*.*", SearchOption.TopDirectoryOnly);
            Task[] tasks = new Task[setOfFiles.Count()];

            // Создание потоков для каждого файла
            int numberOfFiles = 0;
            foreach (var file in setOfFiles)
            {
                tasks[numberOfFiles] = Task.Factory.StartNew(() =>
                {
                    FileInfo tempFileInfo = new FileInfo(file);
                    var content = File.ReadAllText(file);
                    var spacesCount = content.Count(chr => chr == ' ');
                    Console.WriteLine($"\nФайл: {tempFileInfo.Name}");
                    Console.WriteLine($"Количество пробелов: {spacesCount}");
                    Console.WriteLine($"Поток: {Environment.CurrentManagedThreadId}");
                });
                numberOfFiles++;
            }

            // Ожидание завершения всех процессов
            Task.WaitAll(tasks);

            // Останов таймера
            stopWatch.Stop();

            // Получение прошедшего времени
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"\nПодсчёт количества пробелов в {numberOfFiles} файлах был выполнен за " + ts.Milliseconds.ToString() + "мс.");
        }
    }
}