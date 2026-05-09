namespace HardwareStore.Infrastructure.Models
{
    public class HardwareModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public double Price { get; set; }

        // Зовнішній ключ (Foreign Key)
        public int BrandId { get; set; }
        public BrandModel Brand { get; set; } = null!;
    }
}