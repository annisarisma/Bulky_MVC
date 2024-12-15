using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Create(Category obj)
		{
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name cannot be exactly match with the Display Order");
            }

			if (ModelState.IsValid)
            {
				_db.Categories.Add(obj);
				_db.SaveChanges();
                TempData["success"] = "Category created successfully";
				return RedirectToAction("Index", TempData);
			}

            return View();
		}

		public IActionResult Edit(int? id)
		{
            if (id == null || id == 0) {
                return NotFound();
            }

            Category? data = _db.Categories.Find(id);

			if (data == null) {
                return NotFound();
            }

            return View(data);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category updated successfully";

				return RedirectToAction("Index", TempData);
			}

			return View();
		}

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? data = _db.Categories.Find(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);

            if (obj == null) {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index", TempData);

        }
    }
}
