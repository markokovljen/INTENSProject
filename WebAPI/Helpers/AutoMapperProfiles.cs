using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<JobCandidate, JobCandidateDto>().ReverseMap();
            CreateMap<JobCandidate, JobCandidateListDto>().ReverseMap();
            CreateMap<JobCandidate, JobCandidateDetailDto>().ReverseMap();
            CreateMap<JobCandidateDetailDto, JobCandidateDto>().ReverseMap();

            CreateMap<Skill, SkillDto>().ReverseMap();
            
        }
    }
}
