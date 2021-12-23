using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaFoodies.Models;
namespace AlphaFoodies.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private AlphaFoodiesModel context = new AlphaFoodiesModel();
        [HttpGet]
        public ActionResult Index(string text)
        {
            ViewBag.text = text;
            var list = context.MenuItems.Where(x => x.Item_Name.Contains(text) || text == null).ToList();//a list of searched items or all items 
            ViewBag.found = "Search results for '" + text + "' .";//message to be displayed if searched items were found 
            if (list.Count() > 0)
            {
                return View(list); //returns a customer view and selected items
            }
            else
            {
                ViewBag.notfound = "The searched item '" /*message to be displated if no items were found while searching*/
                + text
                + "' is either not available or not sold in this resturant, keep browsing the menu to see available items";
                TempData["notFound"] = "The searched item '" /*message to be displated if no items were found while searching*/
                + text
                + "' is either not available or not sold in this resturant, keep browsing the menu to see available items";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult addItem(int id)
        {
            var list = from a in context.MenuItems
                       where a.Item_Code.Equals(id)
                       select a;
            MenuItem curItem = list.FirstOrDefault();
            if (curItem != null)
            {
                List<OrderItem> orders;
                if (Session["cart"] == null)
                {
                    orders = new List<OrderItem>();
                    OrderItem item = new OrderItem
                    {
                        MenuItem = curItem,
                        Item_Code = curItem.Item_Code,
                        Quantity = 1
                    }; 
                    Session["cart"] = orders;
                }
                else
                {
                    bool yes = false;
                    orders = (List<OrderItem>)Session["cart"];
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (orders[i].MenuItem.Item_Code.Equals(curItem.Item_Code))
                        {
                            orders[i].Quantity += 1;
                            yes = true;
                            break;
                        }
                    }
                    if (!yes)
                    {
                        OrderItem item = new OrderItem
                        {
                            MenuItem = curItem,
                            Item_Code = curItem.Item_Code,
                            Quantity = 1
                        };
                        orders.Add(item); 
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult viewCart()
        {
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];
            return View(orders);
        }
        public ActionResult increament(int id, string check)
        {
            var list = from a in context.MenuItems
                       where a.Item_Code.Equals(id)
                       select a;
            MenuItem curItem = list.FirstOrDefault();  
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].MenuItem.Item_Name.Equals(curItem.Item_Name))
                {
                    orders[i].Quantity += 1; 
                    break;
                }
            } 
            Session["cart"] = orders;
            if(check!= null)
            { 
                return RedirectToAction("viewCart");
            }
            else
            {
                return RedirectToAction("viewOrder"); 
            }
        } 
        public ActionResult Remove(int id, string check)
        {
            var list = from a in context.MenuItems
                       where a.Item_Code.Equals(id)
                       select a;
            MenuItem curItem = list.FirstOrDefault();
            bool found = false;
            int trace = -1;
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].MenuItem.Item_Name.Equals(curItem.Item_Name))
                {
                    orders[i].Quantity -= 1;
                    trace = i;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                if (orders[trace].Quantity == 0)
                {
                    orders.RemoveAt(trace);
                }
            }
            Session["cart"] = orders; 
            if (check != null)
            {
                return RedirectToAction("viewCart");
            }
            else
            {
                return RedirectToAction("viewOrder");
            }
        } 
        public ActionResult RemoveAll(string check)
        {
            emptyCart();
            if (check != null)
            {
                return RedirectToAction("viewCart");
            }
            else
            {
                return RedirectToAction("viewOrder");
            }
        }
        private void emptyCart()
        {
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];
            orders.Clear();
            Session["cart"] = orders; 
        }
        [HttpGet]
        public ActionResult viewOrder()
        {
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];

            return View(orders);
        } 
        [HttpPost] 
        public ActionResult viewOrder(int serviceRating, string Comment, int tip, string orderType)
        { 
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];
            Order newOrder = new Order();
            decimal orderTotal = 0;
            foreach (OrderItem x in orders)
            {
                x.Order_Number = newOrder.Order_Number;
                newOrder.OrderItems.Add(x);
                newOrder.Order_Items += " "+ x.MenuItem.Item_Name;
                orderTotal += x.Quantity * x.MenuItem.Price;
            }
            var list = from a in context.Waiters
                       where a.WaiterID.Equals(4)
                       select a;
            var l = from a in context.Chefs
                       where a.ChefID.Equals(1)
                       select a;
            var waiter = list.FirstOrDefault();
            var chef = l.FirstOrDefault();
            newOrder.Waiter = waiter; 
            newOrder.WaiterID = waiter.WaiterID;
            newOrder.Chef = chef;
            newOrder.ChefID = chef.ChefID;
            newOrder.Order_Date = DateTime.Today;
            newOrder.Order_Time = DateTime.Now.TimeOfDay;
            newOrder.Order_Status = "Pending";
            newOrder.Order_PrepTime = TimeSpan.FromMinutes(30);
            newOrder.Service_Rating = serviceRating;
            newOrder.Comment = Comment;
            newOrder.Order_Type = orderType;
            newOrder.Tip = tip;
            newOrder.Order_Total = orderTotal; 
            context.Orders.Attach(newOrder);
            Session["order"] = newOrder;
            context.SaveChanges();
            return RedirectToAction("awaitingOrder");
        }
        public ActionResult awaitingOrder()
        { 

            return View(Session["order"]);
        }
        public ActionResult exit()
        {
            emptyCart();
            return RedirectToAction("Index"); 
        }
    }
}