using AutoMapper;
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
    public class HotelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public HotelsController(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();
                var Result = _mapper.Map<IList<HotelDTO>>(hotels);
                _logger.Error($"Run properly {nameof(GetHotels)}");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"some thing wents wrong {nameof(GetHotels)}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.Get(x => x.ID == Id);
                var Result = _mapper.Map<HotelDTO>(hotels);
                _logger.Error($"Run properly {nameof(Get)} Country");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"some thing wents wrong {nameof(Get)}");
                return StatusCode(500, ex.Message);
            }
        }


    }
}
