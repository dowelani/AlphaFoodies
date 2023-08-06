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
        // GET: Home
        public ActionResult dashboard()
        {
            return View();
        }
        public ActionResult dash()
        {
            ViewData["id"] = TempData.Peek("c");
            return View(chef.Orders.ToList());
        }
        public ActionResult order()
        {
            if (TempData.Peek("d") != null)
            {
                TempData.Remove("d");
                ViewData.Remove("d");
            }
            ViewData["order"] = chef.Orders.ToList();
            ViewData["menuitem"] = chef.MenuItems.ToList();
            ViewData["orderitem"] = chef.OrderItems.ToList();
            return View();
        }
        public ActionResult Oview(int? id)
        {
            TempData.Add("d", id);
            ViewData["d"] = TempData.Peek("d");
            ViewData["order"] = chef.Orders.ToList();
            ViewData["menuitem"] = chef.MenuItems.ToList();
            ViewData["orderitem"] = chef.OrderItems.ToList();
            return PartialView();
        }
        public ActionResult Oview2(int? id)
        {
            TempData.Add("d", id);
            ViewData["d"] = TempData.Peek("d");
            ViewData["order"] = chef.Orders.ToList();
            ViewData["menuitem"] = chef.MenuItems.ToList();
            ViewData["orderitem"] = chef.OrderItems.ToList();
            return PartialView();
        }
        [HttpGet]
        public ActionResult accept(int? id)
        {
            if (TempData.Peek("d") != null)
            {
                TempData.Remove("d");
                ViewData.Remove("d");
            }
            if (id == null) { return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest); }
            Order cur = chef.Orders.Find(id);
            if (cur == null) { return HttpNotFound(); }
            TempData.Remove("order");
            return PartialView(cur);
        }
        [HttpPost]
        public ActionResult accept(Order cur)
        {
                cur.Order_Status = "accepted";
                chef.Entry(cur).State = System.Data.Entity.EntityState.Modified;
                chef.SaveChanges();
                return RedirectToAction("order");
        }
        [HttpGet]
        public ActionResult complete(int? id)
        {
            if (TempData.Peek("d") != null)
            {
                TempData.Remove("d");
                ViewData.Remove("d");
            }
            if (id == null) { return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest); }
            Order cur = chef.Orders.Find(id);
            if (cur == null) { return HttpNotFound(); }
            TempData.Remove("order");
            return PartialView(cur);
        }
        [HttpPost]
        public ActionResult complete(Order cur)
        {
            cur.Order_Status = "complete";
            chef.Entry(cur).State = System.Data.Entity.EntityState.Modified;
            chef.SaveChanges();
            return RedirectToAction("order");


        }
        public ActionResult profile()
        {
            ViewData["id"] = TempData.Peek("c");
            return View(chef.Chefs.ToList());
        }
        [HttpGet]
        public ActionResult edit()
        {
            return PartialView();
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
        public ActionResult settings(Chef cur, HttpPostedFileBase thePicture)
        {
            if (thePicture!=null) 
            {
                cur.Image = new byte[thePicture.ContentLength];
                thePicture.InputStream.Read(cur.Image, 0, thePicture.ContentLength);
                chef.Entry(cur).State = System.Data.Entity.EntityState.Modified;
                chef.SaveChanges();
                return RedirectToAction("profile");
            }
            else
            {
                chef.Entry(cur).State = System.Data.Entity.EntityState.Modified;          
                chef.SaveChanges();
                return RedirectToAction("profile");
            }
        
        }
       
    }

}
