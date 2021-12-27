using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Skill : BaseEntity
    {
       
        [Required]
        public string Name { get; set; }

        public ICollection<JobCandidateSkill> JobCandidateSkills { get; set; }
    }
}
