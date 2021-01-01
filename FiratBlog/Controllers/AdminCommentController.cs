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
    public class AdminCommentController : Controller
    {
        private BlogFiratDB db = new BlogFiratDB();


        public ActionResult Index(int page = 1, string Search = null)
        {
            try
            {
                if (Search != null)
                {
                    var aranan = db.Comment.Where(m => m.Member.UserName.Contains(Search)).ToList();
                    return View(aranan.OrderByDescending(m => m.MemberId).ToPagedList(page, 100));
                }
                else
                {
                    var comment = db.Comment.OrderByDescending(c => c.CommentId).ToPagedList(page, 4);
                    return View(comment);
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
                Comment comment = db.Comment.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                return View(comment);
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
                Comment comment = db.Comment.Find(id);
                db.Comment.Remove(comment);
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
