using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FiratBlog.Models;
using PagedList;

namespace FiratBlog.Controllers
{
    public class AdminContactController : Controller
    {
        private BlogFiratDB db = new BlogFiratDB();


        public ActionResult Index(int page = 1, string Search = null)
        {
            try
            {
                if (Search != null)
                {
                    var aranan = db.Contact.Where(m => m.NameSurname.Contains(Search)).ToList();
                    return View(aranan.OrderByDescending(m => m.ContactId).ToPagedList(page, 100));
                }
                else
                {
                    var contact = db.Contact.OrderByDescending(m => m.ContactId).ToPagedList(page, 4);
                    return View(contact);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public ActionResult Details(int id)
        {
            try
            {
                Contact contact = db.Contact.Find(id);
                if (contact == null)
                {
                    return HttpNotFound();
                }
                return View(contact);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                Contact contact = db.Contact.Find(id);
                db.Contact.Remove(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Basarisiz = "Silme işlemi başarısız oldu !!";
                return View();
            }
        }
    }
}
