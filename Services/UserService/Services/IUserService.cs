using UserService.DTOs;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<UserDto> CreateUser(CreateUserDto userDto);
        Task<bool> UpdateUser(int id, CreateUserDto userDto);
        Task<bool> DeleteUser(int id);
        Task<AuthResponseDto> Authenticate(LoginDto loginDto);
    }
}