//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagement.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lecture
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public System.DateTime ScheduleDate { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public bool isDeleted { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int BatchId { get; set; }
    
        public virtual Batch Batch { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
