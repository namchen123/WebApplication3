using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
	public class MainController : Controller
	{
		private readonly NewShopContext _context;
		public MainController(NewShopContext context) 
		{ 
			_context = context;
		}
		public IActionResult Index()
		{
			var loaihang = _context.Categories.ToList();
			ViewBag.Loaihang = loaihang;
			var product = _context.Products.ToList();
			return View(product);
		}
		public IActionResult MainBySearch(string name="")
		{
			var product = _context.Products.Where(p=>p.Name==name).ToList();
			return PartialView("NewShopByName",product);
		}
		public IActionResult MainById(int id)
		{
			var product = _context.Products.Where(p => p.CategoryId==id).ToList();
			return PartialView("NewShopByName", product);
		}
		public IActionResult Create()
		{
			var loaihang = _context.Categories.ToList();
			ViewBag.Loaihang = loaihang;
			ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
			ViewBag.ProviderId = new SelectList(_context.Providers, "Id", "ProviderName");
			return View();
		}
		[HttpPost]
		public IActionResult Create(Product product)
		{
			var loaihang = _context.Categories.ToList();
			ViewBag.Loaihang = loaihang;
			if (!ModelState.IsValid)
			{
				_context.Products.Add(product);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
			ViewBag.ProviderId = new SelectList(_context.Providers, "Id", "ProviderName");
			return View();
		}
		public IActionResult Delete(string ?id)
		{
			var product = _context.Products.FirstOrDefault(p=>p.Id==id);
			return View(product);
		}
		[HttpPost,ActionName("Delete")]
		public IActionResult DeleteConfirm(string? id)
		{
			var product = _context.Products.Find(id);
			_context.Products.Remove(product);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
	}
}
