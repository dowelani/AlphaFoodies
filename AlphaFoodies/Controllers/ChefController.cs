using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaFoodies.Models;
namespace AlphaFoodies.Controllers
{
    public class ChefController : Controller
    {
        private AlphaFoodiesModel chef = new AlphaFoodiesModel();
        public ActionResult login()
        {
            return View();
        }

        // POST: security/Create
        [HttpPost]
        public ActionResult login([Bind(Exclude = "Id")] Chef UP)
        {

            TempData.Add("c", UP.ChefID);
            var check1 = (from Chef in chef.Chefs
                          where Chef.ChefID.Equals(UP.ChefID)
                          select Chef).SingleOrDefault();
            int valid = string.Compare(UP.Password, check1.Password);
            if (check1 != null && valid == 0)
            {

                return RedirectToAction("profile");
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public ActionResult profile()
        {
            ViewData["id"] = TempData.Peek("c");
            return View(chef.Chefs.ToList());
        }
        public ActionResult History()
        {
            return View(chef.Orders.ToList());
        }
        public ActionResult settings()
        {
            int? id = (int)TempData.Peek("c");
            if (id == null) { return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest); }
            Chef cur = chef.Chefs.Find(id);
            if (cur == null) { return HttpNotFound(); }
            return View(cur);
        }
        [HttpPost]
        public ActionResult setting(Chef cur)
        {
            if (ModelState.IsValid)
            {
                chef.Entry(cur).State = System.Data.Entity.EntityState.Modified;
                chef.SaveChanges();
                return RedirectToAction("profile");
            }
            return View(cur);
        }
    }
}
