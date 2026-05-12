using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cli_task_manager.Services
{
    public class MenuService
    {
        readonly InputService _inputService;

        public MenuService(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Run()
        {
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
            
            switch (option)
            {
                // add cases
                case 0:
                    // running (bool) = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Choose a number from the menu.");
                    break;
            }
        }

    }
}
