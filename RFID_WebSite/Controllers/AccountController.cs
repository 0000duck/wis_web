using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RFID_WebSite.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult Verify(string userId, string pwd)
        {
            try
            {
                
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                          userId,//你想要存放在 User.Identy.Name 的值，通常是使用者帳號
                                          DateTime.Now,
                                          DateTime.Now.AddMinutes(30),
                                          false,//將管理者登入的 Cookie 設定成 Session Cookie
                                          "Data",//userdata看你想存放啥
                                          FormsAuthentication.FormsCookiePath);
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    return Json("OK", JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception e)
            {
                return Json(e.StackTrace, JsonRequestBehavior.AllowGet);
            }
        }

        public RedirectToRouteResult Logout()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            //FormsAuthentication.RedirectToLoginPage(); 
            //return Json("", JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index", "Home");
            
        }

    }
}
