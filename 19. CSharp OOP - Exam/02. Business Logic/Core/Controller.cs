using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }

        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName($"{firstName} {lastName}") != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }
            else
            {
                IStudent student = new Student(students.Models.Count + 1, firstName, lastName);

                students.AddModel(student);

                return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, nameof(StudentRepository));
            }
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(TechnicalSubject) &&
                subjectType != nameof(EconomicalSubject) &&
                subjectType != nameof(HumanitySubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            else if (subjects.FindByName(subjectName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }
            else
            {
                ISubject subject;

                int subjectId = subjects.Models.Count + 1;

                if (subjectType == nameof(TechnicalSubject))
                {
                    subject = new TechnicalSubject(subjectId, subjectName);
                }
                else if (subjectType == nameof(EconomicalSubject))
                {
                    subject = new EconomicalSubject(subjectId, subjectName);
                }
                else // subjectType == nameof(HumanitySubject)
                {
                    subject = new HumanitySubject(subjectId, subjectName);
                }

                subjects.AddModel(subject);

                return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, nameof(SubjectRepository));
            }
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            else
            {
                List<int> newRequiredSubjects = new List<int>();

                foreach (var subjectName in requiredSubjects)
                {
                    newRequiredSubjects.Add(this.subjects.FindByName(subjectName).Id);
                }

                IUniversity university = new University(universities.Models.Count + 1, universityName, category, capacity, newRequiredSubjects);

                this.universities.AddModel(university);

                return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, nameof(UniversityRepository));
            }
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string firstName = studentName.Split(' ')[0];
            string lastName = studentName.Split(' ')[1];

            IStudent student = students.FindByName(studentName);
            IUniversity university = universities.FindByName(universityName);

            if (student == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, firstName, lastName);
            }
            else if (university == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }
            else if (!university.RequiredSubjects.All(urs => student.CoveredExams.Any(sce => sce == urs)))
            {
                return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }
            else
            {
                if (student.University != null && student.University.Name == universityName)
                {
                    return string.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName);
                }
                else
                {
                    student.JoinUniversity(university);

                    return string.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);
                }
            }
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            ISubject subject = subjects.FindById(subjectId);

            if (student == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }
            else if (subject == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }
            else
            {
                if (student.CoveredExams.Any(sce => sce == subjectId))
                {
                    return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
                }
                else 
                {
                    student.CoverExam(subject);

                    return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
                }
            }
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {students.Models.Where(s => s.University == university).Count()}");
            sb.AppendLine($"University vacancy: {university.Capacity - students.Models.Where(s => s.University == university).Count()}");

            return sb.ToString().TrimEnd();
        }
    }
}
