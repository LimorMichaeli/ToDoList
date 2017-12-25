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
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TestController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/Test
        public IQueryable<Reminders> GetReminders()
        {
            return db.Reminders;
        }

        // GET: api/Test/5
        [ResponseType(typeof(Reminders))]
        public IHttpActionResult GetReminders(int id)
        {
            Reminders reminders = db.Reminders.Find(id);
            if (reminders == null)
            {
                return NotFound();
            }

            return Ok(reminders);
        }

        // PUT: api/Test/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReminders(int id, Reminders reminders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reminders.Id)
            {
                return BadRequest();
            }

            db.Entry(reminders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemindersExists(id))
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

        // POST: api/Test
        [ResponseType(typeof(Reminders))]
        public IHttpActionResult PostReminders(Reminders reminders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reminders.Add(reminders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reminders.Id }, reminders);
        }

        // DELETE: api/Test/5
        [ResponseType(typeof(Reminders))]
        public IHttpActionResult DeleteReminders(int id)
        {
            Reminders reminders = db.Reminders.Find(id);
            if (reminders == null)
            {
                return NotFound();
            }

            db.Reminders.Remove(reminders);
            db.SaveChanges();

            return Ok(reminders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RemindersExists(int id)
        {
            return db.Reminders.Count(e => e.Id == id) > 0;
        }
    }
}