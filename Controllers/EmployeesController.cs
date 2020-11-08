using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WEBAPI.DBContextLayer;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [RoutePrefix("Employees")]
    public class EmployeesController : ApiController
    {
        private DbEmployee db = new DbEmployee();

        // GET: api/Employees
        [Route("")]
        public ICollection<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }
        [Route("JoinEmployeeDepartment")]
        public IHttpActionResult GetJoinEmployeeDepartment()
        {
            var Join = from em in db.Employees
                       join de in db.Departments on em.IDDE equals de.IDDE
                       select new
                       {
                           IDEP = em.ID,
                           NameEmployee = em.Name,
                           Age = em.Age,
                           Addrees = em.Addrees,
                           NameDepartment= de.Name,



                       };

            return Ok(Join.ToList());
        }
        
        [Route("GetEmployee/{id}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(string id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5

        [Route("PutEmployee/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(string id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.ID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [Route("PostEmployee")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            employee.ID = GetIdentity();
            db.Employees.Add(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/Employees/5
        [Route("DeleteEmployee/{id}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(string id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(string id)
        {
            return db.Employees.Count(e => e.ID == id) > 0;
        }

        public string GetIdentity()
        {
            string ID = "";
            using (var entity = new DbEmployee())
            {

                var list = entity.Employees.ToList();
                if (list.Count == 0)
                    ID = "NV00000000";
                else
                {
                    int temp;
                    ID = "NV";
                    temp = Convert.ToInt32(list[list.Count - 1].ID.ToString().Substring(2, 8));
                    temp = temp + 1;
                    if (temp < 10)

                        ID = ID + "0000000";
                    else if (temp < 100)
                        ID = ID + "000000";
                    else if (temp < 1000)
                        ID = ID + "00000";
                    else if (temp < 10000)
                        ID = ID + "0000";
                    else if (temp < 100000)
                        ID = ID + "000";
                    else if (temp < 1000000)
                        ID = ID + "00";
                    else if (temp < 10000000)
                        ID = ID + "0";

                    ID = ID + temp.ToString();
                }
                return ID;

            }
        }
    }
}