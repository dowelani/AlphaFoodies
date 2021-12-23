using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaFoodies.Models;
using System.Data.Entity;
namespace AlphaFoodies.Controllers
{
    public class AdminController : Controller
    {
        private AlphaFoodiesModel model = new AlphaFoodiesModel();
        Admin admin = null;
        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult AddMenuItem()
        {
            return View();
        }
        [HttpPost]
        [OutputCache(Duration = 0)]
        public ActionResult AddMenuItem(MenuItem newItem, HttpPostedFileBase thePicture)
        {
            var cat = Request.Form["category"];
            var price = Request.Form["price"];
            newItem.Category = cat;
            newItem.Price = decimal.Parse(price);
            if (thePicture != null)
            {
                newItem.Picture = new byte[thePicture.ContentLength];  //converts the image to binary
                thePicture.InputStream.Read(newItem.Picture, 0, thePicture.ContentLength);
            }
            model.MenuItems.Add(newItem);
            model.SaveChanges();
            return RedirectToAction("ViewMenu");
        }


        /*View menu items, action methods*/

        public ActionResult ViewMenu()
        {
            return View(model.MenuItems.ToList());
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult add_Staff()
        {
            return View();
        }

        [HttpPost]
        [OutputCache(Duration = 0)]
        public ActionResult add_Staff(HttpPostedFileBase thePicture)
        {
            var firstname = Request.Form["firstname"];
            var lastname = Request.Form["lastname"];
            var email_Address = Request.Form["email_address"];
            var phone = Request.Form["phone_number"];
            var type = Request.Form["user_type"];

            /*The objects are different, hence we need to verify what type of a user is this one being created*/
            if (type.Equals("waiter"))
            {
                Waiter new_waiter = new Waiter();
                new_waiter.Firstname = firstname;
                new_waiter.Lastname = lastname;
                new_waiter.Email_Address = email_Address;
                new_waiter.Phone = phone;
                if (thePicture != null)
                {
                    new_waiter.Image = new byte[thePicture.ContentLength];  /*Convert the image to a byte*/
                    thePicture.InputStream.Read(new_waiter.Image, 0, thePicture.ContentLength);
                }
                model.Waiters.Add(new_waiter);
                model.SaveChanges();
            }
            else
            {
                Chef new_Chef = new Chef();
                new_Chef.Firstname = firstname;
                new_Chef.Lastname = lastname;
                new_Chef.Email_Address = email_Address;
                new_Chef.Phone = phone;
                new_Chef.Password = Request.Form["password"];
                if (thePicture != null)
                {
                    new_Chef.Image = new byte[thePicture.ContentLength];
                    thePicture.InputStream.Read(new_Chef.Image, 0, thePicture.ContentLength);
                }
                model.Chefs.Add(new_Chef);
                model.SaveChanges();
            }

            return View();
        }

        [HttpGet]
        public ActionResult details(int id)
        {
            MenuItem menuItem = model.MenuItems.Where(x => x.Item_Code == id).Single();
            return PartialView(menuItem);
        }

        [HttpGet]
        public ActionResult deleteMenuItem(int? id)
        {
            MenuItem curItem = model.MenuItems.Where(x => x.Item_Code == id).Single();
            return PartialView(curItem);
        }

        [HttpPost, ActionName("deleteMenuItem")]
        [ValidateAntiForgeryToken]
        public ActionResult deleteMenuItemPost(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            MenuItem curItem = model.MenuItems.Find(id);

            if (curItem == null)
                return HttpNotFound();

            model.MenuItems.Remove(curItem);
            model.SaveChanges();
            return RedirectToAction("ViewMenu");
        }

        [HttpGet]
        public ActionResult editItem(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            MenuItem menuItem_ToEdit = model.MenuItems.Find(id);
            if (menuItem_ToEdit != null)
            {
               return PartialView(menuItem_ToEdit);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult editItem(MenuItem menuItem,HttpPostedFileBase uploadPicture)
        {
            var price = Request.Form["price"];
            if (uploadPicture != null)
            {
                menuItem.Picture = new byte[uploadPicture.ContentLength];  //converts the image to binary
                uploadPicture.InputStream.Read(menuItem.Picture, 0, uploadPicture.ContentLength);
            }

                menuItem.Item_Name = "dummy";
            menuItem.Category = "Pizzaa";
                model.Entry(menuItem).State = EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("ViewMenu","Admin"); //  
        }            
        //[HttpGet]
        //public ActionResult addAdmin()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult addAmin(Admin admin,HttpPostedFileBase thePicture)
        //{
        //    if (thePicture != null)
        //    {
        //        admin.Picture = new byte[thePicture.ContentLength];
        //        thePicture.InputStream.Read(admin.Picture,0,thePicture.ContentLength);
        //        model.Admins.Add(admin);
        //        model.SaveChanges();
        //    }
        //    return View();
        //}

        public ActionResult viewAdminProfile()
        {
            return View();
        }

       
    }
}
