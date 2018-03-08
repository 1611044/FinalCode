using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DogWash.DAL;
using DogWash.Models;
using Microsoft.AspNet.Identity;

namespace DogWash.Controllers
{
    public class WashesController : Controller
    {
        private DogWashcontext db = new DogWashcontext();

        // GET: Washes
        public ActionResult Index()
        {
            var uid = User.Identity.GetUserId();
            var draft = db.Washes.Where(d => d.owner == uid);
            return View(draft);

        }

        // GET: Washes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wash wash = db.Washes.Find(id);
            if (wash == null)
            {
                return HttpNotFound();
            }
            return View(wash);
        }

        // GET: Washes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Washes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Time,owner,pet,washtype")] Wash wash)
        {
            String num = "";
            var id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                foreach (Owner o in db.Owners)
                {
                    if (id == o.owner)
                    {
                        num = o.phonenumber;
                    }
                }

                
   
                System.Diagnostics.Debug.WriteLine("Here");
                System.Diagnostics.Debug.WriteLine(num);
                String justDigits = new String(num.Where(char.IsDigit).ToArray());
                System.Diagnostics.Debug.WriteLine(justDigits);
                String Message = "Dog%20Appointment@"+wash.Time;
                String url = "https://api.zipwhip.com/message/send?session=859f80b2-5576-4a8e-b43a-ef44661d6803:345142302&contact=" + justDigits + "&body=" + Message;
                Uri targetUri = new Uri(url);
                System.Diagnostics.Debug.WriteLine(url);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetUri);
                var response = request.GetResponse() as HttpWebResponse;
                System.Diagnostics.Debug.WriteLine(response);


                wash.owner = id ;
                db.Washes.Add(wash);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(wash);
        }

        // GET: Washes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wash wash = db.Washes.Find(id);
            if (wash == null)
            {
                return HttpNotFound();
            }
            return View(wash);
        }

        // POST: Washes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Time,owner,pet,washtype")] Wash wash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wash);
        }

        // GET: Washes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wash wash = db.Washes.Find(id);
            if (wash == null)
            {
                return HttpNotFound();
            }
            return View(wash);
        }

        // POST: Washes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wash wash = db.Washes.Find(id);
            db.Washes.Remove(wash);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
