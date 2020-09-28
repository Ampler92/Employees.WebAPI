using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.WebAPI.Models
{
    public class JobCategory : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
    }
}
