using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VacancyService.Models;
using VacancyService.Services;

namespace VacancyService.Controllers
{
    [Route("api/vacancy")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        // GET: api/<VacancyController>
        [HttpGet]
        public ActionResult<IEnumerable<Vacancy>> GetAll()
        {
            return Ok(_vacancyService.GetAll());
        }

        // GET api/<VacancyController>/5
        [HttpGet("{id}")]
        public ActionResult<Vacancy> GetById(int id)
        {
            return Ok(_vacancyService.GetById(id));
        }

        // POST api/<VacancyController>
        [HttpPost]
        public void Create([FromBody] Vacancy vacancy)
        {
            _vacancyService.Create(vacancy);
        }

        // PUT api/<VacancyController>/5
        [HttpPut("{id}")]
        public void Close(int id)
        {
            _vacancyService.Close(id);
        }
    }
}
