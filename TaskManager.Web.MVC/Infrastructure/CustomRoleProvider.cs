using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Web.MVC.App_Start;


namespace CustomMembershipEF.Infrastructure
{
    public class CustomRoleProvider : RoleProvider
    {


        private readonly IUserRolesService _userRolesService;
        private readonly IRolesService _rolesService;

        public CustomRoleProvider()
        {
            _userRolesService = SimpleInjectorConfig.GetInstance<IUserRolesService>();
            _rolesService = SimpleInjectorConfig.GetInstance<IRolesService>();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            
            return _userRolesService.IsUserInRole(username, roleName);
            
        }

        public override string[] GetRolesForUser(string username)
        {

            return _userRolesService.GetRolesForUser(username);
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {

            return _rolesService.GetAllRoles();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}