using FlipTracks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlipTracks.Controllers
{
    public class ItemsController : Controller
    {

        private readonly AppDbContext _context;

        public IActionResult Index()
        {
            var items = _context.Items.ToList();

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create (Item item, string OtherPurchaseSource)
        {
            if (item.PurchaseSource == "Other" && !string.IsNullOrWhiteSpace(OtherPurchaseSource))
            {
                item.PurchaseSource = OtherPurchaseSource;
            }
            _context.Items.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);

            ViewBag.Categories = new List<SelectListItem>
            {
                new SelectListItem { Value = "Sports", Text = "Sports" },
                new SelectListItem { Value = "Electronics", Text = "Electronics" },
                new SelectListItem { Value = "Books", Text = "Books" },
                new SelectListItem { Value = "Home Goods", Text = "Home Goods" }
            };

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Item item, string OtherPurchaseSource)
        {
            var existingItem = _context.Items.FirstOrDefault(x => x.Id == item.Id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = item.Name;
            existingItem.Category = item.Category;

            if (item.PurchaseSource == "Other" && !string.IsNullOrWhiteSpace(OtherPurchaseSource))
            {
                existingItem.PurchaseSource = OtherPurchaseSource;
            }
            else if (!string.IsNullOrWhiteSpace(item.PurchaseSource))
            {
                existingItem.PurchaseSource = item.PurchaseSource;
            }

            existingItem.PurchasePrice = item.PurchasePrice;
            existingItem.PurchaseDate = item.PurchaseDate;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete (int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Sell(int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);
            
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public IActionResult Sell(Item item, string OtherSaleSource)
        {
            var existingItem = _context.Items.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem == null)
            {
                return NotFound();
            }

            if (item.SaleSource == "Other" && !string.IsNullOrWhiteSpace(OtherSaleSource))
            {
                existingItem.SaleSource = OtherSaleSource;

            }

            else if (!string.IsNullOrWhiteSpace(item.SaleSource))
            {
                existingItem.SaleSource = item.SaleSource;
            }
            existingItem.SalePrice = item.SalePrice;
            existingItem.SaleDate = item.SaleDate;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ItemsController(AppDbContext context)
        {
            _context = context; 
        }
    }
}
