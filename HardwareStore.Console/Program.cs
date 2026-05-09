using HardwareStore.Common;
using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
var service = new HardwareServiceAsync<Processor>();

Console.WriteLine("=== ЛР2: Багатопотоковість та LINQ ===");

// 1. Паралельне створення 1000 об'єктів (Parallel.For)
Stopwatch sw = Stopwatch.StartNew();
Parallel.For(0, 1000, i =>
{
    var p = Processor.CreateNew();
    service.CreateAsync(p).Wait();
});
sw.Stop();

Console.WriteLine($"Створено 1000 процесорів за {sw.ElapsedMilliseconds} мс.");

// 2. Використання LINQ для статистики
var allCpus = await service.ReadAllAsync();

double minPrice = allCpus.Min(p => p.Price);
double maxPrice = allCpus.Max(p => p.Price);
double avgPrice = allCpus.Average(p => p.Price);

Console.WriteLine("\n--- Статистика цін (LINQ) ---");
Console.WriteLine($"Мінімальна ціна: {minPrice:F2} грн.");
Console.WriteLine($"Максимальна ціна: {maxPrice:F2} грн.");
Console.WriteLine($"Середня ціна: {avgPrice:F2} грн.");

// 3. Збереження у файл
Console.WriteLine("\nЗбереження даних у файл...");
await service.SaveAsync();
Console.WriteLine("Дані збережено у data.json");

// 4. Демонстрація пагінації
Console.WriteLine("\nПагінація (Сторінка 1, 5 елементів):");
var page = await service.ReadAllAsync(1, 5);
foreach (var p in page)
{
    Console.WriteLine($"{p.Brand} - {p.Price} грн.");
}