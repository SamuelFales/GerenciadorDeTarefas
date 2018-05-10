using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.Data.Model;


namespace TaskManager.Web.MVC.ViewModel
{
    public class TaskViewModel
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Preencha o campo descrição")]
        [MinLength(5, ErrorMessage = "Minimo {0} caracteres")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public virtual TB_TaskStatus TB_TaskStatus { get; set; }

        [Required(ErrorMessage = "Preencha o campo Status")]
        public int StatusID { get; set; }

        [Display(Name = "Usuário")]
        public virtual TB_User TB_User { get; set; }

        [Display(Name = "Usuário")]
        public int? UserID { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Preencha o campo data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public Nullable<System.DateTime> Date { get; set; }
        
        public virtual IEnumerable<TB_TaskList> TB_TaskList { get; set; }

    }
}