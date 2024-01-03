using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        private List<ISubject> models;

        public SubjectRepository()
        {
            this.models = new List<ISubject>();
        }

        public IReadOnlyCollection<ISubject> Models => this.models;

        public void AddModel(ISubject model)
        {
            this.models.Add(model);
        }

        public ISubject FindById(int id)
            => this.models.FirstOrDefault(m => m.Id == id);

        public ISubject FindByName(string name)
            => this.models.FirstOrDefault(m => m.Name == name);
    }
}
