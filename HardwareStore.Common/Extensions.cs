namespace HardwareStore.Common;

// Статичний клас для методів розширення
public static class ComponentExtensions
{
    // Метод розширення (вимога ЛР)
    public static void PrintShortInfo(this ComputerComponent c)
    {
        Console.WriteLine($"[ID: {c.Id.ToString().Substring(0, 8)}] {c.Brand} - Ціна: {c.Price} грн.");
    }
}

public class HardwareHelper
{
    // Статичний метод (вимога ЛР)
    public static void ShowWelcomeMessage()
    {
        Console.WriteLine("=== Система управління складом HardwareStore ===");
    }
}