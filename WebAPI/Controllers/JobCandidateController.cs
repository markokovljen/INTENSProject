using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Errors;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class JobCandidateController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public JobCandidateController(IUnitOfWork unitOfWork,
                                      IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        
        [HttpGet("list")]
        public async Task<IActionResult> GetJobCandidateList()
        {
            IEnumerable<JobCandidate> jobCandidates = await unitOfWork.JobCandidateRepository.GetJobCandidatesAsync();

            if (jobCandidates == null)
            {
                return NotFound();
            }

            IEnumerable<JobCandidateListDto> jobCandidateListDto = mapper.Map<List<JobCandidateListDto>>(jobCandidates);
            
            
            return Ok(jobCandidateListDto);
        }

        [HttpGet("detail/{jobCandidateId}")]
        public async Task<IActionResult> GetJobCandidate(int jobCandidateId)
        {
            JobCandidate jobCandidate = await unitOfWork.JobCandidateRepository.FindJobCandidateAsync(jobCandidateId);

            if (jobCandidate == null)
            {
                return NotFound();
            }

            JobCandidateDetailDto jobCandidateDto = mapper.Map<JobCandidateDetailDto>(jobCandidate);
         
            List<string> skillNames = await MapSkillsToJobCandidate(jobCandidate.JobCandidateSkills);
            jobCandidateDto.CandidateSkills = skillNames;



            return Ok(jobCandidateDto);
        }

        private async Task<List<string>> MapSkillsToJobCandidate(IEnumerable<JobCandidateSkill> jobCandidateSkills)
        {
            
            List<string> result = new List<string>(jobCandidateSkills.Count());
            foreach (var item in jobCandidateSkills)
            {
                IEnumerable<Skill> skills = await unitOfWork.SkillRepository.GetSkillsAsync();
                skills = skills.Where(s => s.Id == item.SkillId);
                result.Add(skills.First().Name);

            }
            return result;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddJobCandidate(JobCandidateDto jobCandidateDto)
        {

            JobCandidate jobCandidate = mapper.Map<JobCandidate>(jobCandidateDto);

            unitOfWork.JobCandidateRepository.AddJobCandidate(jobCandidate);
            await unitOfWork.SaveAsync();

            await AddSkillToJobCandidate(jobCandidateDto,"newCandidate");


            return StatusCode(201);
        }
       
        private async Task AddSkillToJobCandidate(JobCandidateDto jobCandidateDto, string newOrExisting,
                                                  JobCandidate existingJobCandidate= null)
        {
            foreach (var item in jobCandidateDto.CandidateSkills)
            {
                IEnumerable<Skill> skills = await unitOfWork.SkillRepository.GetSkillsAsync();
                skills = skills.Where(s => s.Name == item);
                if (skills.ToList().Count == 0)
                {
                    Skill skill = new Skill();
                    skill.Name = item;
                    unitOfWork.SkillRepository.AddSkill(skill);

                    await unitOfWork.SaveAsync();
        
                }

                switch (newOrExisting)
                {
                    case "newCandidate":
                        await AddSkillToNewOrExistingJobCandidate(item);
                        break;
                    case "existingCandidate":
                        Skill skill = skills.FirstOrDefault();
                        if (skill == null)
                        {
                            await AddSkillToNewOrExistingJobCandidate(item, existingJobCandidate.Id);
                            break;
                        }
                        else
                        {
                            IEnumerable<JobCandidateSkill> result = existingJobCandidate.JobCandidateSkills.Where(jcs => jcs.SkillId == skill.Id);
                            if (result.Count()==0)
                            {
                                await AddSkillToNewOrExistingJobCandidate(item, existingJobCandidate.Id);
                            }
                            break;
                        }                        
                                               
                }

            }
        }
        private async Task AddSkillToNewOrExistingJobCandidate(string skillName,
                                                               int existingJobCandidateId=0)
        {
            int jobCandidateId = 0;

            if (existingJobCandidateId == 0)
            {
                jobCandidateId = await unitOfWork.JobCandidateRepository.FindLastJobCandidateId();
            }
            else
            {
                jobCandidateId = existingJobCandidateId;
            }

            
            int skillId = (await unitOfWork.SkillRepository.FindSkillByName(skillName)).Id;

            unitOfWork.JobCandidateSkillRepository.AddJobCandidateSkill(jobCandidateId, skillId);
            await unitOfWork.SaveAsync();
        }



        [HttpPut("update/{jobCandidateId}")]
        public async Task<IActionResult> UpdateJobCandidateWithSkill(int jobCandidateId, JobCandidateDetailDto updateJobCandidateDto)
        {
            
            JobCandidate jobCandidateFromDb = await unitOfWork.JobCandidateRepository.FindJobCandidateAsync(jobCandidateId);

            ApiError apiError = GenerateError(jobCandidateFromDb, "Job Candidate");
            if (apiError != null)
            {
                return BadRequest(apiError);
            }

            mapper.Map(updateJobCandidateDto, jobCandidateFromDb);
            JobCandidateDto jobCandidateDto = new JobCandidateDto();
            mapper.Map(updateJobCandidateDto, jobCandidateDto);

            await AddSkillToJobCandidate(jobCandidateDto, "existingCandidate",jobCandidateFromDb);



            await unitOfWork.SaveAsync();
            return StatusCode(200);
        }

        

        [HttpPut("remove/{jobCandidateId}/{skillName}")]
        public async Task<IActionResult> RemoveSkillFromJobCandidate(int jobCandidateId,string skillName)
        {
            Skill skillFromDb = await unitOfWork.SkillRepository.FindSkillByName(skillName);
            JobCandidate jobCandidate = await unitOfWork.JobCandidateRepository.FindJobCandidateAsync(jobCandidateId);
            if (skillFromDb == null || jobCandidate==null)
                return BadRequest("Remove not allowed");

            unitOfWork.JobCandidateSkillRepository.DeleteJobCandidateSkill(jobCandidateId, skillFromDb.Id);

            await unitOfWork.SaveAsync();
            return StatusCode(200);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteJobCandidate(int id)
        {
            JobCandidate jobCandidate = await unitOfWork.JobCandidateRepository.FindJobCandidateAsync(id);

            ApiError apiError = GenerateError(jobCandidate, "Job Candidate");
            if (apiError != null)
            {
                return BadRequest(apiError);
            }

            unitOfWork.JobCandidateSkillRepository.DeleteJobCandidateSkill(id);

            unitOfWork.JobCandidateRepository.DeleteJobCandidate(id);

            await unitOfWork.SaveAsync();

            return Ok(id);
        }

        #nullable enable
        [HttpGet("search/{jobCandidateName?}/{skillName?}")]
        public async Task<ActionResult<IEnumerable<JobCandidate>>> Search(string? jobCandidateName = null, string? skillName = null)
        {
            if (string.IsNullOrEmpty(skillName) && string.IsNullOrEmpty(jobCandidateName))
                return NotFound();

            Skill skill = await unitOfWork.SkillRepository.FindSkillByName(skillName);


            IEnumerable<JobCandidate> result = new List<JobCandidate>();

            if (!string.IsNullOrEmpty(jobCandidateName) && !string.IsNullOrEmpty(skillName))
            {

                if (skill == null)
                    return NotFound();
                result = unitOfWork.JobCandidateRepository.FindJobCandidatesByNameAndSkillAsync(jobCandidateName, skill.Id);

            }
            else if (!string.IsNullOrEmpty(jobCandidateName))
            {
                result = unitOfWork.JobCandidateRepository.FindJobCandidatesByNameAsync(jobCandidateName);

            }
            else if (!string.IsNullOrEmpty(skillName))
            {
                if (skill == null)
                    return NotFound();
                result = unitOfWork.JobCandidateRepository.FindJobCandidatesBySkillAsync(skill.Id);
            }
            if (result.Count() == 0)
                return NotFound();


              return Ok(result);
                                
        }
        #nullable disable


    }
}
