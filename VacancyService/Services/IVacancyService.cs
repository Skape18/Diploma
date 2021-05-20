using System.Collections.Generic;
using VacancyService.Models;

namespace VacancyService.Services
{
    public interface IVacancyService
    {
        IEnumerable<Vacancy> GetAll();
        Vacancy GetById(int id);
        void Create(Vacancy vacancy);
        void Close(int id);
    }
}
