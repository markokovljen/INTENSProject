using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repo;
using WebAPI.Interfaces;

namespace WebAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }

        public IJobCandidateRepository JobCandidateRepository => new JobCandidateRepository(dc);

        public ISkillRepository SkillRepository => new SkillRepository(dc);

        public IJobCandidateSkillRepository JobCandidateSkillRepository => new JobCandidateSkillRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
