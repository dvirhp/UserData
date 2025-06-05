using System.Net.Http;
using System.Net.Http.Json;
using UserData.Models;

namespace UserData.Services;

public class ReqresSource : IUserSource
{
    private const string Url = "https://reqres.in/api/users?page={0}";

    public async Task<List<User>> FetchUsersAsync()
    {
        var users = new List<User>();

        // I added the "x-api-key" header because I tried to call the API without it and it didn't work.
        // The server gave an error and said I need to provide an API key.
        // Also, I set the User-Agent to identify the app, which can help avoid some server issues.
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("UserDataApp/1.0");

        int page = 1;

        while (true)
        {
            // Construct the URL with the current page number
            string urlWithPage = string.Format(Url, page);
            // Fetch users from the API
            var response = await httpClient.GetFromJsonAsync<ReqresResponse>(urlWithPage);

            if (response == null || response.Data == null || response.Data.Count == 0)
                break;

            foreach (var apiUser in response.Data)
            {
                var user = new User
                {
                    FirstName = apiUser.FirstName,
                    LastName = apiUser.LastName,
                    Email = apiUser.Email,
                    SourceId = apiUser.Id.ToString()
                };
                users.Add(user);
            }

            if (page >= response.TotalPages)
                break;

            page++;
        }

        return users;
    }


    private class ReqresResponse
    {
        public List<ReqresUser>? Data { get; set; }
        public int TotalPages { get; set; }
    }

    private class ReqresUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
