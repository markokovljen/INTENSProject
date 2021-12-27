using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class JobCandidateSkill
    {
        public int JobCandidateId { get; set; }
        public JobCandidate JobCandidate { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

    }
}
