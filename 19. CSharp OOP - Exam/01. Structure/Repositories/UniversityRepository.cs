using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class UniversityRepository : IRepository<IUniversity>
    {
        List<IUniversity> universities;

        public UniversityRepository()
        {
            universities = new List<IUniversity>();
        }

        public IReadOnlyCollection<IUniversity> Models => this.universities;

        public void AddModel(IUniversity model)
        {
            this.universities.Add(model);
        }

        public IUniversity FindById(int id)
            => this.universities.FirstOrDefault(u => u.Id == id);

        public IUniversity FindByName(string name)
            => this.universities.FirstOrDefault(u => u.Name == name);
    }
}
