using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesAPI.Models;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;
using EmployeesAPI.Tools;

namespace EmployeesAPI.Controllers
{
    [Route("Company/[action]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly EmployeeByCompanyContext _context;

        public CompaniesController(EmployeeByCompanyContext context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CreateResponse>> Create(Company company, [FromHeader]string authorization)
        {
            if (Authenticate.AuthenticatedUser(authorization))
            {
                string validateInfo = Validate.EstablishmentYear(company.EstablishmentYear);
                if ( validateInfo == "OK")
                {
                    _context.Companies.Add(company);
                    await _context.SaveChangesAsync();
                    return new CreateResponse { Id = company.CompanyID };
                    //return CreatedAtAction("GetCompany", new { id = company.CompanyID }, company);
                }
                else
                {
                    return BadRequest(validateInfo);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int64 id, Company company, [FromHeader]string authorization)
        {
            if (Authenticate.AuthenticatedUser(authorization))
            {
                company.CompanyID = id;

                _context.Entry(company).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            else
            {
                return Unauthorized();
            }
        }



        // SEARCH
        [HttpPost]
        public ActionResult<SearchResponse> Search(SearchRequest search)
        {
            var allEmployees = _context.Employees.Where(x => (
            x.Company.Name.Contains(search.Keyword) &&
            x.DateOfBirth >= search.EmployeeDateOfBirthFrom &&
            x.DateOfBirth <= search.EmployeeDateOfBirthTo
            &&
            search.EmployeeJobTitles.Select(x => x.JobTitle.ToString()).ToList().Contains(x.JobTitle.ToString())
            ));
            List<string> ls = search.EmployeeJobTitles.Select(x => x.JobTitle.ToString()).ToList();

            var companiesID = allEmployees.Select(y => y.CompanyID).Distinct().ToList();

            SearchResponse searchResponse = new SearchResponse();
            searchResponse.Results = new List<SearchResponseCompany>();

            var companies = _context.Companies.Where(x => companiesID.Contains(x.CompanyID));

            foreach (var company in companies)
            {
                SearchResponseCompany searchResponseCompany = new SearchResponseCompany();
                searchResponseCompany.Name = company.Name;
                searchResponseCompany.EstablishmentYear = company.EstablishmentYear;
                List<SearchResponseEmployee> searchResponseEmployees = new List<SearchResponseEmployee>();

                var employees = allEmployees.Where(x => x.CompanyID == company.CompanyID).ToImmutableArray();

                foreach (var employee in employees)
                {
                    SearchResponseEmployee searchResponseEmployee = new SearchResponseEmployee();
                    searchResponseEmployee.FirstName = employee.FirstName;
                    searchResponseEmployee.LastName = employee.LastName;
                    searchResponseEmployee.DateOfBirth = employee.DateOfBirth.ToShortDateString();
                    searchResponseEmployee.JobTitle = employee.JobTitle;
                    searchResponseEmployees.Add(searchResponseEmployee);
                }
                searchResponseCompany.Employees = searchResponseEmployees;
                searchResponse.Results.Add(searchResponseCompany);
            }

            return searchResponse;
        }


        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> Delete(long id, [FromHeader]string authorization)
        {
            if (Authenticate.AuthenticatedUser(authorization))
            {
                var company = await _context.Companies.FindAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();

                return company;
            }
            else
            {
                return Unauthorized();
            }
        }

        private bool CompanyExists(long id)
        {
            return _context.Companies.Any(e => e.CompanyID == id);
        }

        // GET: api/Companies
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        //{
        //    return await _context.Companies.ToListAsync();
        //}

        // GET: api/Companies/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Company>> GetCompany(long id)
        //{
        //    var company = await _context.Companies.FindAsync(id);

        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    return company;
        //}
        //private Int64 GetNextEmployeeID()
        //{
        //    return _context.Employees.Max(x => x.EmployeeID);
        //}

        //private Int64 GetNextCompanyID()
        //{
        //    return _context.Companies.Max(x => x.CompanyID);
        //}

    }
}
