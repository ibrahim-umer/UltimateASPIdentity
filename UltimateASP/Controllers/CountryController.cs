using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateASP.Models;
using UltimateASP.RepoOps.IRepo;

namespace UltimateASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public CountryController(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries =await _unitOfWork.Countries.GetAll();
                var Result = _mapper.Map<IList<CountryDTO>>(countries);
                _logger.Error($"Run properly {nameof(GetCountries)}");
                return Ok(Result);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, $"some thing wents wrong {nameof(GetCountries)}");
                return StatusCode(500,  ex.Message);
            }
        }

        [HttpGet("{Id:int}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {

                var countries = await _unitOfWork.Countries.Get(x=>x.Id == Id , new List<string> { "Hotels" });
                if (countries != null) return NoContent();
                var Result = _mapper.Map<CountryDTO>(countries);
                _logger.Information($"Run properly {nameof(Get)} Country");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"some thing wents wrong {nameof(GetCountries)}");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
