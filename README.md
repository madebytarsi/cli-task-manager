# ✅ CLI Task Manager
A simple command-line task manager built with **C# / .NET**, supporting task creation, completion, and deletion with a clean interactive menu.
> 📚 This project was built as a hands-on study exercise while learning C#. The goal was to practice core concepts like classes, services, LINQ, JSON serialization, and user input in a console application.
---
## Features
- Add tasks
- List pending, completed, or all tasks
- Mark tasks as complete
- Move completed tasks back to pending
- Delete tasks with a confirmation prompt
- Input validation with error feedback
---
## Project Structure
```
cli-task-manager/
├── Program.cs
├── Models/
│   └── Task.cs                  # Task data model
└── Services/
    ├── TaskService.cs           # In-memory task logic (CRUD)
    ├── InputService.cs          # User input handling
    └── MenuService.cs           # Menu display and flow control
```
---
## Getting Started
### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or later recommended)
### Running the project
```bash
git clone https://github.com/madebytarsi/cli-task-manager.git
cd cli-task-manager
dotnet run
```
---
## Usage
When you run the app, you'll see a menu like this:
```
=== CLI TO-DO LIST ===

1. List pending tasks
2. List completed tasks
3. List all tasks
4. Add task
5. Complete a task
6. Uncomplete a task
7. Delete a task
0. Quit

Choose an option:
```
Select an option by entering its number. When completing, uncompleting, or deleting, the relevant task list is shown first so you can pick the right ID. Deletions require a Y/N confirmation before executing.

---
## Example
```
Choose an option: 4
Task title: Buy groceries

Task #1 added: "Buy groceries"

Choose an option: 5

Pending tasks:
  [1] [ ] Buy groceries

Enter task ID to mark as complete: 1

Task #1 marked as complete!
```
---
## Error Handling
- **Invalid menu input** — prompts you to enter a valid number
- **Invalid task ID** — re-prompts until a positive number is entered
- **Empty task title** — re-prompts until a non-empty title is provided
- **Completing an already completed task** — displays an error message instead of duplicating
- **Deleting a non-existent task** — displays an error message instead of crashing
---
## Created by
Made with 💙 by [@madebytarsi](https://github.com/madebytarsi)