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
using PagedList.Mvc;

namespace FiratBlog.Controllers
{
    public class AdminCategoryController : Controller
    {
        private BlogFiratDB db = new BlogFiratDB();


        public ActionResult Index(int page = 1, string Search = null)
        {
            try
            {
                if (Search != null)
                {
                    var aranan = db.Category.Where(m => m.CategoryName.Contains(Search)).ToList();
                    return View(aranan.OrderByDescending(m => m.CategoryId).ToPagedList(page, 100));
                }
                else
                {
                    var category = db.Category.OrderByDescending(m => m.CategoryId).ToPagedList(page, 4);
                    return View(category);
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
                Category category = db.Category.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName,CategoryExplanation")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Category.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(category);
            }
            catch
            {
                ViewBag.Basarisiz = "Kategori Eklenemedi !!!";
                return View();
            }
        }


        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Category category = db.Category.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,CategoryExplanation")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch
            {
                ViewBag.Basarisiz = "Güncelleme işlemi başarısız oldu !!";
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                Category category = db.Category.Find(id);
                db.Category.Remove(category);
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
