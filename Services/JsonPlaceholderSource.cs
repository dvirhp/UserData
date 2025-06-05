using System.Net.Http.Json;
using UserData.Models;

namespace UserData.Services;

public class JsonPlaceholderSource : IUserSource
{
    private const string ApiUrl = "https://jsonplaceholder.typicode.com/users";

    public async Task<List<User>> FetchUsersAsync()
    {
        using var httpClient = new HttpClient();
        // Get users from API as JSON and convert to List<ApiUser>
        var usersFromApi = await httpClient.GetFromJsonAsync<List<ApiUser>>(ApiUrl);

        if (usersFromApi == null)
            return new List<User>();

        var users = new List<User>();

        foreach (var apiUser in usersFromApi)
        {
            var nameParts = apiUser.Name.Split(' ', 2);

            var firstName = nameParts.Length > 0 ? nameParts[0] : "";
            var lastName = nameParts.Length > 1 ? nameParts[1] : "";

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = apiUser.Email,
                SourceId = apiUser.Id.ToString()
            };

            users.Add(user);
        }

        return users;
    }

    private class ApiUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
