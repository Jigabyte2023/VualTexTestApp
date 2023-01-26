using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaultexTestApp.Models
{
    public class Organisations
    {
        [Required]
        [Key]
        [MaxLength(8)]
        public string OrganisationNumber { get; set; }
        [Required]
        [MaxLength(60)]
        public string OrganisationName { get; set; }
        [MaxLength(40)]
        public string AddressLine1 { get; set; }
        [MaxLength(40)]
        public string AddressLine2 { get; set; }
        [MaxLength(40)]
        public string AddressLine3 { get; set; }
        [MaxLength(40)]
        public string AddressLine4 { get; set; }
        [MaxLength(30)]
        public string Town { get; set; }
        [MaxLength(10)]
        public string PostCode { get; set; }
    }
}
