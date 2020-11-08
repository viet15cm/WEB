using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.UI;
using WEBAPI.GlobalVariablesWeb;
using WEBAPI.Models;
using WEBAPI.MVCModels;
using WEBAPI.MVCModels.JionModels;

namespace WEBAPI.Controllers.ControllersMVC
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IEnumerable<mvcDepartments> empList;
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Departments").Result;

            empList = reponse.Content.ReadAsAsync<IEnumerable<mvcDepartments>>().Result;
            return View(empList);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*", Location = OutputCacheLocation.None)]
        public ActionResult Employee()
        {
            IEnumerable<mvcEmployee> empList;
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Employees").Result;

            empList = reponse.Content.ReadAsAsync<IEnumerable<mvcEmployee>>().Result;
            return View(empList);
        }

        public ActionResult EmployeeDepartments()
        {
            IEnumerable<EmployeeDepartments> empList;
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Employees/JoinEmployeeDepartment").Result;
           
            empList = reponse.Content.ReadAsAsync<IEnumerable<EmployeeDepartments>>().Result;
            return View(empList);
        }

        [HttpGet]
        public ActionResult AddOrEdit(string ID)
        {
            if(ID == null)
                return View(new mvcEmployee());
            else
            {
                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Employees/GetEmployee/"+ ID).Result;
                return View(reponse.Content.ReadAsAsync<mvcEmployee>().Result);
            }

        }

        [HttpPost]
       
        public ActionResult AddOrEdit(mvcEmployee em)
        {
            if (em.ID == null)
            {


                HttpResponseMessage reponse = GlobalVariables.WebApiClient.PostAsJsonAsync("Employees/PostEmployee", em).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Thêm Thành Công";
                    

                }
                else
                {
                    TempData["ErrorMessage"] = "Thêm Không Thành Công";

                }
            }
            else
            {

                HttpResponseMessage reponse = GlobalVariables.WebApiClient.PutAsJsonAsync("Employees/PutEmployee/" + em.ID, em).Result;
                if (reponse.IsSuccessStatusCode) { 
                    TempData["SuccessMessage"] = "Sữa Thành Công";
                    

                }
                else
                {
                    TempData["ErrorMessage"] = "Sữa Không Thành Công";
                    
                }
            }
           
            return RedirectToAction("Employee");
        }

        
        public ActionResult Delete(string ID)
        {
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.DeleteAsync("Employees/DeleteEmployee/" + ID).Result;
            TempData["SuccessMessage"] = "Xóa Thành Công";
            return RedirectToAction("Employee");
        }
    }
}