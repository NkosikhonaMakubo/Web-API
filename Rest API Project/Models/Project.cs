using System;
using System.Collections.Generic;

namespace Rest_API_Project.Models
{
    public partial class Project
    {
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? ProjectCreationDate { get; set; }
        public string? ProjectStatus { get; set; }
        public Guid? ClientId { get; set; }
        public virtual ICollection<JobTelemetry> JobTelemetries { get; set; } = new List<JobTelemetry>();
    }
}
