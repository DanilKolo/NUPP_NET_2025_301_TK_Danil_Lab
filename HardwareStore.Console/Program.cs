using HardwareStore.Common;

// Виклик статичного методу
HardwareHelper.ShowWelcomeMessage();

// Створення сервісу
var cpuService = new HardwareService<Processor>();

// Додавання об'єктів
cpuService.Create(new Processor("Intel i7", 12000, 8));
cpuService.Create(new Processor("AMD Ryzen 5", 8000, 6));

Console.WriteLine("\nСписок процесорів:");
foreach (var cpu in cpuService.GetAll())
{
    cpu.PrintShortInfo();
}

// Події
var gpu = new VideoCard("Nvidia RTX 4060", 15000, 8);
gpu.OnOverheat += (msg) => Console.WriteLine(msg);
gpu.CheckTemperature(95);

Console.WriteLine("\nПрограма завершила роботу. Натисніть будь-яку клавішу...");
Console.ReadKey();