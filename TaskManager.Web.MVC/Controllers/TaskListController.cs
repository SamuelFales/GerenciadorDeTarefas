using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Web.MVC.ViewModel;

namespace TaskManager.Web.MVC.Controllers
{
    public class TaskListController : Controller
    {
        private readonly ITaskListService _taskListService;
        private readonly ITaskService _taskService;

        public TaskListController(ITaskListService taskListService, ITaskService taskService)
        {
            _taskListService = taskListService;
            _taskService = taskService;
        }

        // GET: TaskList
        [Authorize]
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            var taskListViewModel = Mapper.Map<IEnumerable<TB_TaskList>, IEnumerable<TaskListViewModel>>(_taskListService.GetByMainTaskID(id));
            return View(taskListViewModel);
        }

        // GET: TaskList/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaskList/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            ViewBag.taskID = new SelectList(_taskService.GetAll().Where(t => t.ID != id) , "ID", "description");
            ViewBag.id = id;
            return View();
        }

        // POST: TaskList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskListViewModel taskList)
        {
            taskList.mainTaskID = taskList.ID;

            try
            {
                if (ModelState.IsValid)
                {

                    if (taskList.dependency == taskList.requeriment)
                    {
                        ModelState.AddModelError("", "Favor selecionar dependência OU pré-requisito. ");
                    }
                    else
                    {
                        var newTaskList = Mapper.Map<TaskListViewModel, TB_TaskList>(taskList);
                        _taskListService.Add(newTaskList);

                        return RedirectToAction("Index", new { id = taskList.mainTaskID });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao cadastrar usuário. ERRO: " + ex.Message);
            }

            ViewBag.taskID = new SelectList(_taskService.GetAll(), "ID", "description", taskList.taskID);
            ViewBag.mainTaskID = taskList.mainTaskID;
            return View(taskList);
        }

        // GET: TaskList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskList/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {

             var task = _taskListService.GetByTaskListID(id);
             var taskListViewModel = Mapper.Map<TB_TaskList, TaskListViewModel>(task);
             ViewBag.mainTaskID = task.mainTaskID;
           
            return View(taskListViewModel);
        }

        // POST: TaskList/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(TaskListViewModel taskList)
        {
            try
            {
                var deleteTask = _taskListService.GetById(taskList.ID);
                ViewBag.mainTaskID = deleteTask.mainTaskID;

                Mapper.Map(taskList, deleteTask);
                _taskListService.Remove(deleteTask);

                return RedirectToAction("Index", new { id = ViewBag.mainTaskID } );
                //TempData["success"] = "Tarefa desassociada com sucesso.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","Erro ao deletar usuário. ERRO: " + ex.Message);    
            }
            return View(taskList);
        }
    }
}
