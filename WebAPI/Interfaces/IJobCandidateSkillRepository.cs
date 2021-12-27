using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IJobCandidateSkillRepository
    {
        void AddJobCandidateSkill(int jobCanidateId,int skillId);
        void DeleteJobCandidateSkill(int jobCanidateId);

        void DeleteJobCandidateSkill(int jobCanidateId,int skillId);
    }
}
