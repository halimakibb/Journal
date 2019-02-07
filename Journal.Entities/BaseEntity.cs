using System;
using System.ComponentModel.DataAnnotations;

namespace Journal.Entities
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}