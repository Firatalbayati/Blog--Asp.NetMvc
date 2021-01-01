using FiratBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FiratBlog.Controllers
{
    public class AdminController : Controller
    {

        BlogFiratDB DB = new BlogFiratDB();


        public ActionResult Index()
        {
            try
            {
                ViewBag.KategoriSayisi = DB.Category.Count();
                ViewBag.MakaleSayisi = DB.Article.Count();
                ViewBag.UyeSayisi = DB.Member.Count();
                ViewBag.YorumSayisi = DB.Comment.Count();

                return View();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
    }
}