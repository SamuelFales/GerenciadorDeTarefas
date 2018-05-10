using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Core.Service
{
    public class TaskListService : ServiceBase<TB_TaskList>, ITaskListService
    {

        private readonly ITaskListRepository _taskListRepository;

        public TaskListService(ITaskListRepository taskListRepository)
            : base(taskListRepository)
        {
            _taskListRepository = taskListRepository;
        }

        public IEnumerable<TB_TaskList> GetByMainTaskID(int mainTaskID)
        {
            return _taskListRepository.GetByMainTaskID(mainTaskID);
        }

        public IEnumerable<TB_TaskList> GetByTaskID(int taskID)
        {
            return _taskListRepository.GetByTaskID(taskID);
        }

        public bool checkDependencyAndRequiriment( int taskID)
        {
            if (this.GetByMainTaskID(taskID).Any() || this.GetByTaskID(taskID).Any())
                return true;

            return false;
            
        }

        public TB_TaskList GetByTaskListID(int taskListID)
        {
            return _taskListRepository.GetByTaskListID(taskListID);   
        }


    }
}
