using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmployeesAPI.Models
{
    public class Employee
    {
        public Int64 EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitleType JobTitle { get; set; }
        public Int64? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }
    }

    public enum JobTitleType
    {
        [Display(Name = "Unknown")]
        Unknown = 0,
        [Display(Name = "Administrator")]
        Administrator = 1,
        [Display(Name = "Developer")]
        Developer = 2,
        [Display(Name = "Architect")]
        Architect = 3,
        [Display(Name = "Manager")]
        Manager = 4
    }

    public class JobTitleString
    {
        public string JobTitle { get; set; }
    }

    public class Company
    {
        [Required]
        public Int64 CompanyID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }
        [Required]
        public ICollection<Employee> Employees { get; set; }
    }

    public class CreateResponse
    {
        public Int64 Id { get; set; }
    }

    public class SearchRequest
    {
        [Required]
        public string Keyword { get; set; }
        [Required]
        public DateTime EmployeeDateOfBirthFrom { get; set; }
        [Required]
        public DateTime EmployeeDateOfBirthTo { get; set; }
        [Required]
        public ICollection<JobTitleString> EmployeeJobTitles { get; set; }
    }

    public class SearchResponseEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public JobTitleType JobTitle { get; set; }
    }

    public class SearchResponseCompany
    {
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public ICollection<SearchResponseEmployee> Employees { get; set; }
    }

    public class SearchResponse
    {
        public ICollection<SearchResponseCompany> Results { get; set; }
    }
}
