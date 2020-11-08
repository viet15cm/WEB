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
    [RoutePrefix("Departments")]
    public class DepartmentsController : ApiController
    {
        private DbEmployee db = new DbEmployee();

        // GET: api/Departments
        [Route("")]
        public ICollection<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }

        // GET: api/Departments/5

        [ResponseType(typeof(Department))]
        public IHttpActionResult GetDepartment(string id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartment(string id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.IDDE)
            {
                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        [ResponseType(typeof(Department))]
        public IHttpActionResult PostDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(department.IDDE))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = department.IDDE }, department);
        }

        // DELETE: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(string id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(string id)
        {
            return db.Departments.Count(e => e.IDDE == id) > 0;
        }
    }
}