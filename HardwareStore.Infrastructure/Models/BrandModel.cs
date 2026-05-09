using System.Collections.Generic;

namespace HardwareStore.Infrastructure.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        // Зв'язок один-до-багатьох: у одного бренду багато товарів
        public List<HardwareModel> Items { get; set; } = new();
    }
}