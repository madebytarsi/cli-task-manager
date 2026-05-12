using CliTodo.Services;

// Create each service and inject the dependencies.
var taskService = new TaskService();
var inputService = new InputService();
var menuService = new MenuService(taskService, inputService);

// Start the app.
menuService.Run();