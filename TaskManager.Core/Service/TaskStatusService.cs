using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Core.Service
{
    public class TaskStatusService : ServiceBase<TB_TaskStatus>, ITaskStatusService
    {

        private readonly ITaskStatusRepository _taskStatusRepository;

        public TaskStatusService(ITaskStatusRepository taskStatusRepository)
            : base(taskStatusRepository)
        {
            _taskStatusRepository = taskStatusRepository;
        }
        
    }
}
