using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Core.Service
{
    public class UserRolesService : ServiceBase<TB_UserRoles>, IUserRolesService
    {

        private readonly IUserRolesRepository _userRolesRepository;

        public UserRolesService(IUserRolesRepository userRolesRepository)
            : base(userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;

        }

      
        public bool IsUserInRole(string username, string roleName)
        {

            return _userRolesRepository.IsUserInRole(username, roleName);
          

        }


        public string[] GetRolesForUser(string username)
        {
            return _userRolesRepository.GetRolesForUser(username);
        }

    }
}
