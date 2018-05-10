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
    public class RolesService : ServiceBase<TB_Roles>, IRolesService
    {

        private readonly IRolesRepository _rolesRepository;

        public RolesService(IRolesRepository rolesRepository)
            : base(rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public string[] GetAllRoles()
        {
            IEnumerable<TB_Roles> roles = _rolesRepository.GetAll();
            return roles.Select(r => r.RoleName).ToArray();
        }

    }
   
    
}
