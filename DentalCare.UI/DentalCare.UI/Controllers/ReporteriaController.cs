using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentalCare.UI.Controllers
{
    public class ReporteriaController : Controller
    {
        // GET: Reporteria
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reporteria/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reporteria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reporteria/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporteria/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reporteria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporteria/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reporteria/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
