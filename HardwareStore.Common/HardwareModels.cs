using System;

namespace HardwareStore.Common
{
    // 1. Абстрактний базовий клас (вимога ЛР)
    public abstract class ComputerComponent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Brand { get; set; } = "Unknown";
        public double Price { get; set; }

        public ComputerComponent(string brand, double price)
        {
            Brand = brand;
            Price = price;
        }
    }

    // 2. Наслідування: Процесор
    public class Processor : ComputerComponent
    {
        public int Cores { get; set; }
        public double Frequency { get; set; }

        // Статичне поле для підрахунку створених об'єктів (вимога ЛР)
        public static int Count;

        public Processor(string brand, double price, int cores) : base(brand, price)
        {
            Cores = cores;
            Count++;
        }
    }

    // 3. Наслідування: Відеокарта
    public class VideoCard : ComputerComponent
    {
        public int MemoryGb { get; set; }

        // Делегат та Подія (вимога ЛР)
        public delegate void OverheatAlert(string message);
        public event OverheatAlert? OnOverheat;

        public VideoCard(string brand, double price, int memory) : base(brand, price)
        {
            MemoryGb = memory;
        }

        public void CheckTemperature(int temp)
        {
            if (temp > 85)
                OnOverheat?.Invoke($"УВАГА! Відеокарта {Brand} перегрілася до {temp}°C!");
        }
    }

    // 4. Додатковий клас
    public class Motherboard : ComputerComponent
    {
        public string Socket { get; set; }
        public Motherboard(string brand, double price, string socket) : base(brand, price)
        {
            Socket = socket;
        }
    }
}
