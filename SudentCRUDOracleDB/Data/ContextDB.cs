using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SudentCRUDOracleDB.Models;
using System.Data.OracleClient;


namespace SudentCRUDOracleDB.Data
{
    public class ContextDB
    {

        private readonly OracleConnection conn = new
          (
            "Data Source=" +
                "(DESCRIPTION=" +
                    "(ADDRESS_LIST=" +
                        "(ADDRESS=" +
                            "(PROTOCOL=TCP)" +
                                "(HOST=localhost)" +
                                    "(PORT=1521)))" +
                                        "(CONNECT_DATA = " +
                                            "(SERVER = DEDICATED)" +
                                                "(SERVICE_NAME = xepdb1))); " +
                                                    "User Id = System;" +
                                                        "Password=1203;"
                                                             );


        public List<Student> Shov()
        {
            List<Student> students = new();
            DataTable table = null;



            string query = "SELECT * FROM students";
            conn.Open();

            try
            {
                using (OracleCommand cmd = new(query, conn))
                {
                    using (OracleDataAdapter da = new(cmd))
                    {
                        table = new DataTable();
                        da.Fill(table);
                    }
                }
            }
            catch { }
            finally { conn.Close(); }

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Student student = new(row);
                    students.Add(student);
                }
            }
            return students;

        }

        public Student GetById(int id)
        {
            Student table = null;
            Student student = null;

            try
            {
                conn.Open();


                using (OracleCommand cmd = new OracleCommand("getById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", "Int32").Value = id;
                    cmd.Parameters.Add("id", "Int32").Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("name", "Varchar2").Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("sname", "Varchar2").Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("email", "Varchar2").Direction = ParameterDirection.Output;
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        table = new Student()
                        {
                            id = Convert.ToInt32(cmd.Parameters["id"].Value),
                            Name = (cmd.Parameters["name"].Value).ToString(),
                            Sname = (cmd.Parameters["sname"].Value).ToString(),
                            Email = (cmd.Parameters["email"].Value).ToString()
                        };

                    }
                }
            }
            catch { }
            finally { conn.Close(); }

            if (table != null && table.Name != null)
            {
                student = table;

            }

            return student;
        }

        public void Create(Student student)
        {
            if (student != null)
            {
                try
                {
                    conn.Open();
                    string query = $"INSERT INTO students VALUES ((SELECT MAX(id)+1 FROM students),'{student.Name}','{student.Sname}','{student.Email}')";

                    using (OracleCommand cmd = new(query, conn))
                    {
                        var a = cmd.ExecuteNonQuery();
                    }
                }
                catch { }
                finally { conn.Close(); }
            }
        }

        public void Update(Student student)
        {
            if (student != null)
            {
                try
                {
                    conn.Open();
                    string query = $"UPDATE students SET id={student.id},name='{student.Name}',sname='{student.Sname}',email='{student.Email}' WHERE id='{student.id}'";

                    using (OracleCommand cmd = new(query, conn))
                    {
                        var a = cmd.ExecuteNonQuery();
                    }
                }
                catch { }
                finally { conn.Close(); }
            }
        }

        public void Remove(int id)
        {

            try
            {
                conn.Open();
                string query = $"DELETE FROM students WHERE id='{id}'";

                using (OracleCommand cmd = new(query, conn))
                {
                    var a = cmd.ExecuteNonQuery();
                }
            }
            catch { }
            finally { conn.Close(); }

        }



    }
}
