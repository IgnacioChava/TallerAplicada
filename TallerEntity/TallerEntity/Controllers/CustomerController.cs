using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TallerEntity.Data;
using TallerEntity.Models;

namespace TallerEntity.Controllers
{
    public class CustomerController : Controller
    {

        HotelContext db = new HotelContext();
        // GET: CustomerController
        public ActionResult Index()
        {
            try
            {
                //Consultamos lso datos utilizando el mapeo de Entity este utiliza linq para optener los datos
                 //var customerList = new List<CustomerReservationsView>();
                 //customerList= db.CustomerReservationsViews.ToList();


                // //vamos a crear nuestra propia consulta LINQ
                /*var customerList = (
                     from customer in db.CustomerReservationsViews
                     select new CustomerReservationsView
                     {
                         NameCustomer = customer.NameCustomer,
                         Email = customer.Email,
                         Phone = customer.Phone,
                         CodeRoom = customer.CodeRoom
                     }
                 ).ToList();
                */
                // //Consulta con querys de sql



                // //Usando expresiones Lambda y metodos de extencion Linq
                //var customerList = db.Customers
                //.Select(customer => new Vista_Customer_Info
                //{
                //    CustomerID = customer.CustomerID,
                //    Name = customer.Name,
                //    Email = customer.Email
                //})
                //.ToList();


                var test = db.Database.SqlQueryRaw<string>(@"exec dbo.VerificarClientes").ToList();

                
                
                var customerList= db.CustomerReservationsViews.FromSqlRaw("SELECT * FROM dbo.CustomerReservationsView").ToList();
                

                return View(customerList);
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }

        }

        public ActionResult FindReservation(Guid id)
        {
            var Reservation = new List<Reservation>();
            Reservation = db.Reservations.Where(obj => obj.CustomerID == id).ToList();

            return View(Reservation);
        }

        public ActionResult FindCustomerCompanion(Guid id)
        {
            var customerCompanions = new List<CustomerCompanion>();
            customerCompanions = db.CustomerCompanions.Where(obj => obj.CompanionID == id).ToList();

            return View(customerCompanions);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(Guid id)
        {


            try
            {

                var customer = db.Customers.Find(id);

                /*var customer = db.Customers
                .Where(customer => customer.CustomerID == searchCustomerId)
                .Select(customer => new Vista_Customer_Info
                {
                    CustomerID = customer.CustomerID,
                    Name = customer.Name,
                    Email = customer.Email
                })
                .FirstOrDefault();*/

                // Consulta SELECT personalizada utilizando FromSqlRaw con parámetros
                /*string sqlQuery = "SELECT * FROM Customers WHERE Id = {0}";
                var customer = db.Customers.FromSqlRaw(sqlQuery, id).FirstOrDefault();*/

                return View(customer);
            }
            catch (Exception ex)
            {
                
                return View(ex.Message);
            }


        }
        public ActionResult CustomerCompanion(Guid id)
        {
             

            return View();
        }

        // POST: HotelContext/Delete/5
        [HttpPost]
        public ActionResult CustomerCompanion(CustomerCompanion customerCompanion)
        {
            try
            {
                //var CustomerId = db.Customers.Where(c => c.Cedula == customerCompanion.Cedula).FirstOrDefault().CustomerID;

                var id = Guid.NewGuid();
                customerCompanion.CustomerID = id;
                db.CustomerCompanions.Add(customerCompanion);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: CustomerController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                /*db.Customers.Add(customer);
                db.SaveChanges();*/

                var id = Guid.NewGuid();
                customer.CustomerID = id;

                string sqlQuery = "INSERT INTO Customers (CustomerID, Cedula, Name, Email, Phone) VALUES (@CustomerID, @Cedula, @Name, @Email, @Phone)";

                // Parámetros para la consulta INSERT
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@CustomerID", customer.CustomerID),
                    new SqlParameter("@Cedula", customer.Cedula),
                    new SqlParameter("@Name", customer.Name),
                    new SqlParameter("@Email", customer.Email),
                    new SqlParameter("@Phone", customer.Phone)
                };

                // Ejecutar la consulta SQL INSERT utilizando FromSqlRaw
                int rowsAffected = db.Database.ExecuteSqlRaw(sqlQuery, parameters);

                if (rowsAffected > 0)
                {
                    // La inserción fue exitosa, redirigir a la página de éxito o realizar alguna otra acción
                    return RedirectToAction("Index");
                }
                else
                {
                    // La inserción no tuvo éxito, manejar el error de alguna manera
                    return View("Index");
                }

                
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }

        public ActionResult CreateCompanion()
        {

            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompanion(CustomerCompanion customer)
        {

            return View();
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var customer = db.Customers.Find(id);

            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Customer editedCustomer)
        {
            try
            {
                var existingCustomer = db.Customers.Find(id);

                    /*existingCustomer.Name =  editedCustomer.Name;
                    existingCustomer.Email = editedCustomer.Email;
                    existingCustomer.Phone = editedCustomer.Phone;

                    
                    db.SaveChanges();*/


                    string sqlQuery = "UPDATE Customers SET Name = @Name, Email = @Email, Phone = @Phone WHERE CustomerID = @CustomerID";

                    // Parámetros para la consulta UPDATE
                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Name", editedCustomer.Name),
                        new SqlParameter("@Email", editedCustomer.Email),
                        new SqlParameter("@Phone", editedCustomer.Phone),
                        new SqlParameter("@CustomerID", id)
                    };

                    // Ejecutar la consulta SQL UPDATE utilizando FromSqlRaw
                    int rowsAffected = db.Database.ExecuteSqlRaw(sqlQuery, parameters);

                    if (rowsAffected > 0)
                    {
                        // La eliminación fue exitosa, redirigir a la página de éxito o realizar alguna otra acción
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // La eliminación no tuvo éxito, manejar el error de alguna manera
                        return NotFound(); // Puedes manejar el caso en que el cliente no se encuentra
                    }

            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var customer = db.Customers.Find(id);
            return View(customer);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                /*var customer = db.Customers.Find(id);

                if (customer == null)
                {
                    return NotFound(); // Puedes manejar el caso en que el cliente no se encuentra
                }

                // Eliminar el cliente de la base de datos
                db.Customers.Remove(customer);

                // Guardar los cambios en la base de datos
                db.SaveChanges();

                // Redirigir a la página de éxito o realizar alguna otra acción
                return RedirectToAction("Index");*/

                string sqlQuery = "DELETE FROM Customers WHERE CustomerID = @CustomerID";

                // Parámetros para la consulta DELETE
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@CustomerID", id)
                };

                // Ejecutar la consulta SQL DELETE utilizando FromSqlRaw
                int rowsAffected = db.Database.ExecuteSqlRaw(sqlQuery, parameters);

                if (rowsAffected > 0)
                {
                    // La eliminación fue exitosa, redirigir a la página de éxito o realizar alguna otra acción
                    return RedirectToAction("Index");
                }
                else
                {
                    // La eliminación no tuvo éxito, manejar el error de alguna manera
                    return RedirectToAction("Index"); // Puedes manejar el caso en que el cliente no se encuentra
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
