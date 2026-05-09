namespace HardwareStore.Common;

// Інтерфейс (вимога ЛР)
public interface ICrudService<T>
{
    void Create(T item);
    IEnumerable<T> GetAll();
}

// Дженерік клас (вимога ЛР)
public class HardwareService<T> : ICrudService<T> where T : ComputerComponent
{
    private List<T> _items = new();
    public void Create(T item) => _items.Add(item);
    public IEnumerable<T> GetAll() => _items;
}