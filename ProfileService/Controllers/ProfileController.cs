using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Controllers
{
    public class ProfileController : Controller
    {
        // GET: ProfileServiceController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProfileServiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileServiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileServiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfileServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
