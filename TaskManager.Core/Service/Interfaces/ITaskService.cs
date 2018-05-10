using System.Collections.Generic;
using TaskManager.Data.Model;

namespace TaskManager.Core.Service.Interfaces
{
    public interface ITaskService : IServiceBase<TB_Task>
    {
        bool validateStatusUpdate(Enums.EnumTaskStatus? iniStatus , Enums.EnumTaskStatus? endStatus);
        IEnumerable<TB_TaskList> GetDepedencyTaskList(TB_Task task);
        IEnumerable<TB_Task> GetTasksByUserID(int? userID);
    
       
    }
}
