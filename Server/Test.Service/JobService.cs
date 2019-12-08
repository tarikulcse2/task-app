using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Entities.Models;
using Test.Repository;

namespace Test.Service
{

    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAll();
        IEnumerable<object> GetAllByUser(int userId);
        Task<Job> GetById(int id);
        Task<int> Add(Job job);
        Task<int> Modify(Job job);
        Task<int> Delete(int id);
    }

    public class JobService : IJobService
    {
        private readonly IJobRepository jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        async Task<IEnumerable<Job>> IJobService.GetAll()
        {
            return await jobRepository.GetAll();
        }

        IEnumerable<object> IJobService.GetAllByUser(int userId)
        {
            return jobRepository.Query().Where(s => s.CreatorId == userId)
            .GroupBy(e => e.Date)
            .Select(s => new {
                date = s.Key,
                data = s.Select(r => r).ToList()
            }).ToList();
        }

        async Task<Job> IJobService.GetById(int id)
        {
            return await jobRepository.FirstOrDefault(s => s.Id == id);
        }

        async Task<int> IJobService.Modify(Job job)
        {
            Job oldJob = jobRepository.Query().FirstOrDefault(s => s.Id == job.Id);
            oldJob.Title = job.Title;
            oldJob.Description = job.Description;
            oldJob.Date = job.Date;
            oldJob.FromTime = job.FromTime;
            oldJob.ToTime = job.ToTime;
            oldJob.Location = job.Location;
            oldJob.Notify = job.Notify;
            oldJob.Label = job.Label;
            oldJob.ModifierId = job.ModifierId;
            oldJob.ModifyDate = job.ModifyDate;
            return await jobRepository.Update(oldJob);
        }

        async Task<int> IJobService.Add(Job job)
        {
            return await jobRepository.Add(job);
        }

        async Task<int> IJobService.Delete(int id)
        {
            var job = await jobRepository.FirstOrDefault(s => s.Id == id);
            return await jobRepository.Remove(job);
        }
    }
}