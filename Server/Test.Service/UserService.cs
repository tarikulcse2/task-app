using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Entities.Models;
using Test.Repository;

namespace Test.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task Registration(User user);
        Task<User> CheckUserLogin(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        async Task<User> IUserService.CheckUserLogin(User user)
        {
            return await userRepository.FirstOrDefault(s => s.Email == user.Email && s.Password == user.Password);
        }

        async Task<IEnumerable<User>> IUserService.GetAll()
        {
            return await userRepository.GetAll();
        }

        async Task IUserService.Registration(User user)
        {
            await userRepository.Add(user);
        }
    }
}