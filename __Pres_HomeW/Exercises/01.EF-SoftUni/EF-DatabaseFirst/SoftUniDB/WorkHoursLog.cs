//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftUniDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkHoursLog
    {
        public int WorkHoursLogID { get; set; }
        public int WorkHoursID { get; set; }
        public Nullable<int> OldEmployeeID { get; set; }
        public Nullable<int> NewEmployeeID { get; set; }
        public Nullable<System.DateTime> OldWorkHoursDate { get; set; }
        public Nullable<System.DateTime> NewWorkHoursDate { get; set; }
        public string OldTask { get; set; }
        public string NewTask { get; set; }
        public Nullable<double> OldHours { get; set; }
        public Nullable<double> NewHours { get; set; }
        public string OldComments { get; set; }
        public string NewComments { get; set; }
        public string Command { get; set; }
    }
}
