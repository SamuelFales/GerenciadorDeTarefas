using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Web.MVC.App_Start;
using TaskManager.Web.MVC.Contexts.Interface;

namespace TaskManager.Web.MVC.Contexts
{
    public class UsersContext : DbContext, IUserContext
    {
       
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;
        private readonly IUserRolesService _userRolesService;

        public UsersContext()
        {

            _userService = SimpleInjectorConfig.GetInstance<IUserService>();
            _rolesService = SimpleInjectorConfig.GetInstance<IRolesService>();
            _userRolesService = SimpleInjectorConfig.GetInstance<IUserRolesService>();
        }


        public void AddUser(TB_User user)
        {
            
        }

        public TB_User GetUser(string userName)
        {
            return _userService.GetUserByName(userName);
        }

        public TB_User GetUser(string userName, string password)
        {
            return _userService.GetUserByNameAndPassword(userName, password);
        }

        public void AddUserRole(TB_UserRoles userRole)
        {
         
        }


        public bool IsUserInRole(string username, string roleName)
        {
            return _userRolesService.IsUserInRole(username, roleName);
        }


        public string[] GetRolesForUser(string username)
        {

            return _userRolesService.GetRolesForUser(username);
        }

        public string[] GetAllRoles()
        {
            return _rolesService.GetAllRoles();
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch) { return null; }

    }
}
