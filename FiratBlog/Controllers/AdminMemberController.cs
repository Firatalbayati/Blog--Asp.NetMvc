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
using System.Web.Helpers;
using System.IO;

namespace FiratBlog.Controllers
{
    public class AdminMemberController : Controller
    {
        private BlogFiratDB db = new BlogFiratDB();


        public ActionResult Index(int page = 1, string Search = null)
        {
            try
            {
                if (Search != null)
                {
                    var aranan = db.Member.Where(m => m.UserName.Contains(Search)).ToList();
                    return View(aranan.OrderByDescending(m => m.MemberId).ToPagedList(page, 100));
                }
                else
                {
                    var member = db.Member.Include(m => m.Authority);
                    return View(member.OrderByDescending(u => u.MemberId).ToPagedList(page, 4));
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Member member = db.Member.Find(id);
                if (member == null)
                {
                    return HttpNotFound();
                }
                return View(member);
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
        public ActionResult Create(Member member, HttpPostedFileBase Photo)
        {
            try
            {
                if (Photo != null)
                {
                    WebImage image = new WebImage(Photo.InputStream);
                    FileInfo imageInfo = new FileInfo(Photo.FileName);

                    string newfoto = Guid.NewGuid().ToString() + imageInfo.Extension;
                    image.Resize(160, 160);
                    image.Save("~/Upload/MemberPhoto/" + newfoto);
                    member.Photo = "/Upload/MemberPhoto/" + newfoto;
                }
                else
                {
                    ModelState.AddModelError("Fotoğraf", "Fotoğraf Seçiniz");
                }
                member.AuthorityId = 1;
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Basarisiz = "Kullanıcı Eklenemedi !!!";
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Member member = db.Member.Find(id);
                db.Member.Remove(member);
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
