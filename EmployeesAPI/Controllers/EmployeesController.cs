﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesAPI.Models;

namespace EmployeesAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeByCompanyContext _context;

        public EmployeesController(EmployeeByCompanyContext context)
        {
            _context = context;
        }

        //// GET
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        //{
        //    return await _context.Employees.ToListAsync();
        //}

        //// GET
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(long id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        //// PUT
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(long id, Employee employee)
        //{
        //    if (id != employee.EmployeeID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        //// POST
        //[HttpPost]
        //public async Task<ActionResult<Employee>> Create(Employee employee)
        //{
        //    Company company = _context.Companies.Where(x => x.CompanyID == employee.CompanyID).FirstOrDefault();
        //    employee.Company = company;

        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.EmployeeID }, employee);
        //}

        //// DELETE
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Employee>> DeleteEmployee(long id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return employee;
        //}

        //private bool EmployeeExists(long id)
        //{
        //    return _context.Employees.Any(e => e.EmployeeID == id);
        //}
    }
}
