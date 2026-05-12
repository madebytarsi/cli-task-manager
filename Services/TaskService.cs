using CliTodo.Models;

namespace CliTodo.Services;

public class TaskService
{
    // In-memory list that holds all tasks during the program's lifetime.
    private List<TodoTask> _tasks = new List<TodoTask>();

    public IReadOnlyList<TodoTask> GetAll() => _tasks.AsReadOnly();

    public IReadOnlyList<TodoTask> GetPending() =>
        _tasks.Where(t => !t.IsCompleted).ToList().AsReadOnly();

    public IReadOnlyList<TodoTask> GetCompleted() =>
        _tasks.Where(t => t.IsCompleted).ToList().AsReadOnly();

    public TodoTask Add(string title)
    {
        int nextId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;

        var task = new TodoTask
        {
            Id = nextId,
            Title = title
        };

        _tasks.Add(task);
        return task;
    }

    public bool Complete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task is null || task.IsCompleted) return false;

        task.IsCompleted = true;
        task.CompletedAt = DateTime.Now;
        return true;
    }

    public bool Uncomplete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task is null || !task.IsCompleted) return false;

        task.IsCompleted = false;
        task.CompletedAt = null;
        return true;
    }

    public bool Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return false;

        _tasks.Remove(task);
        return true;
    }

    // Quick helper used by MenuService to check if an ID exists before trying to complete, uncomplete, or delete it.
    public bool Exists(int id) => _tasks.Any(t => t.Id == id);
}