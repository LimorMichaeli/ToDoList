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
    public class RemindersController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/Reminders
        public IQueryable<Reminders> GetReminders()
        {
            return db.Reminders;
        }

        // PUT: api/Reminders/5
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

        // POST: api/Reminders
        [ResponseType(typeof(Reminders))]
        public IHttpActionResult PostReminders(Reminders reminders)
        {
            if (reminders == null || !ModelState.IsValid)
            {
                return Ok(new { @success = false, @error = "invalid model" });
            }

            db.Reminders.Add(reminders);
            db.SaveChanges();

            return Ok(new { @success = true });
        }

        // DELETE: api/Reminders/5
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