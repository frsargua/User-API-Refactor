using System;
using System.ComponentModel.DataAnnotations;

namespace Tests.User.Api.Data
{
	public class UpdateRequestVM
	{
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name must be less than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(0, 150, ErrorMessage = "Age must be between 0 and 150")]
        public int Age { get; set; }
    }
}

