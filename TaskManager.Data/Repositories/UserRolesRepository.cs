using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Data.Repositories
{
    public class UserRolesRepository : RepositoryBase<TB_UserRoles>, IUserRolesRepository
    {

        public bool IsUserInRole(string username, string roleName)
        {

            var user = Db.TB_User.SingleOrDefault(u => u.name == username);
            if (user == null)
                return false;
            else
                return user.TB_UserRoles != null && user.TB_UserRoles.Select(u => u.TB_Roles).Any(r => r.RoleName == roleName);

            //var user = Users.SingleOrDefault(u => u.name == username);
            //if (user == null)
            //    return false;
            //return user.TB_UserRoles != null && user.TB_UserRoles.Select(u => u.TB_Roles).Any(r => r.RoleName == roleName);

        }


        public string[] GetRolesForUser(string username)
        {
            var user = Db.TB_User.SingleOrDefault(u => u.email == username);
            if (user == null)
                return new string[] { };
            return user.TB_UserRoles == null ? new string[] { } : user.TB_UserRoles.Select(u => u.TB_Roles).Select(u => u.RoleName).ToArray();
        }

    }
}
