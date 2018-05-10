using System.Collections.Generic;
using System.Linq;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Data.Model;

namespace TaskManager.Data.Repositories
{
    public class TaskListRepository : RepositoryBase<TB_TaskList>, ITaskListRepository
    {
        public IEnumerable<TB_TaskList> GetByMainTaskID(int mainTaskID)
        {
            return Db.TB_TaskList.AsNoTracking().Where(t => t.mainTaskID == mainTaskID);
        }

        public IEnumerable<TB_TaskList> GetByTaskID(int taskID)
        {
            return Db.TB_TaskList.AsNoTracking().Where(t => t.taskID == taskID);
        }

        public TB_TaskList GetByTaskListID(int taskListID)
        {
            return Db.TB_TaskList.AsNoTracking().SingleOrDefault(t => t.ID == taskListID);
        }
    }
}
