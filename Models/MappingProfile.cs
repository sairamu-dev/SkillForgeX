using DevTaskFlow.ViewModels;
using DevTaskFlow.Repository_pattern.Core.Enitities;
using DevTaskFlow.Areas.Admin.ViewModels;
using DevTaskFlow.Areas.Developer.ViewModels;
using DevTaskFlow.Areas.Manager.ViewModels;
using AutoMapper;

namespace DevTaskFlow.Models
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
