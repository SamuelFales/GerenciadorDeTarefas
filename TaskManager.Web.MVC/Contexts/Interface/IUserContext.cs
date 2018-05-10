using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Model;

namespace TaskManager.Web.MVC.Contexts.Interface
{
    public interface IUserContext
    {

        void AddUser(TB_User user);
        TB_User GetUser(string userName);
        TB_User GetUser(string userName, string password);
        void AddUserRole(TB_UserRoles userRole);
        bool IsUserInRole(string username, string roleName);
        string[] GetRolesForUser(string username);
        string[] GetAllRoles();
        string[] FindUsersInRole(string roleName, string usernameToMatch);


    }
}
