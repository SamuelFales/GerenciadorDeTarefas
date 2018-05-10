using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.Data.Model;

namespace TaskManager.Web.MVC.ViewModel
{
    public class TaskListViewModel
    {
        [Key]
        public int ID { get; set; }

       
        public Nullable<int> mainTaskID { get; set; }

        [Required]
        public Nullable<int> taskID { get; set; }

        [Required]
        [Display(Name = "Pré-requisito")]
        public bool requeriment { get; set; }

        [Required]
        [Display(Name = "Dependente")]
        public bool dependency { get; set; }

        [Display(Name = "Tarefa")]
        public TB_Task TB_Task { get; set; }

        [Display(Name = "Tarefa")]
        public TB_Task TB_Task1 { get; set; }

    }
}