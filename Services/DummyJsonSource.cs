using System.Net.Http.Json;
using UserData.Models;
using System.Text.Json;

namespace UserData.Services;

public class DummyJsonSource : IUserSource
{
    private const string ApiUrl = "https://dummyjson.com/users";

    public async Task<List<User>> FetchUsersAsync()
    {
        using var httpClient = new HttpClient();
        //get users from API as JSON and convert to List<User>
        var stream = await httpClient.GetStreamAsync(ApiUrl);
        // Parse the JSON response
        using var jsonDoc = await JsonDocument.ParseAsync(stream);

        if (!jsonDoc.RootElement.TryGetProperty("users", out var usersJson))
            return new List<User>();

        var users = new List<User>();
        // Iterate through each user in the JSON array
        foreach (var userJson in usersJson.EnumerateArray())
        {
            // Deserialize each user into a DummyUser object
            var user = JsonSerializer.Deserialize<DummyUser>(userJson.GetRawText());
            if (user != null)
            {
                users.Add(new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        SourceId = user.Id.ToString()
                    });
            }
        }

        return users;
    }

    private class DummyUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
