using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IJobCandidateRepository
    {
        void AddJobCandidate(JobCandidate jobCandidate);
        Task<JobCandidate> FindJobCandidateAsync(int id);
        Task<IEnumerable<JobCandidate>> GetJobCandidatesAsync();
        Task<int> FindLastJobCandidateId();
        Task<JobCandidate> FindJobCandidateByNameAsync(string name);
        IEnumerable<JobCandidate> FindJobCandidatesByNameAndSkillAsync(string name,int skillId);
        IEnumerable<JobCandidate> FindJobCandidatesByNameAsync(string name);
        IEnumerable<JobCandidate> FindJobCandidatesBySkillAsync(int skillId);
        void DeleteJobCandidate(int id);
        
    }
}
