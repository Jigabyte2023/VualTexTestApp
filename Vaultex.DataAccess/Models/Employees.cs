using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaultexTestApp.Models
{
    public class Employees
    {
        [Required]
        [Key]
        public int PK_Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string OrganisationNumber { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
    }
}
