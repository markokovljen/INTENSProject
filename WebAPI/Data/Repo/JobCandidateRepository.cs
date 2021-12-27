using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class JobCandidateRepository : IJobCandidateRepository
    {
        private readonly DataContext dc;

        public JobCandidateRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddJobCandidate(JobCandidate jobCandidate)
        {
            dc.JobCandidates.Add(jobCandidate);
        }

        public void DeleteJobCandidate(int id)
        {
            JobCandidate jobCandidate = dc.JobCandidates.Find(id);
            dc.JobCandidates.Remove(jobCandidate);
        }

        public async Task<JobCandidate> FindJobCandidateAsync(int id)
        {
          JobCandidate jobCandidate = await dc.JobCandidates.Include(jc => jc.JobCandidateSkills)
                                                .FirstOrDefaultAsync(jc => jc.Id == id);

            return jobCandidate;
           

        }

        public async Task<JobCandidate> FindJobCandidateByNameAsync(string name)
        {
         
            JobCandidate jobCandidate = await dc.JobCandidates.FirstOrDefaultAsync(jc => jc.Name == name);
            IQueryable<JobCandidateSkill> query = dc.JobCandidateSkills.Where(jb => jb.JobCandidateId == jobCandidate.Id);
            jobCandidate.JobCandidateSkills = await query.ToListAsync();

            return jobCandidate;
        }

        public IEnumerable<JobCandidate> FindJobCandidatesByNameAndSkillAsync(string name, int skillId)
        {
            IEnumerable<JobCandidate> jobCandidates = dc.JobCandidates.
                                                            Include(jc => jc.JobCandidateSkills).
                                                            Where(jc => jc.Name == name);

            IEnumerable<JobCandidate> result = jobCandidates.Where(jc => jc.JobCandidateSkills.Any(jcs => jcs.SkillId == skillId));

            return result;
            
        }

        public IEnumerable<JobCandidate> FindJobCandidatesByNameAsync(string name)
        {
            IEnumerable<JobCandidate> jobCandidates = dc.JobCandidates.Where(jc => jc.Name == name);
            
            return jobCandidates;
        }

        public IEnumerable<JobCandidate> FindJobCandidatesBySkillAsync(int skillId)
        {
            IEnumerable<JobCandidate> result = dc.JobCandidates.Where(jc => jc.JobCandidateSkills.Any(jcs => jcs.SkillId == skillId));

            return result;
        }

        public async Task<int> FindLastJobCandidateId()
        {
            List<JobCandidate> jobCandidate = await dc.JobCandidates.ToListAsync();
            int result = jobCandidate.Last().Id;
            return result;
        }

        public async Task<IEnumerable<JobCandidate>> GetJobCandidatesAsync()
        {
            return await dc.JobCandidates.Include(jc=>jc.JobCandidateSkills)
                                            .ToListAsync();
        }

    }
}
