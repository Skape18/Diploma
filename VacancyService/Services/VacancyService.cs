using System.Collections.Generic;
using VacancyService.Models;
using VacancyService.Repositories;

namespace VacancyService.Services
{
    public class VacancyService : IVacancyService
    {
        private IVacancyRepository _repository;

        public VacancyService(IVacancyRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Vacancy> GetAll()
        {
            return _repository.GetAll();
        }

        public Vacancy GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Vacancy vacancy)
        {
            _repository.Create(vacancy);
        }

        public void Close(int id)
        {
            _repository.Close(id);
        }
    }
}
