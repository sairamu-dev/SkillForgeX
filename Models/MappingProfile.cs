using SkillForgeX.ViewModels;
using SkillForgeX.Repository_pattern.Core.Enitities;
using SkillForgeX.Areas.Admin.ViewModels;
using SkillForgeX.Areas.Developer.ViewModels;
using SkillForgeX.Areas.Manager.ViewModels;
using AutoMapper;

namespace SkillForgeX.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UsersViewModel>().ReverseMap();
            CreateMap<Tasks, CreateTaskViewmodel>().ReverseMap();
            CreateMap<Tasks, TasksViewModel>().ReverseMap();
            CreateMap<Tasks, UserTaskViewModel>().ReverseMap();
            CreateMap<Tasks, TaskViewModel>().ReverseMap(); // admin
            CreateMap<Tasks, EditTaskViewModel>().ReverseMap();
            CreateMap<Feedback, FeedbackViewModel>().ReverseMap();
            CreateMap<ApiViewmodel, Api>().ReverseMap();
        }
    }
}
