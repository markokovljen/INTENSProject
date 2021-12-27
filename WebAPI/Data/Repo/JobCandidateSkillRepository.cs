using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class JobCandidateSkillRepository : IJobCandidateSkillRepository
    {
        private readonly DataContext dc;

        public JobCandidateSkillRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddJobCandidateSkill(int jobCanidateId, int skillId)
        {
            JobCandidateSkill jobCandidateSkill = new JobCandidateSkill();
            jobCandidateSkill.JobCandidateId = jobCanidateId;
            jobCandidateSkill.SkillId = skillId;

            dc.JobCandidateSkills.Add(jobCandidateSkill);
        }

        public void DeleteJobCandidateSkill(int jobCanidateId)
        {
            IQueryable<JobCandidateSkill> query = dc.JobCandidateSkills.Where(jb => jb.JobCandidateId == jobCanidateId);
            foreach(var item in query.ToList())
            {
                dc.JobCandidateSkills.Remove(item);
            }
            
        }

        public void DeleteJobCandidateSkill(int jobCanidateId, int skillId)
        {
            IQueryable<JobCandidateSkill> query = dc.JobCandidateSkills.Where(jb => jb.JobCandidateId == jobCanidateId);
            foreach (var item in query.ToList())
            {
                if (item.SkillId == skillId)
                {
                    dc.JobCandidateSkills.Remove(item);
                    break;
                }
                
            }
        }
    }
}
