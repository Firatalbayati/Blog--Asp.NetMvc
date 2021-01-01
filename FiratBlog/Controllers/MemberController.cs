using FiratBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FiratBlog.Controllers
{
    public class MemberController : Controller
    {

        BlogFiratDB DB = new BlogFiratDB();


        public ActionResult Index(int id)
        {
            try
            {
                var member = DB.Member.Where(m => m.MemberId == id).SingleOrDefault();

                if (Convert.ToInt32(Session["memberid"]) != member.MemberId)
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


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Member member)
        {
            try
            {
                var check = DB.Member.FirstOrDefault(x => x.Email == member.Email);

                if (check.Email == member.Email && check.Password == member.Password)
                {
                    Session["memberid"] = check.MemberId;
                    Session["username"] = check.UserName;
                    Session["authorityid"] = check.AuthorityId;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.HataErorr = "Mail veya şifre yanlış";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReError = ex.Message;
                return View();
            }

        }

        public ActionResult Logout()
        {
            try
            {
                Session["memberid"] = null;
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Member member, HttpPostedFileBase Photo)
        {
            try
            {
                var CheckUserName = DB.Member.FirstOrDefault(s => s.UserName == member.UserName);

                var CheckMail = DB.Member.FirstOrDefault(s => s.Email == member.Email);

                if (CheckMail == null & CheckUserName == null)
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
                            member.AuthorityId = 2;
                            DB.Member.Add(member);
                            DB.SaveChanges();
                            Session["MemberId"] = member.MemberId;
                            Session["UserName"] = member.UserName;
                            return RedirectToAction("Index", "Home");
                        }
                        return View(member);
                    }
                    catch
                    {
                        ViewBag.Basarisiz = "Kayıt Başarısız Lütfen dosya türünü kontrol edin !";
                        return View();
                    }
                }
                else
                {
                    if (!(CheckUserName == null))
                    {
                        ViewBag.error = "Bu kullanıcı adı zaten alınmış !";
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Bu Email zaten kayıtlı !";
                        return View();
                    }
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                var member = DB.Member.Where(m => m.MemberId == id).SingleOrDefault();

                if (Convert.ToInt32(Session["memberid"]) != member.MemberId)
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


        [HttpPost]
        public ActionResult Edit(Member member, int id, HttpPostedFileBase Photo)
        {

            try
            {
                var members = DB.Member.Where(m => m.MemberId == id).SingleOrDefault();

                var CheckMail = DB.Member.FirstOrDefault(s => s.Email == member.Email);

                var CheckUserName = DB.Member.FirstOrDefault(s => s.UserName == member.UserName);

                if (CheckMail == null & CheckUserName == null)
                {
                    try
                    {
                        if (Photo != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath(member.Photo)))
                            {
                                System.IO.File.Delete(Server.MapPath(members.Photo));
                            }

                            WebImage image = new WebImage(Photo.InputStream);
                            FileInfo imageInfo = new FileInfo(Photo.FileName);

                            string newfoto = Guid.NewGuid().ToString() + imageInfo.Extension;
                            image.Resize(160, 160);
                            image.Save("~/Upload/MemberPhoto/" + newfoto);
                            members.Photo = "/Upload/MemberPhoto/" + newfoto;
                        }
                        members.NameSurname = member.NameSurname;
                        members.UserName = member.UserName;
                        members.Password = member.Password;
                        members.Email = member.Email;
                        DB.SaveChanges();
                        Session["username"] = member.UserName;
                        return RedirectToAction("Index", "Home", new { id = members.MemberId });
                    }
                    catch
                    {
                        ViewBag.Basarisiz = "Güncelleme Başarısız Lütfen dosya türünü kontrol edin !";
                        return View();
                    }
                }
                else
                {
                    if (!(CheckMail == null))
                    {
                        ViewBag.error = "Bu Email zaten kayıtlı !";
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Bu kullanıcı adı zaten alınmış !";
                        return View();
                    }
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult MemberProfile(int id)
        {
            try
            {
                var member = DB.Member.Where(u => u.MemberId == id).SingleOrDefault();
                return View(member);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var member = DB.Member.FirstOrDefault(x => x.Email == email);

            if (member == null)
            {
                ViewBag.MyError = "Böyle bir hesap bulunamadı";
                return View();
            }
            else
            {
                var body = "Şifreniz : " + member.Password;
                MyMail mail = new MyMail(member.Email, "Şifremi Unuttum", body);
                mail.SendMail();
                TempData["Info"] = email + " mail adresinize şifreniz gönderilmiştir.";
                return RedirectToAction("Login");
            }
        }
    }
}