using Refit;
using ShiftsLogger.ConsoleUI.RefitClients;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Services;
public class UserService
{
    private readonly IUsersClient _userClient;

    public UserService(IUsersClient userClient)
    {
        _userClient = userClient;
    }
    public async Task GetAll()
    {
        try
        {
            var response = await _userClient.GetAll();

            if (!response.IsSuccessful)
            {
                AnsiConsole.MarkupLine($"[red]Error[/]: {response.Error} [red]Status code[/]: {response.StatusCode}");
                AnsiConsole.Write("Press any key to continue");
                Console.ReadLine();
                return;
            }

            var users = response.Content;
            TableVisualization.ShowUsersTable(users);

            var index = UserInput.GetIndexInput("Select a user (or press 0 to exit):");

            while (index > users.Count)
            {
                AnsiConsole.Clear();
                TableVisualization.ShowUsersTable(users);
                AnsiConsole.MarkupLine($"[red]User with Id[/] {index} [red]doesn't exist![/]");
                index = UserInput.GetIndexInput("Select a valid user Id (or press 0 to exit):");
            }

            if (index == 0)
            {
                return;
            }


        }
        catch (ApiException ex)
        {
            Console.WriteLine("There was an error while calling the API: " + ex.Message);
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
            Console.ReadLine();
        }


    }
}
