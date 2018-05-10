using System.Collections.Generic;
using TaskManager.Data;
using TaskManager.Data.Model;

namespace TaskManager.Core.Service.Interfaces
{
    public interface ITaskListService : IServiceBase<TB_TaskList>
    {
        IEnumerable<TB_TaskList> GetByMainTaskID(int mainTaskID);
        IEnumerable<TB_TaskList> GetByTaskID(int taskID);
        bool checkDependencyAndRequiriment(int taskID);
        TB_TaskList GetByTaskListID(int taskListID);



    }
}
