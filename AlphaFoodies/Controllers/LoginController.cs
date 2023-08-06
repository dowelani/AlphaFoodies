using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaFoodies.Models;
namespace AlphaFoodies.Controllers
{
    public class LoginController : Controller
    {
        private AlphaFoodiesModel model = new AlphaFoodiesModel();
        [HttpGet]
        public ActionResult home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult pass()
        {
            //Invoked when the user clicks on the login button on the home page
            var usertype = Request.Form["user_type"];
            
            if (usertype.Equals("chef") || usertype.Equals("admin"))
            {
                Session["usertype"] = usertype; //used to determin which table to look up on when logging in
                //need login details
                return RedirectToAction("verifyUser");
            }
            else
            {
                //if its a customer it must be directed to the menu
                return RedirectToAction("customerIndex", "Customer");
            }              
        }

        [HttpGet]
        public ActionResult verifyUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult passTo()
        {
            var email_address = Request.Form["email_address"];
            var password = Request.Form["password"];
            //User user = new User(email_address, password);
          return lookUp(new User(email_address, password));    
        }

        private ActionResult lookUp(User user)
        {
            var cur_userType = Session["usertype"].ToString();
            if (cur_userType.Equals("admin"))
            {
                Admin admin = (from cur_user in model.Admins
                               where cur_user.Email_Address.Equals(user.Email_Address)
                               select cur_user).SingleOrDefault();
                TempData.Add("cA", admin.Admin1);
                //check if the password matches
                if (admin.Password.Equals(user.Password))
                {
                    return RedirectToAction("profile","Admin"); //go to landing page
                }
                else
                {
                    TempData.Remove("cA");
                    ViewBag.message = "*email or password incorrect";
                    return View("verifyUser");
                }
            }
            else
            {
                //otherwise the user is a chef
                Chef chef = (from cur_chef in model.Chefs
                             where cur_chef.Email_Address.Equals(user.Email_Address)
                             select cur_chef).SingleOrDefault();
                TempData.Add("c", chef.ChefID);
                //check if the password matches
                if (chef.Password.Equals(user.Password))
                {
                    return RedirectToAction("dashboard","Chef");     //go to chef landing page
                }
                else
                {
                    TempData.Remove("c");
                    ViewBag.message = "*email or password incorrect";
                    return View("verifyUser");
                }
            }
        }
    }
}