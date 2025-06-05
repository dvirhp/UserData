using UserData.Models;

namespace UserData.Services;
public interface IUserSource // interface for fetching user data
{
    Task<List<User>> FetchUsersAsync();
}
