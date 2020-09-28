using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.WebAPI.Models.Dtos
{
    public class CountryPutPostDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
