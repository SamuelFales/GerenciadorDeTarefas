using System.Collections.Generic;
using System.Linq;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Data.Repositories
{
    public class TaskRepository : RepositoryBase<TB_Task>, ITaskRepository
    {

        public IEnumerable<TB_Task> GetTasksByUserID(int? userID)
        {
            return Db.TB_Task.Where(t => t.userID == userID);      
        }

        public IEnumerable<TB_TaskList> GetDepedencyTaskList(TB_Task task)
        {
            IEnumerable<TB_TaskList> taskList =  task.TB_TaskList.Where(t => t.dependency == true);
            return taskList.Where(t => t.TB_Task1.statusID != 2);
        }

       
    }
}
