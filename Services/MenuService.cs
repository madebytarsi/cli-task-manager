using CliTodo.Models;

namespace CliTodo.Services;

public class MenuService
{
    // MenuService doesn't do logic or input itself —
    // it delegates to TaskService and InputService respectively.
    private readonly TaskService _taskService;
    private readonly InputService _inputService;

    // Both dependencies are injected via the constructor.
    // This is called Dependency Injection: instead of creating them here,
    // we receive them from outside (Program.cs), making each class easier to change.
    public MenuService(TaskService taskService, InputService inputService)
    {
        _taskService = taskService;
        _inputService = inputService;
    }

    public void Run()
    {
        bool running = true;

        // The main loop: keeps showing the menu until the user chooses to quit.
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== CLI TO-DO LIST ===");
            Console.WriteLine();
            Console.WriteLine("1. List pending tasks");
            Console.WriteLine("2. List completed tasks");
            Console.WriteLine("3. List all tasks");
            Console.WriteLine("4. Add task");
            Console.WriteLine("5. Complete a task");
            Console.WriteLine("6. Uncomplete a task");
            Console.WriteLine("7. Delete a task");
            Console.WriteLine("0. Quit");
            Console.WriteLine();

            int option = _inputService.ReadMenuOption();
            Console.WriteLine();

            // Each case delegates to a private handler method below.
            // Keeping the switch clean and each action in its own method
            // makes the code much easier to read and maintain.
            switch (option)
            {
                case 1: HandleListPending(); break;
                case 2: HandleListCompleted(); break;
                case 3: HandleListAll(); break;
                case 4: HandleAdd(); break;
                case 5: HandleComplete(); break;
                case 6: HandleUncomplete(); break;
                case 7: HandleDelete(); break;
                case 0: running = false; break;
                default:
                    Console.WriteLine("  Invalid option. Choose a number from the menu.");
                    break;
            }

            // After any action (except quit), pause so the user can read the output
            // before the screen clears on the next loop iteration.
            if (running)
            {
                Console.WriteLine();
                Console.Write("  Press any key to continue...");
                Console.ReadKey(intercept: true); // intercept: true means the key won't be printed to the console.
            }
        }

        Console.WriteLine();
        Console.WriteLine("  Goodbye!");
    }

    // Shared helper to print a labeled list of tasks.
    // Used by HandleListPending, HandleListCompleted, and HandleListAll.
    private void PrintTasks(IReadOnlyList<TodoTask> tasks, string label)
    {
        Console.WriteLine($"  {label}:");

        if (tasks.Count == 0)
        {
            Console.WriteLine("  No tasks found.");
            return;
        }

        foreach (var task in tasks)
        {
            // Ternary operator: if completed show ✓, otherwise show an empty box.
            string status = task.IsCompleted ? "[✓]" : "[ ]";
            Console.WriteLine($"  [{task.Id}] {status} {task.Title}");
        }
    }

    private void HandleListPending()
    {
        PrintTasks(_taskService.GetPending(), "Pending tasks");
    }

    private void HandleListCompleted()
    {
        PrintTasks(_taskService.GetCompleted(), "Completed tasks");
    }

    private void HandleListAll()
    {
        PrintTasks(_taskService.GetAll(), "All tasks");
    }

    private void HandleAdd()
    {
        string title = _inputService.ReadTaskTitle();
        var task = _taskService.Add(title); // Add() returns the created task so we can show its ID.
        Console.WriteLine();
        Console.WriteLine($"  Task #{task.Id} added: \"{task.Title}\"");
    }

    private void HandleComplete()
    {
        var pending = _taskService.GetPending();

        // No point asking for an ID if there's nothing to complete.
        if (pending.Count == 0)
        {
            Console.WriteLine("  No pending tasks to complete.");
            return;
        }

        // Show the list first so the user knows which IDs are available.
        PrintTasks(pending, "Pending tasks");
        Console.WriteLine();
        int id = _inputService.ReadTaskId("Enter task ID to mark as complete");
        Console.WriteLine();

        // Complete() returns false if the task doesn't exist or is already done.
        if (!_taskService.Exists(id))
            Console.WriteLine($"  No task found with ID #{id}.");
        else if (!_taskService.Complete(id))
            Console.WriteLine($"  Task #{id} is already completed.");
        else
            Console.WriteLine($"  Task #{id} marked as complete!");
    }

    private void HandleUncomplete()
    {
        var completed = _taskService.GetCompleted();

        if (completed.Count == 0)
        {
            Console.WriteLine("  No completed tasks to reopen.");
            return;
        }

        PrintTasks(completed, "Completed tasks");
        Console.WriteLine();
        int id = _inputService.ReadTaskId("Enter task ID to mark as pending");
        Console.WriteLine();

        if (!_taskService.Exists(id))
            Console.WriteLine($"  No task found with ID #{id}.");
        else if (!_taskService.Uncomplete(id))
            Console.WriteLine($"  Task #{id} is not completed.");
        else
            Console.WriteLine($"  Task #{id} moved back to pending.");
    }

    private void HandleDelete()
    {
        var all = _taskService.GetAll();

        if (all.Count == 0)
        {
            Console.WriteLine("  No tasks to delete.");
            return;
        }

        PrintTasks(all, "All tasks");
        Console.WriteLine();
        int id = _inputService.ReadTaskId("Enter task ID to delete");
        Console.WriteLine();

        if (!_taskService.Exists(id))
        {
            Console.WriteLine($"  No task found with ID #{id}.");
            return;
        }

        // Ask for confirmation before a destructive action.
        // Good UX practice: deletions should always require a second step.
        bool confirmed = _inputService.Confirm($"Are you sure you want to delete task #{id}?");
        Console.WriteLine();

        if (confirmed)
        {
            _taskService.Delete(id);
            Console.WriteLine($"  Task #{id} deleted.");
        }
        else
        {
            Console.WriteLine("  Deletion cancelled.");
        }
    }
}