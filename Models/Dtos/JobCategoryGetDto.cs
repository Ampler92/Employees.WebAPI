using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.WebAPI.Models.Dtos
{
    public class JobCategoryGetDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
    }
}
