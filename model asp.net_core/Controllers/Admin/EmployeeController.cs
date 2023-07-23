using Microsoft.AspNetCore.Mvc;
using model_asp.net_core.Models;
using System.Data.SqlClient;
using model_asp.net_core.Controllers.Admin.Document;
using System.Diagnostics;


namespace model_asp.net_core.Controllers.Admin
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeeController> _logger;
        protected SqlConnection con=null;
        protected SqlCommand cmd = null;
        protected SqlDataReader read = null;
        protected List<EmployeeModel> listEmployee = null;
        protected EmployeeModel employeeModel = null;

        public EmployeeController(ILogger<EmployeeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("employee")]
        public IActionResult Index()
        {
            listEmployee = new List<EmployeeModel>();
            try
            {
                string sql_select = "SELECT * FROM employees";
                con = new SqlConnection(_configuration.GetConnectionString("solus"));
                cmd = new SqlCommand(sql_select, con);
                con.Open();

                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    employeeModel = new EmployeeModel();
                    employeeModel.Id = Int16.Parse(read["id"].ToString());
                    employeeModel.Name = read["name"].ToString();
                    employeeModel.Surname = read["surname"].ToString();
                    employeeModel.Gender = read["gender"].ToString();
                    employeeModel.Document = read["document"].ToString();
                    employeeModel.DocNumber = read["doc_number"].ToString();
                    employeeModel.CreateAt = DateTime.Parse(read["create_at"].ToString());
                    listEmployee.Add(employeeModel);
                }
                read.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("Views/Admin/Employee/Index.cshtml");
            }

            ViewBag.listEmployee = listEmployee;
            return View("Views/Admin/Employee/Index.cshtml");
        }

        [HttpGet]
        [Route("employee/create")]
        public IActionResult Create()
        {
            ViewBag.Documents = new DocumentController(new SqlConnection(_configuration.GetConnectionString("solus"))).GetDocuments();
            return View("Views/Admin/Employee/Create.cshtml");
        }

        [HttpPost]
        [Route("employee/create")]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string sql_insert = "INSERT INTO employees (id,name,surname,gender,document,doc_number,create_at) VALUES (@id,@name,@surname,@gender,@document,@doc_number,@create_at)";
                con = new SqlConnection(_configuration.GetConnectionString("solus"));
                cmd = new SqlCommand(sql_insert, con);
                con.Open();

                cmd.Parameters.AddWithValue("@id", collection["id"].Equals("") ? null : collection["id"].ToString());
                cmd.Parameters.AddWithValue("@name", collection["name"].Equals("") ? null : collection["name"].ToString());
                cmd.Parameters.AddWithValue("@surname", collection["surname"].Equals("") ? null : collection["surname"].ToString());
                cmd.Parameters.AddWithValue("@gender", collection["gender"].Equals("") || collection["gender"].Equals("Selecione") ? null : collection["gender"].ToString());
                cmd.Parameters.AddWithValue("@document", collection["document"].Equals("") || collection["document"].Equals("Selecione") ? null : collection["document"].ToString());
                cmd.Parameters.AddWithValue("@doc_number", collection["doc_number"].Equals("") ? null : collection["doc_number"].ToString());
                cmd.Parameters.AddWithValue("@create_at", DateTime.Now);
                cmd.ExecuteNonQuery();

                con.Close();
                TempData["in"] = "Customer saved successfully!";
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("Views/Admin/Employee/Create.cshtml");
            }
        }

        [HttpGet]
        [Route("employee/edit/{id?}")]
        public IActionResult Edit(int id)
        {
            try
            {
                string sql_select = "SELECT * FROM employees WHERE id=@id";
                con = new SqlConnection(_configuration.GetConnectionString("solus"));
                cmd = new SqlCommand(sql_select, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);

                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    employeeModel = new EmployeeModel();
                    employeeModel.Id = Int16.Parse(read["id"].ToString());
                    employeeModel.Name = read["name"].ToString();
                    employeeModel.Surname = read["surname"].ToString();
                    employeeModel.Gender = read["gender"].ToString();
                    employeeModel.Document = read["document"].ToString();
                    employeeModel.DocNumber = read["doc_number"].ToString();
                    employeeModel.Genders = Standard.UpdateGender(employeeModel.Gender);
                    employeeModel.Documents = new DocumentController(new SqlConnection(_configuration.GetConnectionString("solus"))).UpdateDocument(employeeModel.Document);
                }
                read.Close();
                con.Close();

                ViewBag.E = employeeModel;
                return View("Views/Admin/Employee/Edit.cshtml");
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("Views/Admin/Employee/Edit.cshtml");
            }
        }

        [HttpPost]
        [Route("employee/edit/{id?}")]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                string sql_update = "UPDATE employees SET name=@name, surname=@surname, gender=@gender, document=@document, doc_number=@doc_number WHERE id=@id";
                con = new SqlConnection(_configuration.GetConnectionString("solus"));
                cmd = new SqlCommand(sql_update, con);
                con.Open();

                cmd.Parameters.AddWithValue("@name", collection["name"].Equals("") ? null : collection["name"].ToString());
                cmd.Parameters.AddWithValue("@surname", collection["surname"].Equals("") ? null : collection["surname"].ToString());
                cmd.Parameters.AddWithValue("@gender", collection["gender"].Equals("") || collection["gender"].Equals("Selecione") ? null : collection["gender"].ToString());
                cmd.Parameters.AddWithValue("@document", collection["document"].Equals("") || collection["document"].Equals("Selecione") ? null : collection["document"].ToString());
                cmd.Parameters.AddWithValue("@doc_number", collection["doc_number"].Equals("") ? null : collection["doc_number"].ToString());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                con.Close();
                TempData["up"] = "Customer updated successfully!";
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                ViewBag.E = new EmployeeModel();
                return View("Views/Admin/Employee/Edit.cshtml");
            }
        }

        [HttpGet]
        [Route("employee/details/{id?}")]
        public IActionResult Details(int id)
        {
            try
            {
                string sql_select = "SELECT * FROM employees WHERE id=@id";
                con = new SqlConnection(_configuration.GetConnectionString("solus"));
                cmd = new SqlCommand(sql_select, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);

                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    employeeModel = new EmployeeModel();
                    employeeModel.Id = Int16.Parse(read["id"].ToString());
                    employeeModel.Name = read["name"].ToString();
                    employeeModel.Surname = read["surname"].ToString();
                    employeeModel.Gender = read["gender"].ToString();
                    employeeModel.Document = read["document"].ToString();
                    employeeModel.DocNumber = read["doc_number"].ToString();
                }
                read.Close();
                con.Close();

                ViewBag.E = employeeModel;
                return View("Views/Admin/Employee/Details.cshtml");
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("Views/Admin/Employee/Details.cshtml");
            }
        }

        [HttpGet]
        [Route("employee/delete/{id?}")]
        public IActionResult Delete(int id)
        {
            try
            {
                string sql_delete = "DELETE FROM employees WHERE id=@id";
                con = new SqlConnection(_configuration.GetConnectionString("solus"));
                cmd = new SqlCommand(sql_delete, con);
                con.Open();

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                con.Close();
                TempData["del"] = "Employee deleted successfully!";
                return RedirectToAction("Index", "Employee");
            }
            catch(Exception ex)
            {
                this.Index();
                ViewBag.listEmployee = listEmployee;
                ViewData["error"] = ex.Message;
                return View("Views/Admin/Employee/Index.cshtml");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
