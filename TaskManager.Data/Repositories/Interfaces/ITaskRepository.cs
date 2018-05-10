using System.Collections.Generic;
using TaskManager.Data.Model;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface ITaskRepository : IRepositoryBase<TB_Task>
    {
        IEnumerable<TB_Task> GetTasksByUserID(int? userID);
        IEnumerable<TB_TaskList> GetDepedencyTaskList(TB_Task task);
      
    }
}
