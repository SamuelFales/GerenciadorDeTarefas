using System.Collections.Generic;
using TaskManager.Data.Model;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface ITaskListRepository : IRepositoryBase<TB_TaskList>
    {
        IEnumerable<TB_TaskList> GetByMainTaskID(int mainTaskID);
        IEnumerable<TB_TaskList> GetByTaskID(int taskID);
        TB_TaskList GetByTaskListID(int taskListID);
    }
}
