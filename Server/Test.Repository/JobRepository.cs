using Microsoft.EntityFrameworkCore;
using Test.Entities.DBContext;
using Test.Entities.Models;

namespace Test.Repository
{

    public interface IJobRepository : IAsyncRepository<Job>
    {
        
    }

    public class JobRepository : AsyncRepository<Job>, IJobRepository
    {
        public JobRepository(TestDBContext context) : base(context)
        {
        }
    }
}