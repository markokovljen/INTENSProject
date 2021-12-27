using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IJobCandidateRepository JobCandidateRepository { get; }
        ISkillRepository SkillRepository { get; }
        IJobCandidateSkillRepository JobCandidateSkillRepository {get;}
        Task<bool> SaveAsync();
    }
}
