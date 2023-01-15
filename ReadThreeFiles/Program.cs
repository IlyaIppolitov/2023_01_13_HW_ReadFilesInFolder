using CountSpacesInFiles;

Console.WriteLine("=====Программа подсчёта пробелов в файлах=====");

string dirFullName = "D:\\FilesToRead";

Console.WriteLine($"По умолчанию будет использована папка \"{dirFullName}\".");
Console.WriteLine("Для продолжения нажмите Enter или, при необходимости, введите полный путь к альтернативной директории: ");
string? tempName = Console.ReadLine();

if (tempName != string.Empty)
    dirFullName = tempName;

FuncToCount.CountSpacesInAllFiles(dirFullName);
