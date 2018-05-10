using System.Collections.Generic;
using System.Linq;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Core.Service
{

    //1   Em produção
    //2   Finalizada
    //3   Pendente
    //4   Suspensa
    // Uma vez[Pendente] a tarefa só pode ficar[Em produção]
    //Uma vez[Em produção] a tarefa só pode ser[suspensa] ou[finalizada]
    //Uma vez[Suspensa] a tarefa só pode voltar a[produção]
    //Uma vez[Finalizada] a tarefa completou o fluxo e nenhuma informação pode ser alterada(nem pelo master).

    public class TaskService : ServiceBase<TB_Task>, ITaskService
    {

        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
            : base(taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Validação das possibilidades de alteração de status
        /// </summary>
        /// <param name="iniStatus">Status inicial da tarefa</param>
        /// <param name="endStatus">Status final da tarefa</param>
        /// <returns></returns>
        public bool validateStatusUpdate(Enums.EnumTaskStatus? iniStatus, Enums.EnumTaskStatus? endStatus)
        {
            
            if (iniStatus == endStatus)
                return true;
            else if (iniStatus == Enums.EnumTaskStatus.Pendente && endStatus == Enums.EnumTaskStatus.Emproducao)
                return true;
            else if (iniStatus == Enums.EnumTaskStatus.Emproducao && (endStatus == Enums.EnumTaskStatus.Suspensa || endStatus == Enums.EnumTaskStatus.Finalizada))
                return true;
            else if (iniStatus == Enums.EnumTaskStatus.Suspensa && endStatus == Enums.EnumTaskStatus.Emproducao)
                return true;
            else
                return false;

        }

        public IEnumerable<TB_Task> GetTasksByUserID(int? userID)
        {
            return  this.GetAll().Where(t => t.userID == userID);
        }

        public IEnumerable<TB_TaskList> GetDepedencyTaskList(TB_Task task)
        {
            return _taskRepository.GetDepedencyTaskList(task);

            //IEnumerable<TB_TaskList> taskList =  task.TB_TaskList.Where(t => t.dependency == true);
            //return taskList.Where(t => (Enums.EnumTaskStatus)t.TB_Task.statusID != Enums.EnumTaskStatus.Finalizada);
        }

 
    }
}
