using API.Entity_Framework.Data;
using API.Entity_Framework.Dtos;
using API.Entity_Framework.Interface;
using API.Entity_Framework.Models;
using AutoMapper;

public class StudentsRepository : IStudentsRepository
{
    private readonly StudentContext _context;
    private readonly IMapper _mapper;

    public StudentsRepository(StudentContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Create(StudentDto studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public IEnumerable<StudentDto> ListAll()
    {
        var students = _context.Students.ToList();
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public StudentDto FindById(int studentId)
    {
        var student = _context.Students.Find(studentId);
        return _mapper.Map<StudentDto>(student);
    }

    public void Edit(int id, StudentDto studentDto)
    {
        var existingStudent = _context.Students.Find(id);
        if (existingStudent != null)
        {
            _mapper.Map(studentDto, existingStudent);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("Student not found.");
        }
    }
    public void Delete(int studentId)
    {
        var student = _context.Students.Find(studentId);
        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("Student not found.");
        }
    }
}
