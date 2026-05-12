namespace CliTodo.Services;

public class InputService
{
    public int ReadMenuOption()
    {
        while (true)
        {
            Console.Write("Choose an option: ");
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int option))
                return option;

            Console.WriteLine("Invalid option. Please enter a number.");
        }
    }

    public int ReadTaskId(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int id) && id > 0)
                return id;

            Console.WriteLine("Invalid ID. Please enter a positive number.");
        }
    }

    public string ReadTaskTitle()
    {
        while (true)
        {
            Console.Write("Task title: ");
            string? input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
                return input;

            Console.WriteLine("Title cannot be empty.");
        }
    }

    public bool Confirm(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (Y/N): ");
            string? input = Console.ReadLine()?.Trim().ToUpper();

            if (input == "Y") return true;
            if (input == "N") return false;

            Console.WriteLine("Please enter Y or N.");
        }
    }
}