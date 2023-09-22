using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
                var roomList = new List<Room>();

                roomList = db.Rooms.ToList();


                return View(roomList);
            }
            catch
            {
                return View(null);
            }
            
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

        public ActionResult CreateReservation()
        {
            var customerList = new List<Customer>();
                customerList= db.Customers.ToList();
                ViewBag.Cedulas = customerList;
            return View();
        }

        // POST: HotelContext/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReservation(ReservationCreate reservation)
        {
            try
            {
                var CustomerId = db.Customers.Where(c => c.Cedula == reservation.Cedula).FirstOrDefault().CustomerID;
               // var customer = db.Customers.Where(c => c.Cedula == reservation.CustomerID);
                /*LINQ
                 
                 List<Reservation> listaOrdenada = db.Reservations.OrderBy(objeto => objeto.ReservationID).ToList();

                if (listaOrdenada.Any())
                {
                    Reservation ultimoObjeto = listaOrdenada.Last();
                    //id = ultimoObjeto.ReservationID + 1;

                }
                 */

                var id = Guid.NewGuid();


                if (db.Rooms.Find(reservation.RoomID).AvailabilityRoom == false)
                {
                    ViewBag.reservationBad = "Este cuarto esta ocupado";
                    return View();
                }

                var c = db.Rooms.Find(reservation.RoomID).Capacity;



                var n = db.CustomerCompanions.Where(c => c.CompanionID == CustomerId).ToList().Count;

                if (c < n)
                {
                    ViewBag.reservationBad = "Este cuarto no acepta mas de " + c + " clientes";
                    return View();
                }

                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ReservationID", id));
                parameter.Add(new SqlParameter("@CustomerID", CustomerId));
                parameter.Add(new SqlParameter("@RoomID", reservation.RoomID));
                parameter.Add(new SqlParameter("@ReservationDate", reservation.ReservationDate));
                parameter.Add(new SqlParameter("@CheckInDate", reservation.CheckInDate));
                parameter.Add(new SqlParameter("@CheckOutDate", reservation.CheckOutDate));
                parameter.Add(new SqlParameter("@CustomersIn", n));


                var result = Task.Run(() => db.Database.ExecuteSqlRaw(@"exec dbo.CreateReservations @ReservationID, @CustomerID, @RoomID, @ReservationDate, @CheckInDate, @CheckOutDate, @CustomersIn", parameter.ToArray()));


                ViewBag.reservation = id;

                db.SaveChangesAsync();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.reservationBad = ex.Message;
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
        
    }
}
