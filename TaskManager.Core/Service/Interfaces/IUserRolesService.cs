using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Model;

namespace TaskManager.Core.Service.Interfaces
{
    public interface IUserRolesService : IServiceBase<TB_UserRoles>
    {
        bool IsUserInRole(string username, string roleName);
        string[] GetRolesForUser(string username);
       
    }
}
