using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Core;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Web.MVC.ViewModel;

namespace TaskManager.Web.MVC.Controllers
{
    public class TaskController : Controller
    {

        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly ITaskStatusService _taskStatusService;
        private readonly ITaskListService _taskListService;
  

        public TaskController(ITaskService taskService, IUserService userService, ITaskStatusService taskStatusService, ITaskListService taskListService)
        {
            _taskService = taskService;
            _userService = userService;
            _taskStatusService = taskStatusService;
            _taskListService = taskListService;
        }

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.UserID = GetUserIdentityID();
            var taskViewModel = Mapper.Map<IEnumerable<TB_Task>, IEnumerable<TaskViewModel>>(_taskService.GetAll());
            return View(taskViewModel);
        }

        [Authorize]
        public ActionResult Details(int id)
        {

            var taskViewModel = Mapper.Map<TB_Task, TaskViewModel>(_taskService.GetById(id));
            return View(taskViewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            if (User.IsInRole("Master"))
                ViewBag.UserID = new SelectList(_userService.GetAll(), "ID", "Name");
            else
                ViewBag.UserID = GetUserIdentityID().ToString();
            
            ViewBag.StatusID = new SelectList(_taskStatusService.GetAll(), "ID", "Status");

            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(TaskViewModel task)
        {

            if (!User.IsInRole("Master"))
            {
                task.UserID = GetUserIdentityID();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    //valida se usuário possui outras tarefas em produção.
                    bool userTasksInProduction = this.validateUserTasksInProduction(task.UserID, (Enums.EnumTaskStatus)task.StatusID);

                    if (userTasksInProduction)
                    {
                        ModelState.AddModelError("", "Cada usuário só pode ter uma tarefa [Em produção] por vez.");
                    }
                    //validação para UserID não consta na ViewModel.
                    else if (!task.UserID.HasValue)
                    {
                        ModelState.AddModelError("UserID", "Preencha o campo usuário.");
                    }
                    else
                    {
                        var newTask = Mapper.Map<TaskViewModel, TB_Task>(task);
                        _taskService.Add(newTask);

                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar tarefa. ERRO: "+ ex.Message);
            }

          
            if (User.IsInRole("Master"))
                ViewBag.UserID = new SelectList(_userService.GetAll(), "ID", "Name", task.UserID);
            else
                ViewBag.UserID = GetUserIdentityID().ToString();

            ViewBag.StatusID = new SelectList(_taskStatusService.GetAll(), "ID", "Status", task.StatusID);

            return View(task);
        }

       
        [Authorize]
        public ActionResult Edit(int id)
        {
            
            var task = _taskService.GetById(id);
            var taskViewModel = Mapper.Map<TB_Task, TaskViewModel>(task);

            ViewBag.UserID = new SelectList(_userService.GetAll(), "ID", "Name",task.userID);
            ViewBag.StatusID = new SelectList(_taskStatusService.GetAll(), "ID", "Status",task.statusID);

            return View(taskViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskViewModel task)
        {
            try
            {

                if (!User.IsInRole("Master"))
                {
                    task.UserID = GetUserIdentityID();
                }

                if (ModelState.IsValid)
                {

                    var editTask = _taskService.GetById(task.ID);

                    //tarefa finalizada não pode ser alterada.
                    if ((Enums.EnumTaskStatus)editTask.statusID == Enums.EnumTaskStatus.Finalizada)
                    {

                        //throw new Exception("Uma vez [Finalizada] a tarefa completou o fluxo e nenhuma informação pode ser alterada.");
                        ViewBag.UserID = new SelectList(_userService.GetAll(), "ID", "Name", task.UserID);
                        ViewBag.StatusID = new SelectList(_taskStatusService.GetAll(), "ID", "Status", task.StatusID);

                        ModelState.AddModelError("", "Uma vez [Finalizada] a tarefa completou o fluxo e nenhuma informação pode ser alterada.");
                        return View(task);
                    }

                        //valida se usuário possui outras tarefas em produção.
                    bool userTasksInProduction = this.validateUserTasksInProduction(task.UserID, (Enums.EnumTaskStatus)task.StatusID);

                    if (userTasksInProduction)
                    {
                        ViewBag.UserID = new SelectList(_userService.GetAll(), "ID", "Name", task.UserID);
                        ViewBag.StatusID = new SelectList(_taskStatusService.GetAll(), "ID", "Status", task.StatusID);

                        ModelState.AddModelError("", "Cada usuário só pode ter uma tarefa [Em produção] por vez.");
                        return View(task);
                    }

                    //valida se possui tarefas dependentes finalizadas para ir para produção
                    if ((Enums.EnumTaskStatus)task.StatusID == Enums.EnumTaskStatus.Emproducao)
                    {
                        IEnumerable<TB_TaskList> taskList = _taskService.GetDepedencyTaskList(editTask);
                        if (taskList.Any())
                        {
                            ViewBag.UserID = new SelectList(_userService.GetAll(), "ID", "Name", task.UserID);
                            ViewBag.StatusID = new SelectList(_taskStatusService.GetAll(), "ID", "Status", task.StatusID);

                            ModelState.AddModelError("", "Tarefas com dependências, precisam que as mesmas tenham sido [finalizadas] para poder iniciar [em produção].");
                            return View(task);
                        }
                    }

                    if (!_taskService.validateStatusUpdate((Enums.EnumTaskStatus)editTask.statusID, (Enums.EnumTaskStatus)task.StatusID))
                    {

                        ModelState.AddModelError("", "Mudança de status não permitida: " + 
                                                    Enums.GetDescription((Enums.EnumTaskStatus)editTask.statusID) + " para " +
                                                    Enums.GetDescription((Enums.EnumTaskStatus)task.StatusID)) ;
                    
                    }
                    else
                    {
                        
                        editTask.statusID = task.StatusID;
                        editTask.userID = task.UserID;

                        _taskService.Update(editTask);
                        return RedirectToAction("Index");

                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao alterar tarefa.  ERRO: " + ex.Message);
            }
            
            ViewBag.UserID = new SelectList(_userService.GetAll(), "ID", "Name", task.UserID);
            ViewBag.StatusID = new SelectList(_taskStatusService.GetAll(), "ID", "Status", task.StatusID);
            
            return View(task);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var task = _taskService.GetById(id);
            var taskViewModel = Mapper.Map<TB_Task, TaskViewModel>(task);

            return View(taskViewModel);
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(TaskViewModel task)
        {
            try
            {
                
                var deleteTask = _taskService.GetById(task.ID);
            
                bool checkDepAndReq = _taskListService.checkDependencyAndRequiriment(task.ID);

                if (checkDepAndReq)
                {
                    task = Mapper.Map<TB_Task , TaskViewModel>(deleteTask);
                    ModelState.AddModelError("", "Exclusão não permitida. Existem tarefas pendentes e|ou pré-requisitos para essa tarefa ou a mesma está associada a outras tarefas.");
                }
                else
                {
                    _taskService.Remove(deleteTask);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao deletar task. ERRO: " + ex.Message);
            }
            return View(task);
        }


        private int GetUserIdentityID()
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);

            return user.ID; 
        }
        /// <summary>
        /// Valida se existem tarefas em produção para o usuário ao alterar tarefa para produção.
        /// </summary>
        /// <param name="userID">usuário</param>
        /// <param name="statusID"></param>
        /// <returns></returns>
        private bool validateUserTasksInProduction(int? userID, Enums.EnumTaskStatus statusID)
        {

            if (statusID == Enums.EnumTaskStatus.Emproducao)
            {
                var tasksUserInProduction = _taskService.GetTasksByUserID(userID).Where(t => (Enums.EnumTaskStatus)t.statusID == Enums.EnumTaskStatus.Emproducao);

                if (tasksUserInProduction.Any())
                    return true;
            }

            return false;
        }


    }

    

}
