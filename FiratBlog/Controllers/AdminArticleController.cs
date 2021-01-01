using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiratBlog.Models;
using System.IO;
using System.Web.Helpers;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace FiratBlog.Controllers
{
    public class AdminArticleController : Controller
    {

        BlogFiratDB DB = new BlogFiratDB();


        public ActionResult Index(int page = 1, string Search = null)
        {
            try
            {
                if (Search != null)
                {
                    var aranan = DB.Article.Where(m => m.Title.Contains(Search)).ToList();
                    return View(aranan.OrderByDescending(m => m.ArticleId).ToPagedList(page, 100));
                }
                else
                {
                    var Article = DB.Article.OrderByDescending(m => m.ArticleId).ToPagedList(page, 4);
                    return View(Article);
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
                Article Article = DB.Article.Find(id);
                if (Article == null)
                {
                    return HttpNotFound();
                }
                return View(Article);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(DB.Category, "CategoryId", "CategoryName");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Article article, HttpPostedFileBase Photo)
        {
            try
            {

                if (Photo != null)
                {
                    WebImage image = new WebImage(Photo.InputStream);
                    FileInfo imageInfo = new FileInfo(Photo.FileName);

                    string newfoto = Guid.NewGuid().ToString() + imageInfo.Extension;
                    image.Resize(800, 350);
                    image.Save("~/Upload/ArticlePhoto/" + newfoto);
                    article.Photo = "/Upload/ArticlePhoto/" + newfoto;
                }
                article.Views = 0;
                article.MemberId = Convert.ToInt32(Session["memberid"]);
                DB.Article.Add(article);
                DB.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(DB.Category, "CategoryId", "CategoryName");
                ViewBag.Basarisiz = "Makale Eklenemedi !!!";
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var article = DB.Article.Where(m => m.ArticleId == id).SingleOrDefault();

            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(DB.Category, "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }


        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase Photo, Article oldarticle)
        {
            try
            {
                var Editedarticle = DB.Article.Where(m => m.ArticleId == id).SingleOrDefault();

                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Editedarticle.Photo)))
                    {
                        System.IO.File.Delete(Server.MapPath(Editedarticle.Photo));
                    }

                    WebImage image = new WebImage(Photo.InputStream);
                    FileInfo imageInfo = new FileInfo(Photo.FileName);

                    string newfoto = Guid.NewGuid().ToString() + imageInfo.Extension;
                    image.Resize(800, 350);
                    image.Save("~/Upload/ArticlePhoto/" + newfoto);
                    Editedarticle.Photo = "/Upload/ArticlePhoto/" + newfoto;
                }
                Editedarticle.Title = oldarticle.Title;
                Editedarticle.Contents = oldarticle.Contents;
                Editedarticle.CategoryId = oldarticle.CategoryId;
                DB.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(DB.Category, "CategoryId", "CategoryName", oldarticle.CategoryId);
                ViewBag.Basarisiz = "Güncelleme işlemi başarısız oldu !!";
                return View(oldarticle);
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                var Articless = DB.Article.Where(m => m.ArticleId == id).SingleOrDefault();

                if (Articless == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(Articless.Photo)))
                {
                    System.IO.File.Delete(Server.MapPath(Articless.Photo));
                }
                foreach (var degisken in Articless.Comment.ToList())
                {
                    DB.Comment.Remove(degisken);
                }
                DB.Article.Remove(Articless);
                DB.SaveChanges();
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
