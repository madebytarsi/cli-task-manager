using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cli_task_manager.Services
{
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
    }
}
