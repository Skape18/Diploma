using System.Collections.Generic;
using VacancyService.Models;

namespace VacancyService.Repositories
{
    public interface IVacancyRepository
    {
        IEnumerable<Vacancy> GetAll();
        Vacancy GetById(int id);
        void Create(Vacancy vacancy);
        void Close(int id);
    }
}
