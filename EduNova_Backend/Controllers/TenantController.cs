﻿using AutoMapper;
using EduNova.Core.DTO.Tenant;
using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduNova.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TenantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> ReadTenantById(Guid id)
        {
            Tenant? tenant = await _unitOfWork.TenantRepo.GetByIdAsync(id);
            if (tenant == null)
            {
                return NotFound("No tenant found with this id");
            }

            ReadTenantDTO readTenantDTO = _mapper.Map<ReadTenantDTO>(tenant);

            return Ok(readTenantDTO);
        }
    }
}
