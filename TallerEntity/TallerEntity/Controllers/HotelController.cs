using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TallerEntity.Data;
using TallerEntity.Models;

namespace TallerEntity.Controllers
{

    public class HotelController : Controller
    {
        HotelContext db = new HotelContext();
        // GET: HotelContext
        public ActionResult Index()
        {

            try
            {
                var reservationList = new List<Reservation>();

                reservationList = db.Reservations.ToList();


                return View(reservationList);
            }
            catch
            {
                return View(null);
            }
            
        }

        // GET: HotelContext/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HotelContext/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HotelContext/Create
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

        // GET: HotelContext/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HotelContext/Edit/5
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

        // GET: HotelContext/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HotelContext/Delete/5
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
