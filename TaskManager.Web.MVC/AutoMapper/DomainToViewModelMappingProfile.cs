using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Data.Model;
using TaskManager.Web.MVC.ViewModel;
namespace TaskManager.Web.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {

        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            
            CreateMap<UserViewModel, TB_User>();
            CreateMap<TaskViewModel, TB_Task>();
            CreateMap<TaskListViewModel, TB_TaskList>();
        }

    }
}