using Application.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

        CreateMap<TaskItem, TaskViewModel>();
    }
}