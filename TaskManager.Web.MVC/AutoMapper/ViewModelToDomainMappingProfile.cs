using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskManager.Web.MVC.ViewModel;
using TaskManager.Data.Model;

namespace TaskManager.Web.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {

        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public ViewModelToDomainMappingProfile()
        {

            CreateMap<TB_User, UserViewModel>();
            CreateMap<TB_Task, TaskViewModel>();
            CreateMap<TB_TaskList, TaskListViewModel>();
        }


    }
}