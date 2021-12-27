using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DataContext dc;

        public SkillRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddSkill(Skill skill)
        {
            dc.Skills.Add(skill);
        }

        public async Task<Skill> FindSkillByName(string name)
        {
            return await dc.Skills.FirstOrDefaultAsync(s=>s.Name==name);
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await dc.Skills.ToListAsync();
        }
    }
}
