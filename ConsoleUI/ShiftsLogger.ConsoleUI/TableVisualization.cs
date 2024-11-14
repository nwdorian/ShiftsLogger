using ShiftsLogger.ConsoleUI.Models;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class TableVisualization
{
    public static void ShowUsersTable(List<User> users)
    {
        var table = new Table();
        table.Title = new TableTitle("Users Table", "bold");
        table.AddColumns("Id", "Name", "Email");

        var index = 1;
        foreach (var user in users)
        {
            table.AddRow(index.ToString(), user.ToString(), user.Email ?? "Email not found!");
            index++;
        }

        AnsiConsole.Write(table);
    }
}
