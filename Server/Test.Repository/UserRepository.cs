using Microsoft.EntityFrameworkCore;
using Test.Entities.DBContext;
using Test.Entities.Models;

namespace Test.Repository
{

    public interface IUserRepository : IAsyncRepository<User>
    {

    }
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        public UserRepository(TestDBContext context) : base(context)
        {
        }
    }
}