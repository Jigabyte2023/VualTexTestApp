using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaultexTestApp.Models;

namespace VaultexTestApp.ViewModels
{
    public class EmployeesViewModel
    {
        public Organisations organisations { get; set; }
        public Employees employees { get; set; }
    }
}
