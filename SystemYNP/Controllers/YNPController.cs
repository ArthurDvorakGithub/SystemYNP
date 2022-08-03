using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SystemYNP.Data;
using SystemYNP.Models;

namespace SystemYNP.Controllers
{
    public class YnpController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public YnpController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<YNP> objList = _db.YNP;
            return View(objList);
        }

        //GET - Create
        public IActionResult Create()
        {
            return View();
        }

        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(YNP obj)
        {
            if (ModelState.IsValid)
            {
                _db.YNP.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.YNP.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(YNP obj)
        {
            if (ModelState.IsValid)
            {
                _db.YNP.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var ynp = _db.YNP.Find(id);

            if (ynp == null)
            {
                return NotFound();
            }

            return View(ynp);
        }

        // POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.YNP.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.YNP.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
