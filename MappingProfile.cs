using AutoMapper;
using LawSchool.Models;
using LawSchool.ModelsDto;

namespace LawSchool;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDto>()
        .ForMember(st => st.FullName,
            opt => opt.MapFrom(s => string.Join(' ', s.FirstName, s.LastName)));

        CreateMap<StudentForCreationDto, Student>();
        CreateMap<StudentForUpdateDto, Student>().ReverseMap();
    }
}