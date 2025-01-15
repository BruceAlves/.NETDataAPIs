using Microsoft.Data.SqlClient;
using API.ADO.NET.Models;
using System.Collections.Generic;

namespace API.ADO.NET.Repository
{
    public class StudentsRepository
    {
        private readonly string _connectionString = "Server=DESKTOP-QMAB8K9\\SQLEXPRESS; Database=StudentAssignmt; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";

        public void Create(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Students (Name, Age, Address) VALUES (@Name, @Age, @Address)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Address", student.Address);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Student> ListAll()
        {
            var students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Age, Address FROM Students";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Address = reader.GetString(3)
                            };
                            students.Add(student);
                        }
                    }
                }
            }
            return students;
        }

        public Student FindById(int studentId)
        {
            const string query = "SELECT Id, Name, Age, Address FROM Students WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", studentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Address = reader.GetString(3)
                            };
                        }
                    }
                }
            }

            return new Student();
        }


        public void Edit(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Students SET Name = @Name, Age = @Age, Address = @Address WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Address", student.Address);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Students WHERE Id = @studentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@studentId", studentId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
