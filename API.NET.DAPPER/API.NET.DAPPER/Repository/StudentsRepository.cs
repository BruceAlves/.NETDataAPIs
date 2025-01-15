using API.NET.DAPPER.Interface;
using API.NET.DAPPER.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace API.NET.DAPPER.Repository
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly string? _connectionString;

        public StudentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("A string de conexão 'DefaultConnection' não pode ser nula ou vazia.", nameof(configuration));
            }
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public void Create(Student student)
        {
            const string query = "INSERT INTO Students (Name, Age, Address) VALUES (@Name, @Age, @Address)";
            using (var connection = CreateConnection())
            {
                connection.Execute(query, student);
            }
        }

        public void Delete(int studentId)
        {
            const string query = "DELETE FROM Students WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                connection.Execute(query, new { Id = studentId });
            }
        }

        public void Edit(Student student)
        {
            const string query = "UPDATE Students SET Name = @Name, Age = @Age, Address = @Address WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                connection.Execute(query, student);
            }
        }

        public Student FindById(int studentId)
        {
            const string query = "SELECT Id, Name, Age, Address FROM Students WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var student = connection.QuerySingleOrDefault<Student>(query, new { Id = studentId });
                if (student == null)
                {
                    throw new KeyNotFoundException("Estudante não encontrado.");
                }

                return student;
            }
        }


        public IEnumerable<Student> ListAll()
        {
            const string query = "SELECT Id, Name, Age, Address FROM Students";
            using (var connection = CreateConnection())
            {
                return connection.Query<Student>(query).ToList();
            }
        }
    }
}
