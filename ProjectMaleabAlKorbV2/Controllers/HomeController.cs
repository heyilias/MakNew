using ProjectMaleabAlKorbV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectMaleabAlKorbV2.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        MalaebAlKorbEntities db = new MalaebAlKorbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }
        //Contact
        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            
            model.dateMessage = DateTime.Now;
            db.Contacts.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        //Player
        public JsonResult saveData(Player player)
        {
            player.dateCreated = DateTime.Now;
            db.Players.Add(player);
            db.SaveChanges();
            Session["emails"] = player.emails;

            return Json("Registration successfull", JsonRequestBehavior.AllowGet);
        }
        //Login Form
        public JsonResult Login_Verifier(Player player)
        {
            var result = "fail";
            //var isCheck = 0;
            var ply = db.Players.Where(p => p.emails == player.emails && p.passwords == player.passwords).FirstOrDefault();
            if (ply != null)
            {
                result = "success";
                Session["emails"] = ply.emails;
                Session["names"] = ply.names;
                Session["password"] = ply.passwords;
                //Remember Me
                //if(isCheck == 1)
                //{
                //    HttpCookie userInfo = new HttpCookie("userInfo");
                //    userInfo["emails"] = ply.emails;
                //    userInfo["password"] = ply.passwords;
                //    userInfo.Expires.AddSeconds(60);
                //    Response.Cookies.Add(userInfo);
                //}
                
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        
        public ActionResult LogOut()
        {
            Session.Abandon();
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}