using SudentCRUDOracleDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudentCRUDOracleDB.Interface
{
    public interface IStudentService
    {
        IEnumerable<Student> getAll();

        Student getById(int id);

        void CreateStudent(Student student);

        void UpdateStudent(Student student);

        void DeleteStudent(int id);
    }
}
