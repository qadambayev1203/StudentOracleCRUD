using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SudentCRUDOracleDB.Models
{
    public class Student
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Sname { get; set; }

        public string Email { get; set; }


        public Student(DataRow row)
        {
            id = int.Parse("" + row["id"]);
            Name = row["name"].ToString();
            Sname = row["sname"].ToString();
            Email = row["email"].ToString();
        }
        public Student()
        {

        }
    }

   
}
