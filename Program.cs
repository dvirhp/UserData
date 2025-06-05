using UserData.Models;
using UserData.Services;
using UserData.Utils;

Console.WriteLine("Enter folder path to save the file:");
string? folderPath = Console.ReadLine();// could be null if the user just presses Enter (?)

while (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
{
    Console.WriteLine("Invalid folder path. Try again:");
    folderPath = Console.ReadLine();
}

Console.WriteLine("Choose file format (csv / json):");
string? format = Console.ReadLine()?.ToLower();

while (format != "csv" && format != "json")
{
    Console.WriteLine("Invalid format. Please type 'csv' or 'json':");
    format = Console.ReadLine()?.ToLower();
}

try
{
    Console.WriteLine("\nFetching users from APIs...\n");

    // adding all user sources to a list if you want to add more sources in the future in simple way
    List<IUserSource> sources = new()
    {
        new RandomUserSource(),
        new JsonPlaceholderSource(),
        new DummyJsonSource(),
        new ReqresSource()
    };

    List<Task<List<User>>> fetchTasks = new List<Task<List<User>>>();

    foreach (IUserSource source in sources)
    {
        Task<List<User>> fetchTask = source.FetchUsersAsync();
        fetchTasks.Add(fetchTask);
    }

    await Task.WhenAll(fetchTasks);

    var allUsers = fetchTasks.SelectMany(task => task.Result).ToList();

    // This will overwrite the existing file. 
    // If you want to create a new file each time, you can uncomment the line below and comment the line above.
    var fileName = $"users.{format}";
    //var fileName = $"users_{DateTime.Now:yyyyMMdd_HHmmss}.{format}";

    var fullPath = Path.Combine(folderPath!, fileName); // Ensure folderPath is not null

    if (format == "csv") CsvExporter.ExportToCsv(fullPath, allUsers);
    else JsonExporter.ExportToJson(fullPath, allUsers);

    Console.WriteLine($"\nSaved {allUsers.Count} users to:\n{fullPath}");
}
catch (HttpRequestException httpEx)
{
    Console.WriteLine($"Network error while fetching users: {httpEx.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
