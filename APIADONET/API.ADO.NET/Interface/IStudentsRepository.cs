using API.ADO.NET.Models;

namespace API.ADO.NET.Interface
{
    public interface IStudentsRepository
    {
        void Edit(Student student);
        void Create(Student student);
        IEnumerable<Student> ListAll();
        Student FindById(int studentId);
        void Delete(int studentId);
    }
}
