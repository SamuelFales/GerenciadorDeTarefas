using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using TaskManager.Core.Service;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Repositories;
using TaskManager.Data.Repositories.Interfaces;


namespace TaskManager.Web.MVC.App_Start
{
    public class SimpleInjectorConfig
    {

        public static Container container;

        public static void RegisterComponents()  
        {
            container = new Container();


            container.Register<IUserService, UserService>(Lifestyle.Singleton);
            container.Register<IUserRespository, UserRepository>(Lifestyle.Singleton);

            container.Register<ITaskListService, TaskListService>(Lifestyle.Singleton);
            container.Register<ITaskListRepository, TaskListRepository>(Lifestyle.Singleton);

            container.Register<ITaskService, TaskService>(Lifestyle.Singleton);
            container.Register<ITaskRepository, TaskRepository>(Lifestyle.Singleton);

            container.Register<ITaskStatusService, TaskStatusService>(Lifestyle.Singleton);
            container.Register<ITaskStatusRepository, TaskStatusRepository>(Lifestyle.Singleton);

            //container.Register<IUserContext, UsersContext>(Lifestyle.Singleton);

            container.Register<IRolesService, RolesService>(Lifestyle.Singleton);
            container.Register<IRolesRepository, RolesRepository>(Lifestyle.Singleton);

            container.Register<IUserRolesService, UserRolesService>(Lifestyle.Singleton);
            container.Register<IUserRolesRepository, UserRolesRepository>(Lifestyle.Singleton);


            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

        }


        public static T GetInstance<T>() where T : class
        {
            return container.GetInstance<T>();
        }

    }
}