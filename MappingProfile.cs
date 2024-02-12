using AutoMapper;
using LawSchool.Models;
using LawSchool.ModelsDto;

namespace LawSchool;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDTO>()
        .ForCtorParam("FullName", opt => opt.MapFrom(s => string.Join(' ', s.FirstName, s.LastName)));
    }
}