using SudentCRUDOracleDB.Data;
using SudentCRUDOracleDB.Interface;
using SudentCRUDOracleDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudentCRUDOracleDB.Services
{
    public class StudentService : IStudentService
    {
        private readonly ContextDB _context;

        public StudentService(ContextDB context)
        {
            _context = context;
        }

        public void CreateStudent(Student student)
        {
            _context.Create(student);
        }

        public void DeleteStudent(int id)
        {
            _context.Remove(id);
        }

        public IEnumerable<Student> getAll()
        {
            IEnumerable<Student> students = _context.Shov();

            return students;
        }

        public Student getById(int id)
        {
            Student student = _context.GetById(id);

            return student;
        }

        public void UpdateStudent(Student student)
        {
            _context.Update(student);
        }
    }
}
