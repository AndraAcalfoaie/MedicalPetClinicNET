using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
