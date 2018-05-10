using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Model;

namespace TaskManager.Core.Service.Interfaces
{
    public interface IRolesService : IServiceBase<TB_Roles>
    {
        string[] GetAllRoles();
       
    }
}
