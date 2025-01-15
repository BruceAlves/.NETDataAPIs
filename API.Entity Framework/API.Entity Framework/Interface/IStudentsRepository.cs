using API.Entity_Framework.Dtos;
using API.Entity_Framework.Models;

namespace API.Entity_Framework.Interface
{
    public interface IStudentsRepository
    {
        void Edit(int id, StudentDto studentDto);
        void Create(StudentDto student);
        IEnumerable<StudentDto> ListAll();
        StudentDto FindById(int studentId);
        void Delete(int studentId);
    }
}
