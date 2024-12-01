using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace API.Models
{
    public class Graduate
    {
        public Guid GraduateId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Age
        {
            get
            {
                var ageInDays = (DateTime.Now - DateOfBirth).TotalDays;
                int age = (int)(ageInDays / 365); 
                return age;
            }
        }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateEdited { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } 
    }
}
