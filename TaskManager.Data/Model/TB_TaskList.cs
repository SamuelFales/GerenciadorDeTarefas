//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManager.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_TaskList
    {
        public int ID { get; set; }
        public Nullable<int> mainTaskID { get; set; }
        public Nullable<int> taskID { get; set; }
        public Nullable<bool> requeriment { get; set; }
        public Nullable<bool> dependency { get; set; }
    
        public virtual TB_Task TB_Task { get; set; }
        public virtual TB_Task TB_Task1 { get; set; }
    }
}
