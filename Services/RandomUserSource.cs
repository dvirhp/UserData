using System.Net.Http.Json;
using UserData.Models;

namespace UserData.Services;

public class RandomUserSource : IUserSource
{
    private const string ApiUrl = "https://randomuser.me/api/?results=500";

    public async Task<List<User>> FetchUsersAsync()
    {
        using var httpClient = new HttpClient();
        var data = await httpClient.GetFromJsonAsync<RandomUserApiResponse>(ApiUrl);

        if (data?.Results == null)// Check if the API response is null or has no results
            return new List<User>();

        var users = new List<User>();

        foreach (var item in data.Results)  // Iterate through each user in the results
        {
            var user = new User
            {
                FirstName = item.Name.First,
                LastName = item.Name.Last,
                Email = item.Email,
                SourceId = item.Login.Uuid
            };

            users.Add(user);
        }

        return users;
    }


    private class RandomUserApiResponse // Represents the structure of the API response
    {
        public List<RandomUser>? Results { get; set; }
    }

    private class RandomUser // Represents a single user in the API response
    {
        public Name Name { get; set; } = new();
        public string Email { get; set; } = "";
        public Login Login { get; set; } = new();
    }

    private class Name
    {
        public string First { get; set; } = "";
        public string Last { get; set; } = "";
    }

    private class Login
    {
        public string Uuid { get; set; } = "";
    }
}
