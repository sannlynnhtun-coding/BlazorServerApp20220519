using BlazorServerApp20220519.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp20220519.Services
{
    public class DbService
    {
        private readonly IConfiguration _configuration;
        private readonly string conStr;

        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
            conStr = _configuration.GetConnectionString("TestDbStr");
        }

        /// <summary>
        /// ADO .Net
        /// </summary>
        /// <returns></returns>
        public List<StudentModel> GetStudents()
        {
            List<StudentModel> lst = new List<StudentModel>();
            SqlConnection connection = new SqlConnection(conStr);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from tblstudent", connection);
            command.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var item = dataTable.Rows[i];
                StudentModel studentModel = new StudentModel();
                studentModel.StudentId = Convert.ToInt32(item["StudentId"]);
                studentModel.StudentCode = Convert.ToString(item["StudentCode"]);
                studentModel.StudentName = Convert.ToString(item["StudentName"]);
                lst.Add(studentModel);
            }
            return lst;
        }

        /// <summary>
        /// EF Core (Entity Framework Core)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<StudentModel> GetStudents2(int id)
        {
            EntityFrameworkService db = new EntityFrameworkService();
            return db.Students.Where(x => x.StudentId == id).ToList();
        }

        public StudentModel GetStudents(int id)
        {
            EntityFrameworkService db = new EntityFrameworkService();
            return db.Students.Where(x => x.StudentId == id).FirstOrDefault();
        }

        public int AddStudent(StudentModel item)
        {
            EntityFrameworkService db = new EntityFrameworkService();
            db.Students.Add(item);
            return db.SaveChanges();
        }

        public int UpdateStudent(StudentModel item)
        {
            EntityFrameworkService db = new EntityFrameworkService();
            db.Students.Update(item);
            return db.SaveChanges();
        }

        public int DeleteStudent(int id)
        {
            EntityFrameworkService db = new EntityFrameworkService();
            //var item = db.Students.Where(x => x.StudentId == id).FirstOrDefault();
            //db.Students.Remove(item);
            db.Database.ExecuteSqlRaw(SqlQueryList.DeleteStudent(id));
            return db.SaveChanges();
        }

        /// <summary>
        /// Dapper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<StudentModel> GetStudents3(int id)
        {
            using (var db = new SqlConnection(conStr))
            {
                var lst = db.Query<StudentModel>(
                    "select * from tblstudent where StudentId = @StudentId",
                    new { StudentId = id }).ToList();
                return lst;
            }
        }
    }
}
