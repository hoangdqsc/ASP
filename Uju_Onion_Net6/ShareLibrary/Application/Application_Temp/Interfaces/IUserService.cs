namespace Application_Temp.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Authenticate(string username, string password);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task AddUser(UserDto userDto);
        Task UpdateUser(UserDto userDto);
        Task DeleteUser(int id);
    }
}
