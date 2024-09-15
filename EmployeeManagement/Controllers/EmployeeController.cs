using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // Instancia del contexto
        private EmployeeDBContext db = new EmployeeDBContext();

        // Accion para obtener la list de empleados
        public ActionResult Index()
        {
            return View();
        }

        // Accion para obtener la list de empleados en formato json
        [HttpGet]
        public JsonResult GetEmployees()
        {
            var employees = db.Employees.ToList();  // Consulta a la base de datos
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        // Accion para mostrar la vista de add empleado
        public ActionResult AddEmployeeView()
        {
            return View();
        }
        // Accion para agregar un nuevo empleado
        [HttpPost]
        public JsonResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Validar que name, position y office sean cadenas de texto
                if (string.IsNullOrWhiteSpace(employee.Name) || string.IsNullOrWhiteSpace(employee.Position) || string.IsNullOrWhiteSpace(employee.Office))
                {
                    return Json(new { success = false, errors = "Nombre, Cargo y Oficina no pueden estar vacíos." });
                }

                // Validar que el salario sea un numero positivo
                if (employee.Salary <= 0)
                {
                    return Json(new { success = false, errors = "El salario debe ser un número positivo." });
                }

                db.Employees.Add(employee); // Agregar empleado a la tabla
                db.SaveChanges();           // Guardar cambios en la base de datos
                return Json(new { success = true });
            }

            // Devuelve los errores de validación
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        // Accion para eliminar un empleado
        [HttpPost]
        public JsonResult DeleteEmployee(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee != null)
            {
                db.Employees.Remove(employee); // Eliminar empleado
                db.SaveChanges();              // Guardar cambios
                return Json(new { success = true });
            }
            return Json(new { success = false });


        }

        // Accion para obtener la vista de edit
        [HttpGet]
        public ActionResult EditEmployeeView(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // Accion para actualizar la información de un empleado
        [HttpPost]
        public JsonResult UpdateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Validar que name, position y office sean cadenas de texto
                if (string.IsNullOrWhiteSpace(employee.Name) ||
                    string.IsNullOrWhiteSpace(employee.Position) ||
                    string.IsNullOrWhiteSpace(employee.Office))
                {
                    return Json(new { success = false, errors = "Nombre, Cargo y Oficina no pueden estar vacíos." });
                }

                // Validar que el salario sea un numero positivo
                if (employee.Salary <= 0)
                {
                    return Json(new { success = false, errors = "El salario debe ser un número positivo." });
                }

                // Buscar el empleado por ID
                var existingEmployee = db.Employees.Find(employee.Id);
                if (existingEmployee != null)
                {
                    existingEmployee.Name = employee.Name;
                    existingEmployee.Position = employee.Position;
                    existingEmployee.Office = employee.Office;
                    existingEmployee.Salary = employee.Salary;
                    db.SaveChanges();  // Guardar cambios
                    return Json(new { success = true });
                }

                // Empleado no encontrado
                return Json(new { success = false, errors = "Empleado no encontrado." });
            }

            // Devuelve los errores de validación
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

    }
}


