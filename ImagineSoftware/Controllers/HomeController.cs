using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ImagineSoftware.Models;
using ImagineSoftware.Utilities;
using System.Web.Http;
using ImagineSoftware.DAL;

namespace ImagineSoftware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISoftwareUtility software;

        public HomeController(ISoftwareUtility software)
        {
            this.software = software;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GreaterSoftware([FromUri]SearchRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new List<Software>
                    {
                        new Software { Name = "Invalid", Version = "Input was not in correct format." }
                    }
                );
            }

            var inputVersion = software.GetInputVersion(request.Input);
            return new JsonResult(software.GetSoftwareGreaterThan(inputVersion, SoftwareManager.GetAllSoftware()));
        }
    }
}
