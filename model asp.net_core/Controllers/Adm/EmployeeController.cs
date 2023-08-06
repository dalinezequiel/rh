using Microsoft.AspNetCore.Mvc;
using model_asp.net_core.Models;
using System.Data.SqlClient;
using model_asp.net_core.Controllers.Admin.Document;
using model_asp.net_core.Controllers.Admin.Address;
using model_asp.net_core.Controllers.Adm.Status;
using System.Diagnostics;


namespace model_asp.net_core.Controllers.Admin
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeeController> _logger;
        protected SqlConnection con;
        protected SqlTransaction tran;
        protected SqlCommand cmd;
        protected SqlDataReader read;
        protected List<EmployeeModel> listEmployee;
        protected EmployeeModel employeeModel;
        protected DocumentModel documentModel;
        protected ProvinceModel provinceModel;
        protected DistrictModel districtModel;
        protected AddressModel addressModel;
        protected ContactModel contactModel;

        public EmployeeController(ILogger<EmployeeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("solus"));
        }

        [HttpGet]
        [Route("employee")]
        public IActionResult Index()
        {
            listEmployee = new List<EmployeeModel>();
            try
            {
                string sql_select = "SELECT * FROM employees";
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
                    documentModel = new DocumentModel();
                    documentModel.Name = read["document"].ToString();
                    documentModel.Number = read["doc_number"].ToString();
                    employeeModel.Document = documentModel;
                    employeeModel.CreatedAt = DateTime.Parse(read["created_at"].ToString());
                    listEmployee.Add(employeeModel);
                }
                read.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("Views/Adm/Employee/Index.cshtml");
            }

            ViewBag.listEmployee = listEmployee;
            return View("Views/Adm/Employee/Index.cshtml");
        }

        [HttpGet]
        [Route("employee/create")]
        public IActionResult Create()
        {
            districtModel = new DistrictModel();
            districtModel.Districts = new AddressController().GetDistricts(this.con);

            provinceModel = new ProvinceModel();
            provinceModel.Provinces = new AddressController().GetProvinces(this.con);
            provinceModel.District = districtModel;

            addressModel = new AddressModel();
            addressModel.Province = provinceModel;

            documentModel = new DocumentModel();
            documentModel.Documents = new DocumentController().GetDocuments(this.con);

            employeeModel = new EmployeeModel();
            employeeModel.Document = documentModel;
            employeeModel.Address = addressModel;

            ViewBag.Default = employeeModel;
            return View("Views/Adm/Employee/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("employee/create")]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string sql_insert = "INSERT INTO employees (id,name,surname,gender,birthday,document,doc_number,date_of_issue,place_of_issue,province,district,neighborhood,road,avenue,email,phone,statux,created_at,updated_at) VALUES (@id,@name,@surname,@gender,@birthday,@document,@doc_number,@date_of_issue,@place_of_issue,@province,@district,@neighborhood,@road,@avenue,@email,@phone,@statux,@created_at,@updated_at)";
                cmd = new SqlCommand(sql_insert, con);
                con.Open();

                cmd.Parameters.AddWithValue("@id", collection["id"].Equals("") ? null : collection["id"].ToString());
                cmd.Parameters.AddWithValue("@name", collection["name"].Equals("") ? null : collection["name"].ToString());
                cmd.Parameters.AddWithValue("@surname", collection["surname"].Equals("") ? null : collection["surname"].ToString());
                cmd.Parameters.AddWithValue("@gender", collection["gender"].Equals("") || collection["gender"].Equals("Selecione") ? null : collection["gender"].ToString());
                cmd.Parameters.AddWithValue("@birthday", collection["birthday"].Equals("") ? null : DateTime.Parse(collection["birthday"].ToString()));
                cmd.Parameters.AddWithValue("@document", collection["document"].Equals("") || collection["document"].Equals("Selecione") ? null : collection["document"].ToString());
                cmd.Parameters.AddWithValue("@doc_number", collection["doc_number"].Equals("") ? null : collection["doc_number"].ToString());
                cmd.Parameters.AddWithValue("@date_of_issue", collection["date_issue"].Equals("") ? null : DateTime.Parse(collection["date_issue"].ToString()));
                cmd.Parameters.AddWithValue("@place_of_issue", collection["place_issue"].Equals("") ? null : collection["place_issue"].ToString());
                cmd.Parameters.AddWithValue("@province", collection["province"].Equals("") ? null : collection["province"].ToString());
                cmd.Parameters.AddWithValue("@district", collection["district"].Equals("") ? null : collection["district"].ToString());
                cmd.Parameters.AddWithValue("@neighborhood", collection["neighborhood"].Equals("") ? null : collection["neighborhood"].ToString());
                cmd.Parameters.AddWithValue("@road", collection["road"].Equals("") ? null : collection["road"].ToString());
                cmd.Parameters.AddWithValue("@avenue", collection["avenue"].Equals("") ? null : collection["avenue"].ToString());
                cmd.Parameters.AddWithValue("@email", collection["email"].Equals("") ? null : collection["email"].ToString());
                cmd.Parameters.AddWithValue("@phone", collection["phone"].Equals("") ? null : collection["phone"].ToString());
                cmd.Parameters.AddWithValue("@statux", collection["status"].Equals("") || collection["status"].Equals("Selecione") ? null : collection["status"].ToString());
                cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                cmd.Parameters.AddWithValue("@updated_at", DateTime.Now);
                cmd.ExecuteNonQuery();

                con.Close();
                TempData["in"] = "Customer saved successfully!";
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("Views/Adm/Employee/Create.cshtml");
            }
        }

        [HttpGet]
        [Route("employee/edit/{id?}")]
        public IActionResult Edit(int id)
        {
            try
            {
                string sql_select = "SELECT * FROM employees WHERE id=@id";
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

                    documentModel = new DocumentModel();
                    documentModel.Name = read["document"].ToString();
                    documentModel.Number = read["doc_number"].ToString();
                    documentModel.DateOfIssue = DateTime.Parse(read["date_of_issue"].ToString());
                    documentModel.DateIssue = String.Format("{0:yyyy-MM-dd}", documentModel.DateOfIssue);
                    documentModel.PlaceOfIssue = read["place_of_issue"].ToString();
                    //documentModel.Documents = new DocumentController().UpdateDocument(con, documentModel.Name);
                    employeeModel.Document = documentModel;

                    addressModel = new AddressModel();
                    addressModel.Neighborhood = read["neighborhood"].ToString();
                    addressModel.Road = read["road"].ToString();
                    addressModel.Avenue = read["avenue"].ToString();
                    employeeModel.Address = addressModel;

                    contactModel = new ContactModel();
                    contactModel.Email = read["email"].ToString();
                    contactModel.Phone = read["phone"].ToString();
                    contactModel.WhatsApp = read["phone"].ToString();
                    employeeModel.Contact = contactModel;

                    employeeModel.Birthday = DateTime.Parse(read["birthday"].ToString());
                    employeeModel.Birth = String.Format("{0:yyyy-MM-dd}", employeeModel.Birthday);
                    employeeModel.Status = read["statux"].ToString();
                    employeeModel.Genders = Standard.UpdateGender(employeeModel.Gender);
                    employeeModel.Documents = new DocumentController().UpdateDocument(con, employeeModel.Document.Name);
                    employeeModel.States = new StatusController().UpdateStatus(employeeModel.Status);
                }
                read.Close();
                con.Close();

                ViewBag.Data = employeeModel;
                return View("Views/Adm/Employee/Edit.cshtml");
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("Views/Adm/Employee/Edit.cshtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("employee/edit/{id?}")]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                string sql_update = "UPDATE employees SET name=@name, surname=@surname, gender=@gender, document=@document, doc_number=@doc_number WHERE id=@id";
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
                return View("Views/Adm/Employee/Edit.cshtml");
            }
        }

        [HttpGet]
        [Route("employee/details/{id?}")]
        public IActionResult Details(int id)
        {
            try
            {
                string sql_select = "SELECT * FROM employees WHERE id=@id";
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
                    documentModel = new DocumentModel();
                    documentModel.Name = read["document"].ToString();
                    documentModel.Number = read["doc_number"].ToString();
                    employeeModel.Document = documentModel;
                    employeeModel.CreatedAt = DateTime.Parse(read["created_at"].ToString());
                }
                read.Close();
                con.Close();

                ViewBag.E = employeeModel;
                return View("Views/Adm/Employee/Details.cshtml");
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View("Views/Adm/Employee/Details.cshtml");
            }
        }

        [HttpGet]
        [Route("employee/delete/{id?}")]
        public IActionResult Delete(int id)
        {
            try
            {
                string sql_delete = "DELETE FROM employees WHERE id=@id";
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
                return View("Views/Adm/Employee/Index.cshtml");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
