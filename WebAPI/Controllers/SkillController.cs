using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class SkillController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SkillController(IUnitOfWork unitOfWork,
                               IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSkill(SkillDto skillDto)
        {
            Skill skill = mapper.Map<Skill>(skillDto);

            unitOfWork.SkillRepository.AddSkill(skill);

            await unitOfWork.SaveAsync();

            return StatusCode(201);
        }
    }
}
