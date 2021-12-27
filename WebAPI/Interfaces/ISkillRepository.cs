using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ISkillRepository
    {
        void AddSkill(Skill skill);
        Task<Skill> FindSkillByName(string name);
        Task<IEnumerable<Skill>> GetSkillsAsync();
    }
}
