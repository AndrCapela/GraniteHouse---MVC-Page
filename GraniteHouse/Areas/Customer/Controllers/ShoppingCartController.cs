using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Extensions;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Models.Products>()
            };
        }

        public async Task<IActionResult> Index()
        {
            List<int> listShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (listShoppingCart != null && listShoppingCart.Count > 0)
            {
                foreach (int cartItem in listShoppingCart)
                {
                    Products prod = await _db.Products.Include(p => p.SpecialTags).Include(p => p.ProductTypes).Where(p => p.Id == cartItem).FirstOrDefaultAsync();
                    ShoppingCartVM.Products.Add(prod);
                }
            }

            return View(ShoppingCartVM);
        }
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public IActionResult IndexPost()
        {
            List<int> listCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate
                .AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour)
                .AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);

            Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointments.Add(appointments);
            _db.SaveChanges();

            int appointmentId = appointments.Id;

            foreach (int productId in listCartItems)
            {
                ProductsSelectedForAppointment productsSelectedForAppointment = new ProductsSelectedForAppointment()
                {
                    AppointmentId = appointments.Id,
                    ProductId = productId
                };
                _db.ProductsSelectedForAppointment.Add(productsSelectedForAppointment);
            }
            _db.SaveChanges();
            listCartItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", listCartItems);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            List<int> listCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if(listCartItems != null && listCartItems.Count > 0)
            {
                if (listCartItems.Contains(id))
                {
                    listCartItems.Remove(id);
                }
            }
            HttpContext.Session.Set("ssShoppingCart", listCartItems);

            return RedirectToAction(nameof(Index));
        }
    }
}