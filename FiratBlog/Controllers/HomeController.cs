using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiratBlog.Models;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace FiratBlog.Controllers
{
    public class HomeController : Controller
    {

        BlogFiratDB DB = new BlogFiratDB();


        public ActionResult Index(int page = 1)
        {
            try
            {
                var article = DB.Article.OrderByDescending(m => m.ArticleId).ToPagedList(page, 4);
                return View(article);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        public ActionResult ArticleDetails(int id)
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

        public ActionResult About()
        {
            return View();
        }


        public ActionResult CreateContact()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContact(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DB.Contact.Add(contact);
                    DB.SaveChanges();
                    ViewBag.Basarili = "Mesajınız gönderildi";
                }

                return View(contact);
            }
            catch
            {
                ViewBag.Basarisiz = "Gönderme işlemi başarısız!";
                return View();
            }

        }


        public ActionResult CategoryPartial()
        {
            try
            {
                return View(DB.Category.ToList());
            }
            catch
            {
                ViewBag.Basarisiz = "Kategoriler yüklenmedi !!!";
                return View();
            }

        }


        public JsonResult SendComment(string comment, int Articleid)
        {
            try
            {
                var kullaniciid = Session["memberid"];

                if (comment == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    DB.Comment.Add(new Comment { MemberId = Convert.ToInt32(kullaniciid), ArticleId = Articleid, Contents = comment, Date = DateTime.Now });
                    DB.SaveChanges();
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                ViewBag.Basarisiz = "Yorum yapma işlemi başarısız !!!";
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteComment(int id)
        {
            try
            {
                var memberid = Session["memberid"];

                var comment = DB.Comment.Where(y => y.CommentId == id).SingleOrDefault();

                var article = DB.Article.Where(m => m.ArticleId == comment.ArticleId).SingleOrDefault();

                if (comment.MemberId == Convert.ToInt32(memberid))
                {
                    DB.Comment.Remove(comment);
                    DB.SaveChanges();
                    return RedirectToAction("ArticleDetails", "Home", new { id = article.ArticleId });
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch
            {
                ViewBag.Basarisiz = "Silme işlemi başarısız oldu !!";
                return View();
            }
        }

        public ActionResult ArticleRead(int Articleid)
        {
            try
            {
                var article = DB.Article.Where(m => m.ArticleId == Articleid).SingleOrDefault();
                article.Views += 1;
                DB.SaveChanges();
                return View();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult CategoryArticles(int id)
        {
            try
            {
                var articles = DB.Article.Where(m => m.Category.CategoryId == id).ToList();
                return View(articles);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }


        public ActionResult BlogSearch(string Search = null)
        {
            try
            {
                var aranan = DB.Article.Where(m => m.Title.Contains(Search)).ToList();
                return View(aranan.OrderByDescending(m => m.ArticleId));
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public ActionResult lastComments()
        {
            try
            {
                return View(DB.Comment.OrderByDescending(y => y.CommentId).Take(5));
            }
            catch
            {
                ViewBag.Basarisiz = "Yorumlar yüklenmedi !!!";
                return View();
            }

        }

        public ActionResult PopularArticles()
        {
            try
            {
                return View(DB.Article.OrderByDescending(m => m.Views).Take(5));
            }
            catch
            {
                ViewBag.Basarisiz = "Popüler makaleler yüklenmedi !!!";
                return View();
            }
        }
    }
}