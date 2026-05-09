using System.Collections;
using System.Text.Json;

namespace HardwareStore.Common;

public class HardwareServiceAsync<T> : ICrudServiceAsync<T> where T : ComputerComponent
{
    private readonly List<T> _items = new();
    private readonly string _filePath = "data.json";
    private readonly object _lock = new(); // Для thread-safety (lock)
    private readonly SemaphoreSlim _semaphore = new(1, 1); // Для асинхронної синхронізації

    public async Task<bool> CreateAsync(T element)
    {
        lock (_lock) { _items.Add(element); }
        return await Task.FromResult(true);
    }

    public async Task<T> ReadAsync(Guid id) =>
        await Task.Run(() => _items.FirstOrDefault(x => x.Id == id));

    public async Task<IEnumerable<T>> ReadAllAsync() =>
        await Task.FromResult(_items.AsEnumerable());

    // Пагінація (LINQ: Skip, Take)
    public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        return await Task.Run(() => _items
            .Skip((page - 1) * amount)
            .Take(amount)
            .ToList());
    }

    public async Task<bool> UpdateAsync(T element) => await Task.FromResult(true);

    public async Task<bool> RemoveAsync(T element)
    {
        lock (_lock) { return _items.Remove(element); }
    }

    // Асинхронне збереження у файл (JSON)
    public async Task<bool> SaveAsync()
    {
        await _semaphore.WaitAsync(); // Використання семафора
        try
        {
            var json = JsonSerializer.Serialize(_items);
            await File.WriteAllTextAsync(_filePath, json);
            return true;
        }
        finally { _semaphore.Release(); }
    }

    // Реалізація IEnumerable для LINQ
    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public interface ICrudServiceAsync<T> : IEnumerable<T>
{
    Task<bool> CreateAsync(T element);
    Task<T> ReadAsync(Guid id);
    Task<IEnumerable<T>> ReadAllAsync();
    Task<IEnumerable<T>> ReadAllAsync(int page, int amount);
    Task<bool> UpdateAsync(T element);
    Task<bool> RemoveAsync(T element);
    Task<bool> SaveAsync();
}