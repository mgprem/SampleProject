using BusinessEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Users
{
    public class UserModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        public UserTypes Type { get; set; }
        public int age { get; set; }
        [Required(ErrorMessage = "Annual Salary is required.")]

        public decimal? AnnualSalary { get; set; }
        //[Required(ErrorMessage = "At least one tag is required.")]
        //[MinLength(1, ErrorMessage = "At least one tag is required.")]
        [MinCollectionCount(1, ErrorMessage = "At least one tag is required.")]
        public IEnumerable<string> Tags { get; set; }
    }
}